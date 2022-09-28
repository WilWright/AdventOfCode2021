using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day2 : MonoBehaviour {
    public int day = 2;
    public Part part;
    public File file;
    public bool convertInputToInt;

    string[] stringInput;
    int[] intInput;

    [ContextMenu("Start")]
    void Start() {
        string filePath = "Day" + day + "/" + "Day" + day;
        switch (file) {
            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;
            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;
        }
        if (convertInputToInt)
            intInput = AdventHelper.GetIntInput(stringInput);

        switch (part) {
            case Part.Part1: Part1(); break;
            case Part.Part2: Part2(); break;
        }
        print("");
    }
    void Part1() {
        print("Part 1");

        // Nothing fancy here, just parsing the commands and moving the initial
        // position by the amount.

        (int x, int y) position = (0, 0);
        foreach (string s in stringInput) {
            string[] commands = s.Split(' ');
            int amount = int.Parse(commands[1]);
            switch (commands[0]) {
                case "forward": position.x += amount; break;
                case "up": position.y -= amount; break;
                case "down": position.y += amount; break;
            }
        }

        print(position.x * position.y);
    }
    void Part2() {
        print("Part 2");

        // Part 2 changed it up a little, but not by much.
        // position.y += amount * aim; was a little tricky and took a few tries to get to,
        // I just had to make sure I was understanding the instructions correctly.
        // Still only took about 10 minutes between Part 1 and 2.

        (int x, int y) position = (0, 0);
        int aim = 0;
        foreach (string s in stringInput) {
            string[] commands = s.Split(' ');
            int amount = int.Parse(commands[1]);
            switch (commands[0]) {
                case "forward": position.x += amount; position.y += amount * aim; break;
                case "up": aim -= amount; break;
                case "down": aim += amount; break;
            }
        }

        print(position.x * position.y);
    }
}

