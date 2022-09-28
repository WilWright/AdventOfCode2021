using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayTemplate : MonoBehaviour { //@
    public int day; //#
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



        //print();
    }
    void Part2() {
        print("Part 2");



        //print();
    }
}
