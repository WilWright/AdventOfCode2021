using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day3 : MonoBehaviour {
    public int day = 3;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    int bitCount;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); bitCount = 5;  break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); bitCount = 12; break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // This challenge was a little step up in difficulty, but it was fun.        // To keep track of the most common bits I used a net value,        // so any amount below 0 was a 0, and above 0 was a 1,        // a net value of 0 meant a tie, but that didn't matter for Part 1.        // I knew the epsilon was just the opposite of the gamma, so I only kept track of gamma        // and then flipped it. For calculating the actual binary number I looped through the bits        // and got their values with 2^i, but later after looking at other participants' solutions I learned        // I could have just used Convert.ToInt32(binaryString, 2).        int[] gamma = new int[bitCount];        int[] epsilon = new int[bitCount];        foreach (string s in stringInput) {
            for (int i = 0; i < s.Length; i++) {
                switch (s[i]) {
                    case '1': gamma[i]++; break;
                    case '0': gamma[i]--; break;
                }
            }
        }        for (int i = 0; i < bitCount; i++) {
            if (gamma[i] > 0) { gamma[i] = 1; epsilon[i] = 0; }
            else              { gamma[i] = 0; epsilon[i] = 1; }
        }        int decGamma = 0;        int decEps = 0;        for (int i = bitCount - 1; i >= 0; i--) {
            int power = (int)Mathf.Pow(2, bitCount - i - 1);
            decGamma += gamma[i] * power;
            decEps += epsilon[i] * power;
        }

        print(decGamma*decEps);
    }    void Part2() {        print("Part 2");

        // Part 2 added quite a lot of complexity to the challenge, needing us to filter down
        // the bit strings based on Part 1's criteria of most/least common bits for 
        // oxygen generator and CO2 scrubber ratings.        // I started with the oxygen generator, because I knew the CO2 scrubber would be the same logic,        // just swapped. After getting it right I copied the logic and changed the names.        // Unfortunately I missed some spots and got wrong answers which took some debugging,        // but after a quick lookover I caught and fixed them.        List<string> oxygen = new List<string>();        List<string> scrubber = new List<string>();        foreach (string s in stringInput) {            oxygen.Add(s);
            scrubber.Add(s);
        }        for (int i = 0; i < bitCount; i++) {
            if (oxygen.Count == 1 && scrubber.Count == 1)
                break;

            List<string> oxygenOnes = new List<string>();
            List<string> oxygenZeros = new List<string>();
            int oxyScore = 0;
            foreach (string s in oxygen) {
                switch (s[i]) {
                    case '1': oxyScore++; oxygenOnes.Add(s); break;
                    case '0': oxyScore--; oxygenZeros.Add(s); break;
                }
            }
            if (oxygen.Count > 1) oxygen = oxyScore >= 0 ? oxygenOnes : oxygenZeros;

            List<string> scrubberOnes = new List<string>();
            List<string> scrubberZeros = new List<string>();
            int scrubScore = 0;
            foreach (string s in scrubber) {
                switch (s[i]) {
                    case '1': scrubScore++; scrubberOnes.Add(s); break;
                    case '0': scrubScore--; scrubberZeros.Add(s); break;
                }
            }
            if (scrubber.Count > 1) scrubber = scrubScore >= 0 ? scrubberZeros : scrubberOnes;
        }        int decOxy = 0;        int decScrub = 0;                for (int i = bitCount - 1; i >= 0; i--) {
            int power = (int)Mathf.Pow(2, bitCount - i - 1);
            decOxy += int.Parse(oxygen[0][i] + "") * power;
            decScrub += int.Parse(scrubber[0][i] + "") * power;
        }        
        print(decOxy*decScrub);
    }}