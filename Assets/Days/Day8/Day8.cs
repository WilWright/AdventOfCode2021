using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day8 : MonoBehaviour {
    public int day = 8;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    static string[] digitMaster = new string[] {
        "abcefg", //0 6
        "cf",     //1 2
        "acdeg",  //2 5
        "acdfg",  //3 5
        "bcdf",   //4 4
        "abdfg",  //5 5
        "abdefg", //6 6
        "acf",    //7 3
        "abcdefg",//8 7
        "abcdfg"  //9 6
    };    class Entry {
        public string[] signalPatterns;
        public string[] outputValue;
        public string[] digits = new string[10];
        public char[] segments = new char[7];
        List<string> patternsToFind = new List<string>();

        public Entry(string line, bool lastLine) {
            string[] values = line.Replace("\n", "").Replace(" | ", "|").Split('|');
            signalPatterns = values[0].Split(' ');
            outputValue = values[1].Split(' ');
            if (!lastLine)
                outputValue[3] = outputValue[3].Substring(0, outputValue[3].Length - 1);
            
            Sort(signalPatterns);
            Sort(outputValue);

            // Get digits 1,4,7,8
            foreach (string s in signalPatterns)
                Decode(s);

            FindSegment('a'); //(1,7)
            FindDigit(9);     //(4+a)
            FindSegment('g'); //(4+a,9)
            FindSegment('e'); //(8,9)
            FindDigit(3);     //(1+a+g)
            FindSegment('d'); //(1+a+g,3)
            FindDigit(0);     //(1+a+e+g)
            FindSegment('b'); //(1+a+e+g,0)
            FindDigit(2);     //(a+d+e+g)
            FindSegment('c'); //(a+d+e+g,2)
            digits[5] = patternsToFind[0].Length == 5 ? patternsToFind[0] : patternsToFind[1]; // Only 5 or 6 left
            patternsToFind.Remove(digits[5]);
            digits[6] = patternsToFind[0];
            FindSegment('f'); //(a+b+d+e+g)
        }

        public string GetOutput() {
            string output = "";
            foreach (string s in outputValue) {
                for (int i = 0; i < digits.Length; i++) {
                    if (s == digits[i]) {
                        output += i + "";
                        break;
                    }
                }
            }
            return output;
        }

        void Sort(string[] strings) {
            for(int i = 0; i < strings.Length; i++) {
                char[] c = strings[i].ToCharArray();
                System.Array.Sort(c);
                string s = "";
                foreach (char cc in c)
                    s += cc;
                strings[i] = s;
            }
        }

        public void Decode(string pattern) {
            switch (pattern.Length) {
                case 2:
                case 3:
                case 4:
                case 7:
                    for (int i = 0; i < digitMaster.Length; i++) {
                        if (pattern.Length == digitMaster[i].Length) {
                            digits[i] = pattern;
                            break;
                        }
                    }
                    break;

                default:
                    patternsToFind.Add(pattern);
                    break;
            }
        }

        void FindDigit(int digit) {
            switch (digit) {
                case 0: digits[0] = SearchAgainst(digits[1] + segments[0] + segments[4] + segments[6]); break;
                case 2: digits[2] = SearchAgainst("" + segments[0] + segments[3] + segments[4] + segments[6]); break;
                case 3: digits[3] = SearchAgainst(digits[1] + segments[0] + segments[6]); break;
                case 9: digits[9] = SearchAgainst(digits[4] + segments[0]); break;
            }

        }

        void FindSegment(char segment) {
            switch (segment) {
                case 'a': segments[0] = OddOneOut(digits[1], digits[7]); return;
                case 'b': segments[1] = OddOneOut(digits[0], digits[1] + segments[0] + segments[4] + segments[6]); return;
                case 'c': segments[2] = OddOneOut("" + segments[0] + segments[3] + segments[4] + segments[6], digits[2]); return;
                case 'd': segments[3] = OddOneOut(digits[1] + segments[0] + segments[6], digits[3]); return;
                case 'e': segments[4] = OddOneOut(digits[8], digits[9]); return;
                case 'f': segments[5] = OddOneOut("" + segments[0] + segments[1] + segments[3] + segments[4] + segments[6], digits[5]); return;
                case 'g': segments[6] = OddOneOut(digits[4] + segments[0], digits[9]); return;
            }
        }

        string SearchAgainst(string s1) {
            foreach (string s in patternsToFind) {
                if (OddOneOut(s1, s) != 'z') {
                    patternsToFind.Remove(s);
                    return s;
                }
            }
            return null;
        }

        char OddOneOut(string s1, string s2) {
            if (Mathf.Abs(s1.Length - s2.Length) != 1)
                return 'z';

            if (s1.Length < s2.Length) {
                string temp = s1;
                s1 = s2;
                s2 = temp;
            }

            char odd = 'z';
            int odds = 0;
            foreach (char c in s1) {
                bool found = false;
                foreach (char c2 in s2) {
                    if (c == c2) {
                        found = true;
                        break;
                    }
                }
                if (!found) {
                    odd = c;
                    odds++;
                }
            }

            return odds == 1 ? odd : 'z';
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // Once again I got burned a little parsing the input. There was some invisible character (not \n) at the end of most of the lines.
        // I think it's time to make a proper string parser instead of having headaches with replacing and splitting a bunch of uneeded characters.
        // This challenge took a bit to understand what the overall problem was about. Other than that the logic was very simple for this part. 
        // I started with some basic needs for an Entry. This part was mostly preparing us for how to think about Part 2.        int uniques = 0;        for (int i = 0; i < stringInput.Length; i++) {            Entry e = new Entry(stringInput[i], i == stringInput.Length - 1);
            foreach (string o in e.outputValue) {
                switch (o.Length) {
                    case 2:
                    case 4:
                    case 3:
                    case 7:
                        uniques++;
                        break;
                }
            }
        }                print(uniques);    }    void Part2() {        print("Part 2");

        // Part 2 was really tough. There was a lot of information to unpack, it was crucial to really examine the examples in this one.
        // After making sure I understood the challenge completely, I began to break it down into the steps I'll need.

        // The first place I started was figuring out that overlapping 1 (CF) with 7 (ACF) would guarantee what segment A is. This lead to the OddOneOut() function.
        // If I could overlap any two patterns that I knew were the same except for one extra segment, then I know where that segment belongs.
        // Similary, I could also work backwards from the remaining unfound digits. The next step I figured out was that 4 plus the now guaranteed A would
        // have an OddOneOut with G, the 9's tail, which lead to the SearchAgainst() function.

        // After having the tools I needed, I just had to manually go over the digit patterns and figure out which combinations of available digits and segments
        // would lead to more unfound digits or segments, and then execute the functions in the correct order. This took a long time, and as I got more tired it got slower.
        // When I was all done I then needed to match the outputs with the unscrambled digits. I ran the program, and it almost worked first try,
        // but I had some logic errors I needed to debug (the manual pattern logic was solid at least). Part 2 in total took almost 3 hours.
        // Even with my slow start to Part 1 and headaches in Part 2, my rank only dropped from 9,840 to 9,899 though, so most other people seemed to have trouble as well.

        int sum = 0;        for (int i = 0; i < stringInput.Length; i++) {            Entry e = new Entry(stringInput[i], i == stringInput.Length - 1);
            sum += int.Parse(e.GetOutput());
        }

        print(sum);
    }}