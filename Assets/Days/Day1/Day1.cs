using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day1 : MonoBehaviour {
    public int day = 1;
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
        
        // For Day 1 I just got familiar with the process and
        // completed the simple challenge for Part 1, counting the number of increases
        // throughout the elements.

        int increaseCount = 0;
        for (int i = 1; i < intInput.Length; i++) {
            if (intInput[i] > intInput[i - 1])
                increaseCount++;
        }

        print(increaseCount);
    }
    void Part2() {
        print("Part 2");
        
        // Part 2 didn't take much longer. I used a nested loop to count 3 ahead
        // of each element and get the sums.

        List<int> sums = new List<int>();
        bool stop = false;
        for (int i = 0; i < intInput.Length; i++) {
            int sum = 0;
            for (int j = 0; j < 3; j++) {
                if (i + j >= intInput.Length) {
                    stop = true;
                    break;
                }
                sum += intInput[i + j];
            }
            if (stop)
                break;

            sums.Add(sum);
        }

        int increaseCount = 0;
        for (int i = 1; i < sums.Count; i++) {
            if (sums[i] > sums[i - 1])
                increaseCount++;
        }

        print(increaseCount);
    }
}


