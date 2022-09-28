using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day5 : MonoBehaviour {
    public int day = 5;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // This challenge was a little tough. We had to plot lines based on a start and end point,        // and the points weren't in ascending order so the logic needed a little more than a simple for loop.        // Luckily we only had to worry about horizontal and vertical lines. Using this I knew either the x's or the y's        // would be the same. To make the for loop work I also swapped the coordinates if the lower one needed to be first.        // Then depending on which axis the line was in I incremented the x or y coordinate.        // Instead of using a big 2D array I just used a dictionary, and when a point overlapped I just increased the amount at that location,        // or added it if it didn't exist.        Dictionary<(int x, int y), int> grid = new Dictionary<(int x, int y), int>();        int[][] coords = new int[stringInput.Length][];        for (int i = 0; i < stringInput.Length; i++) {
            coords[i] = new int[4];
            string[] s = stringInput[i].Replace(" -> ", ",").Split(',');
            for (int j = 0; j < s.Length; j++)
                coords[i][j] = int.Parse(s[j]);
        }        foreach (int[] coord in coords) {
            if (coord[0] != coord[2] && coord[1] != coord[3])
                continue;

            int axis = 0;
            for (int i = 0; i < 2; i++) {
                if (coord[i] != coord[i + 2])
                    axis = i;
                if (coord[i] > coord[i + 2]) {
                    int temp = coord[i];
                    coord[i] = coord[i + 2];
                    coord[i + 2] = temp;
                }
            }
            
            if (axis == 0) {
                for (int x = coord[0]; x <= coord[2]; x++) {
                    var c = (x, coord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
            else {
                for (int y = coord[1]; y <= coord[3]; y++) {
                    var c = (coord[0], y);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
        }        int points = 0;        foreach (var kvp in grid) {
            if (kvp.Value > 1)
                points++;
        }        print(points);    }    void Part2() {        print("Part 2");        // Part 2 had a dirty trick up its sleeve, asking us to now implement the diagonal lines.        // Thankfully the challenge only gave us 45 degree diagonals.        // Using this I knew going from start to end meant moving by +/-(1, 1) when iterating.        // First I checked the absolute values of the differences between x's and y's,        // which should each be 1 for diagonals. I couldn't use a for loop for these,        // because the x and y would move opposite each other instead of one or the other,        // so I checked which of the coordinates were lower and incremented/decremented each separately.        Dictionary<(int x, int y), int> grid = new Dictionary<(int x, int y), int>();        int[][] coords = new int[stringInput.Length][];        for (int i = 0; i < stringInput.Length; i++) {
            coords[i] = new int[4];
            string[] s = stringInput[i].Replace(" -> ", ",").Split(',');
            for (int j = 0; j < s.Length; j++)
                coords[i][j] = int.Parse(s[j]);
        }        foreach (int[] coord in coords) {
            if (Mathf.Abs(coord[0] - coord[2]) == Mathf.Abs(coord[1] - coord[3])) {
                int[] tempCoord = new int[4];
                coord.CopyTo(tempCoord, 0);

                var c = (tempCoord[0], tempCoord[1]);
                try { grid[c]++; }
                catch { grid.Add(c, 1); }

                while (tempCoord[0] != coord[2] && tempCoord[1] != coord[3]) {
                    if (tempCoord[0] < coord[2]) tempCoord[0]++;
                    else                         tempCoord[0]--;
                    if (tempCoord[1] < coord[3]) tempCoord[1]++;
                    else                         tempCoord[1]--;

                    c = (tempCoord[0], tempCoord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
                continue;
            }

            int axis = 0;
            for (int i = 0; i < 2; i++) {
                if (coord[i] != coord[i + 2])
                    axis = i;
                if (coord[i] > coord[i + 2]) {
                    int temp = coord[i];
                    coord[i] = coord[i + 2];
                    coord[i + 2] = temp;
                }
            }

            if (axis == 0) {
                for (int x = coord[0]; x <= coord[2]; x++) {
                    var c = (x, coord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
            else {
                for (int y = coord[1]; y <= coord[3]; y++) {
                    var c = (coord[0], y);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
        }        int points = 0;        foreach (var kvp in grid) {
            if (kvp.Value > 1)
                points++;
        }        print(points);    }}