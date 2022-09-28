using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day4 : MonoBehaviour {
    public int day = 4;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    public class Board {
        public int[,] grid = new int[5, 5];
        public bool[,] marks = new bool[5, 5];

        public Board(string[] boardLines) {
            int lineCount = 0;
            for (int y = 4; y >= 0; y--) {
                string[] nums = boardLines[lineCount++].Replace("  ", " ").Replace("\n", " ").Split(' ');
                List<string> filter = new List<string>();
                foreach (string s in nums) {
                    if (s.Length > 0)
                        filter.Add(s);
                }
                for (int x = 0; x < 5; x++)
                    grid[x, y] = int.Parse(filter[x]);
            }
        }

        public void Mark(int num) {
            for (int y = 4; y >= 0; y--) {
                for (int x = 0; x < 5; x++) {
                    if (grid[x, y] == num)
                        marks[x, y] = true;
                }
            }
        }

        public bool Check() {
            for (int i = 0; i < 5; i++) {
                bool fail = false;
                for (int j = 0; j < 5; j++) {
                    if (!marks[i, j]) {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                    return true;
            }

            for (int i = 0; i < 5; i++) {
                bool fail = false;
                for (int j = 0; j < 5; j++) {
                    if (!marks[j, i]) {
                        fail = true;
                        break;
                    }
                }
                if (!fail)
                    return true;
            }

            return false;
        }

        public int Sum() {
            int sum = 0;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (!marks[i, j])
                        sum += grid[i, j];
                }
            }
            return sum;
        }

        public override string ToString() {
            string lines = "";
            for (int y = 4; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < 5; x++)
                    s += grid[x, y] + " ";
                lines += s + "\n";
            }
            return lines;
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        intInput = AdventHelper.GetIntInput(stringInput[0].Split(','));        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This challenge was another big leap in difficulty, taking me 1 hour and 18 minutes for Part 1
        // compared to the average 15 minutes for previous days. Despite this, I still got a rank of 7,514 which
        // was better than the 7,929 I got yesterday. I guess a lot of other participants had issues with this challenge as well.
        // For me the bulk of this time wasn't even the Board logic, but dealing with parsing the input.
        // This is what one board of values looks like:

        // 22 13 17 11  0
        //  8  2 23  4 24
        // 21  9 14 16  7
        //  6 10  3 18  5
        //  1 12 20 15 19

        // The single digits having an extra space really tripped me up, but I eventually was able to get to
        // string[] nums = boardLines[lineCount++].Replace("  ", " ").Replace("\n", " ").Split(' '); for each line.
        // If a single digit was first, this also left me with an array with an empty string in the first element,
        // so I then had to filter each string and check for length > 0 on each element.

        // The rest of the logic followed pretty smoothly afterwards. I made a Board class which is a 5x5 int[,],
        // with a similar bool[,] for marking numbers. Then I mark the input elements for each board,
        // and checked if that board won by checking all the rows and columns for bingo.

        List<Board> boards = new List<Board>();        int lineCount = 0;
        string[] boardLines = new string[5];
        for (int i = 2; i < stringInput.Length; i++) {
            boardLines[lineCount++] = stringInput[i];

            if (lineCount >= 5) {
                boards.Add(new Board(boardLines));
                boardLines = new string[5];
                lineCount = 0;
                i++;
            }
        }
        
        foreach (int i in intInput) {
            foreach (Board b in boards) {
                b.Mark(i);
                if (b.Check()) {
                    print(b.Sum() * i);
                    return;
                }
            }
        }    }    void Part2() {        print("Part 2");        // Part 2 was a simple iteration where we had to find the last winning board        // instead of the first. After a grueling hour of headaches this part only took another 10 minutes.        // The only thing that tripped me up was that after getting one board left that hasn't won,        // I initially forgot to continue running the inputs until that last one won to get the correct sum.        List<Board> boards = new List<Board>();        int lineCount = 0;
        string[] boardLines = new string[5];
        for (int i = 2; i < stringInput.Length; i++) {
            boardLines[lineCount++] = stringInput[i];

            if (lineCount >= 5) {
                boards.Add(new Board(boardLines));
                boardLines = new string[5];
                lineCount = 0;
                i++;
            }
        }
        
        int index = 0;
        while (boards.Count > 1) {
            for (int i = 0; i < intInput.Length; i++) {
                bool win = false;
                index = i;
                foreach (Board b in boards) {
                    b.Mark(intInput[i]);
                    if (b.Check()) {
                        boards.Remove(b);
                        win = true;
                        break;
                    }
                }
                if (win)
                    break;
            }
        }

        for (int i = index; i < intInput.Length; i++) {
            index = i;
            foreach (Board b in boards) {
                b.Mark(intInput[i]);
                if (b.Check()) {
                    print(boards[0].Sum() * intInput[index]);
                    return;
                }
            }
        }

    }}