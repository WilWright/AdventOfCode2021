using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day9 : MonoBehaviour {
    public int day = 9;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // Before the challenge started I was working on the string parser to make life a lot easier, but had to work on it a little
        // more while doing Part 1, so I had a slower time than I would've liked. The contents of Part 1 were very familiar, though.
        // In my game Wilbur's Quest I do something very similar where I check adjacent blocks to see if they're the same type,
        // and if they need to be tiled together. Here instead I would just need to check integers rather than block types.

        // After getting wrong answers I had to do some debugging. I had gotten tripped up on (next < current) vs. (next <= current).
        // There were a couple indications in the problem of why it needed to be like this, but like on day 7, the image I built in my head
        // had worked perfectly fine for the example input, but there were cases where it didn't work for the main input. It seemed like there
        // were quite a few other participants that had this problem as well, so I also think this challenge could have used some more explicit instructions.

        (int x, int y)[] directions = new (int x, int y)[] { (0, 1), (0, -1), (-1, 0), (1, 0) };        for (int i = 0; i < stringInput.Length; i++) {
            string[] s = AdventHelper.ParseString(stringInput[i]);
            stringInput[i] = s[0];
        }        int[,] grid = new int[stringInput.Length, stringInput[0].Length];        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = int.Parse(stringInput[i][j] + "");
        }        int sum = 0;        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                bool lowest = true;
                int current = grid[x, y];

                foreach (var d in directions) {
                    (int x, int y) n = (x + d.x, y + d.y);
                    if (WithinBounds(n) && grid[n.x, n.y] <= current)
                        lowest = false;
                }
                if (lowest)
                    sum += current + 1;
            }
        }        bool WithinBounds((int x, int y) coordinate) {
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < grid.GetLength(0) && coordinate.y < grid.GetLength(1);
        }

        print(sum);    }    void Part2() {        print("Part 2");        // Part 2 again needed very similar logic to a feature in Wilbur's Quest. In order to know where to draw air spaces for        // the map system, I would start with tunnel entrances and then search outwards. This way I can ignore any aesthetic holes in the terrain,        // and keep only relevant room information to make reading the map easier. The rest of the logic followed pretty simply,        // taking me in total about 25 minutes for Part 2. This was my best improvement so far between Parts 1 and 2, with my rank going from 8,033 to 5,288.        // In addition to the string parser, this challenge inclined me to think about bringing over the coordinate struct from my game, maybe also the grid system,        // as well as some math functions like a Sum() and Product(). This hasn't been the first time I've needed that logic and probably won't be the last.        (int x, int y)[] directions = new (int x, int y)[] { (0, 1), (0, -1), (-1, 0), (1, 0) };        for (int i = 0; i < stringInput.Length; i++) {
            string[] s = AdventHelper.ParseString(stringInput[i]);
            stringInput[i] = s[0];
        }

        int[,] grid = new int[stringInput.Length, stringInput[0].Length];        for (int i = 0; i < grid.GetLength(0); i++) {
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = int.Parse(stringInput[i][j] + "");
        }

        int[] largestBasins = new int[3];        for (int x = 0; x < grid.GetLength(0); x++) {
            for (int y = 0; y < grid.GetLength(1); y++) {
                bool lowest = true;
                int current = grid[x, y];

                foreach (var d in directions) {
                    (int x, int y) n = (x + d.x, y + d.y);
                    if (WithinBounds(n) && grid[n.x, n.y] <= current)
                        lowest = false;
                }

                if (lowest) {
                    int size = SearchBasin((x, y));
                    int smallest = -1;
                    for (int i = 0; i < largestBasins.Length; i++) {
                        if (smallest == -1)
                            smallest = largestBasins[i];
                        smallest = Mathf.Min(smallest, largestBasins[i]);
                    }
                    if (size > smallest) {
                        for (int i = 0; i < largestBasins.Length; i++) {
                            if (largestBasins[i] == smallest) {
                                largestBasins[i] = size;
                                break;
                            }
                        }
                    }
                }
            }
        }        bool WithinBounds((int x, int y) coordinate) {
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < grid.GetLength(0) && coordinate.y < grid.GetLength(1);
        }

        int SearchBasin((int x, int y) lowest) {
            List<(int x, int y)> check = new List<(int x, int y)>();
            check.Add(lowest);
            int index = 0;
            int size = 0;

            do {
                (int x, int y) current = check[index];
                size++;

                foreach (var d in directions) {
                    (int x, int y) n = (current.x + d.x, current.y + d.y);
                    if (WithinBounds(n) && !check.Contains(n)) {
                        if (grid[n.x, n.y] < 9)
                            check.Add(n);
                    }
                }
            } while (++index < check.Count);

            return size;
        }

        int product = 1;
        foreach (int i in largestBasins)
            product *= i;

        print(product);
    }}