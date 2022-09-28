using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day11 : MonoBehaviour {
    public int day = 11;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // It was a good thing I brought over my Coordinates struct and a modified grid system        // from my game after yesterday. I got a new best rank (2,099) for Part 1!        // I was really surprised by this, because it took me a little bit to really understand how the challenge worked.        // I was initially confused about how to deal with individual numbers when an octopus flashed.        // I wasn't sure if they reset immediately or after the step. After looking over the examples more I realized        // I should be going strictly by each step, and it didn't matter if energy levels continued to go over 10.                // With my new tools in place the logic was fairly simple, I just had to add diagonals into the Coordinates struct.        // Even though I had a slower start, I think these premade tools really helped me get ahead of a lot of other participants.        GridSystem<int> octopi = new GridSystem<int>(10, 10);
        for (int y = octopi.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < octopi.Width(); x++)                octopi.Set(new Coordinates(x, y), int.Parse(stringInput[octopi.Height() - 1 - y][x] + ""));        }        int flashes = 0;        int steps = 100;        for (int step = 0; step < steps; step++) {
            foreach (Coordinates c in octopi.coordinates)
                Increase(c);
            foreach (Coordinates c in octopi.coordinates)
                CheckReset(c);

            bool all = true;
            foreach (Coordinates c in octopi.coordinates) {
                if (octopi.Get(c) != 0) {
                    all = false;
                    break;
                }
            }
            if (all)
                print(step);
        }        void Increase(Coordinates c) {
            if (!octopi.WithinBounds(c))
                return;

            int energy = octopi.Get(c);
            energy++;
            octopi.Set(c, energy);

            if (energy == 10) {
                flashes++;
                foreach (Coordinates d in Coordinates.AllDirection)
                    Increase(c + d);
            }
        }
        void CheckReset(Coordinates c) {
            if (octopi.Get(c) > 9)
                octopi.Set(c, 0);
        }        print(flashes);    }    void Part2() {        print("Part 2");        // Part 2 went very well too, It only took another 5 minutes.        // With most of my logic already in place I just had to add an extra check after Part 1's logic to see        // if everything was 0.        GridSystem<int> octopi = new GridSystem<int>(10, 10);
        for (int y = octopi.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < octopi.Width(); x++)                octopi.Set(new Coordinates(x, y), int.Parse(stringInput[octopi.Height() - 1 - y][x] + ""));        }                bool all = false;        for (int step = 0; !all; step++) {
            foreach (Coordinates c in octopi.coordinates)
                Increase(c);
            foreach (Coordinates c in octopi.coordinates)
                CheckReset(c);

            all = true;
            foreach (Coordinates c in octopi.coordinates) {
                if (octopi.Get(c) != 0) {
                    all = false;
                    break;
                }
            }
            if (all)
                print(step + 1);
        }        void Increase(Coordinates c) {
            if (!octopi.WithinBounds(c))
                return;

            int energy = octopi.Get(c);
            energy++;
            octopi.Set(c, energy);

            if (energy == 10) {
                foreach (Coordinates d in Coordinates.AllDirection)
                    Increase(c + d);
            }
        }
        void CheckReset(Coordinates c) {
            if (octopi.Get(c) > 9)
                octopi.Set(c, 0);
        }    }}