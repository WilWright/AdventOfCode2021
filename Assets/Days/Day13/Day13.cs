using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;

public class Day13 : MonoBehaviour {
    public int day = 13;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    public RawImage image;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");
                // Another pretty fun challenge. Part 1 wasn't too hard to figure out, but I ran into some implementation problems.        // Since we were always folding the paper up or left (towards (0, 0)), I could get an offset from the fold to the current dot,        // and then get its new position on the other side of the fold. If the offset was >= 0 then that means the dot was on the half        // that wasn't getting folded, so I could skip it.        // I had pretty much all of this logic down within the first 30 minutes, but I was getting wrong answers.        // Initially I used an int[] in the coordinates dictionary that held the x and y values.        // I didn't realize for a while that when I changed the values in the int[] to the new coordinates that the hashkey didn't change.        // So I changed the key from an int[] to a string, which was "x,y" as it is in the input, and then converted it inbetween plots and back.        Dictionary<string, bool> coordinates = new Dictionary<string, bool>();        List<string[]> instructions = new List<string[]>();        foreach (string s in stringInput) {
            if (s[0] == 'f')
                instructions.Add(AdventHelper.ParseString(s, false, '=')[2].Split('='));
            else
                coordinates.Add(s, true);
        }

        foreach (string[] s in instructions) {
            int axis = s[0] == "x" ? 0 : 1;
            int fold = int.Parse(s[1]);

            List<string> keys = new List<string>();
            foreach (string ss in coordinates.Keys)
                keys.Add(ss);

            foreach (string ss in keys) {
                try { if (!coordinates[ss]) continue; } catch { }

                string[] cc = ss.Split(',');
                int[] c = new int[] { int.Parse(cc[0]), int.Parse(cc[1]) };
                int offset = fold - c[axis];
                if (offset >= 0)
                    continue;
                
                coordinates[ss] = false;
                c[axis] = fold + offset;
                string newKey = c[0] + "," + c[1];
                try { coordinates[newKey] = true; }
                catch { coordinates.Add(newKey, true); }
            }
            break;
        }        int dots = 0;        foreach (bool b in coordinates.Values) {
            if (b)
                dots++;
        }        print(dots);    }    void Part2() {        print("Part 2");

        // Part 2 had a couple of hurdles. I was a little confused by the problem initally,
        // but eventually figured out I would need to map out all the dots and read them.
        // My first thought was to go through the coordinates and put them in my GridSystem, 
        // so that I could then go through the grid in order and print out each element.

        // The first problem I ran into was IndexOutOfRange errors. After some debugging,
        // I realized I was getting some negative coordinates from the main input.
        // Sometimes when folding the dots, they would end up "off the page".
        // This didn't matter when they were keys in a dictionary, but they wouldn't work in an array based grid. 
        // So I then went through all of the coordinates and got the lowest x and y values as an offset.

        // After that was fixed I was ready to print the answer... but it was way too long horizontally to fit in the console.
        // I even tried popping it out and stretching it across my 2 monitors, but that also wasn't enough.
        // Luckily I've had experience creating sprites by pixel in my game Wilbur's Quest for various things,
        // like the branching map system and sprite tilings. So the next step was to set up a RawImage in the scene and draw on it.

        // The image was really big, and seemingly still full of random dots. After scouring its contents for a bit,
        // I eventually found the 8 character code made by tiny 3x6 letters at the bottom: BCZRCEAB

        // Thinking about this challenge after the fact, I realized the reason the image was so big and cluttered with other dots
        // was because I wasn't checking if a dot was present in the current list of keys before folding it,
        // even though I marked it false in the previous step. Instead of being discarded, each fold was converting the pre-folded dots.
        // This is also why I was getting negative coordinates. I went back and fixed the dot check in lines 64 and 135, but kept the negatives fix
        // even though it's made redundant now.

        Dictionary<string, bool> coordinates = new Dictionary<string, bool>();        List<string[]> instructions = new List<string[]>();        foreach (string s in stringInput) {
            if (s[0] == 'f')
                instructions.Add(AdventHelper.ParseString(s, false, '=')[2].Split('='));
            else
                coordinates.Add(s, true);
        }

        foreach (string[] s in instructions) {
            int axis = s[0] == "x" ? 0 : 1;
            int fold = int.Parse(s[1]);

            List<string> keys = new List<string>();
            foreach (string ss in coordinates.Keys)
                keys.Add(ss);

            foreach (string ss in keys) {
                try { if (!coordinates[ss]) continue; } catch { }

                string[] cc = ss.Split(',');
                int[] c = new int[] { int.Parse(cc[0]), int.Parse(cc[1]) };
                int offset = fold - c[axis];
                if (offset >= 0)
                    continue;
                
                coordinates[ss] = false;
                c[axis] = fold + offset;
                string newKey = c[0] + "," + c[1];
                try { coordinates[newKey] = true; }
                catch { coordinates.Add(newKey, true); }
            }
        }        List<int[]> dots = new List<int[]>();        int[] negativeOffset = new int[2];        int[] size = new int[2];        foreach (var kvp in coordinates) {
            if (kvp.Value) {
                string[] cc = kvp.Key.Split(',');
                int[] c = new int[] { int.Parse(cc[0]), int.Parse(cc[1]) };
                dots.Add(c);

                for (int i = 0; i < 2; i++) {
                    if (c[i] < negativeOffset[i])
                        negativeOffset[i] = c[i];
                    else {
                        if (c[i] > size[i])
                            size[i] = c[i];
                    }
                }
            }
        }
        for (int i = 0; i < 2; i++)
            size[i] = size[i] - negativeOffset[i] + 1;

        Texture2D texture = new Texture2D(size[0], size[1]);        foreach (int[] d in dots) {
            for (int i = 0; i < 2; i++)
                d[i] -= negativeOffset[i];
            texture.SetPixel(d[0], d[1], Color.black);
        }        texture.Apply();        texture.filterMode = FilterMode.Point;        image.texture = texture;
        image.rectTransform.sizeDelta = new Vector2(size[0] * 10, size[1] * 10);
    }}