using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day25 : MonoBehaviour {
    public int day = 25;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class SeaCucumber {
        public Coordinates position;
        public Coordinates nextPosition;
        public Coordinates moveDirection;

        public SeaCucumber(Coordinates position, Coordinates moveDirection) {
            this.position = position;
            this.moveDirection = moveDirection;
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // For the last day, it seems like the challenge is pretty straightforward.        // The GridSystem was perfect for this problem, I just had to follow the rules of moving        // and updating.        GridSystem<string> grid = GridSystem<string>.GetStringGrid(stringInput);        GridSystem<SeaCucumber> cucumbers = new GridSystem<SeaCucumber>(grid.Width(), grid.Height());        List<SeaCucumber> east = new List<SeaCucumber>();        List<SeaCucumber> south = new List<SeaCucumber>();        foreach (Coordinates c in grid.coordinates) {
            switch (grid.Get(c)) {
                case ">":
                    cucumbers.Set(c, new SeaCucumber(c, Coordinates.Right));
                    east.Add(cucumbers.Get(c));
                    break;
                case "v":
                    cucumbers.Set(c, new SeaCucumber(c, Coordinates.Down));
                    south.Add(cucumbers.Get(c));
                    break;
            }
        }        bool stopped = false;        int steps = 0;        while (!stopped) {
            List<SeaCucumber> updateEast = new List<SeaCucumber>();
            foreach (SeaCucumber sc in east) {
                Coordinates next = sc.position + sc.moveDirection;
                if (!cucumbers.WithinBounds(next))
                    next = new Coordinates(0, next.y);
                if (cucumbers.Get(next) == null) {
                    sc.nextPosition = next;
                    updateEast.Add(sc);
                }
            }

            foreach (SeaCucumber sc in updateEast) {
                cucumbers.Set(sc.position, null);
                cucumbers.Set(sc.nextPosition, sc);
                sc.position = sc.nextPosition;
            }

            List<SeaCucumber> updateSouth = new List<SeaCucumber>();
            foreach (SeaCucumber sc in south) {
                Coordinates next = sc.position + sc.moveDirection;
                if (!cucumbers.WithinBounds(next))
                    next = new Coordinates(next.x, cucumbers.Height() - 1);
                if (cucumbers.Get(next) == null) {
                    sc.nextPosition = next;
                    updateSouth.Add(sc);
                }
            }

            foreach (SeaCucumber sc in updateSouth) {
                cucumbers.Set(sc.position, null);
                cucumbers.Set(sc.nextPosition, sc);
                sc.position = sc.nextPosition;
            }

            steps++;
            if (updateEast.Count == 0 && updateSouth.Count == 0) {
                stopped = true;
                break;
            }
        }        void PrintCucumbers() {
            for (int y = cucumbers.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < cucumbers.Width(); x++) {
                    Coordinates c = new Coordinates(x, y);
                    SeaCucumber sc = cucumbers.Get(c);
                    if (sc == null)
                        s += " .";
                    else {
                        if (sc.moveDirection == Coordinates.Right)
                            s += ">";
                        else
                            s += "v";
                    }
                }
                print(s);
            }
        }        print(steps);    }    void Part2() {        print("Part 2");        // No part two, need to get all previous stars to finish event.        // Missing Day 23 Part 2, and Day 24 Parts 1 and 2.        //print();    }}