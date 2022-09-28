using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day7 : MonoBehaviour {
    public int day = 7;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class CrabGroup {
        public int count = 1;
        public int fuel = 0;
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        stringInput = stringInput[0].Split(',');        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // This challenge was pretty simple. After learning my lesson yesterday about long computation times,        // I grouped the positions together so if there were any that occurred more than once, I just increased its count.        // Then I just had to calculate the fuel count for that position against the rest of the positions and multiply it by the count.        Dictionary<int, CrabGroup> positions = new Dictionary<int, CrabGroup>();        foreach (int i in intInput) {
            try { positions[i].count++; }
            catch { positions.Add(i, new CrabGroup()); }
        }        foreach (var kvp in positions) {
            CrabGroup cg = kvp.Value;
            int pos = kvp.Key;

            foreach (var kvp2 in positions) {
                CrabGroup cg2 = kvp2.Value;
                if (cg == cg2)
                    continue;

                int pos2 = kvp2.Key;
                cg.fuel += Mathf.Abs(pos - pos2) * cg2.count;
            }
        }        CrabGroup lowest = null;        foreach (var kvp in positions) {
            if (lowest == null || kvp.Value.fuel < lowest.fuel)
                lowest = kvp.Value;
        }        print(lowest.fuel);    }    void Part2() {        print("Part 2");

        // For Part 2 I quickly understood I needed to do the same thing, but instead loop through the amount of moves. 
        // So instead of adding to fuel by 1, it was 1 -> Moves incrementally.

        // I became very confused when my answer was wrong and started debugging.
        // My answer was only off by a little bit, but some of the other fuel counts DID match the examples. 
        // After some more headaches and debugging, changing values to make sure I wasn't off by one somewhere,
        // trying different calculations, etc, I read through the whole challenge again and realized I had misinterpreted what it wanted.

        // For all of the challenges I try to visualize how their scenarios play out to better understand them.
        // So in my head I imagined the crabs in their initial positions, and then choosing one of the other crabs' positions to move to, using the least fuel.
        // The initial example had positions of 16, 1, 2, 0, 4, 2, 7, 1, 2, 14, with a correct position of 2, but when I unlocked Part 2 I realized they gave a new correct position of 5.
        // I had skimmed over this, because the correct position for Part 1 just happened to be in the list of inital positions.
        // This had confirmed the original image I had in my head, and I failed to realize the final position could be anywhere between 0 and 16,
        // and not only an initial position (0, 1, 2, 4, 7, 14, 16). Even though my logic was wrong, I had no issues with Part 1 because my correct position ALSO
        // happened to be an initial position from my given input (not as hard to get lucky because there were many times more positions in my input (hundreds) vs. the example (only 7)).

        // For Part 2 however the position I needed to find was NOT included in the initial positions and I had to keep a fuel count for each. I knew at this point instead of looping over the initial positions,
        // I needed to loop over all possible positions. In another attempt to avoid long computation times like yesterday, I didn't want to just loop from 0 to an arbitrary number like 10,000.
        // I figured out that the correct position had to be within the range of the initial ones, because if it was less than the lowest or more than the highest, 
        // the fuel cost would definitely be more than another candidate, so I just sorted out the smallest and largest values and looped between those.

        // I was pretty disappointed with this one, because I would have had the logic all down very quickly. I should definitely scrutinize the challenges more carefully,
        // but I kind of think this one should have been more explicit in regards to this criterion.

        Dictionary<int, CrabGroup> positions = new Dictionary<int, CrabGroup>();        (int smallest, int largest) values = (intInput[0], intInput[0]);        foreach (int i in intInput) {
            try { positions[i].count++; }
            catch { positions.Add(i, new CrabGroup()); }

            values = (Mathf.Min(i, values.smallest), Mathf.Max(i, values.largest));
        }
        
        int[] fuels = new int[values.largest - values.smallest + 1];
        for (int i = values.smallest; i <= values.largest; i++) {
            foreach (var kvp in positions) {
                int pos = kvp.Key;
                if (pos == i)
                    continue;
                
                CrabGroup cg = kvp.Value;
                int moves = Mathf.Abs(pos - i);
                for (int j = 1; j <= moves; j++)
                    fuels[i - values.smallest] += j * cg.count;
            }
        }        int lowest = fuels[0];        foreach (int i in fuels) {
            if (i < lowest)
                lowest = i;
        }        print(lowest);    }}