using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day10 : MonoBehaviour {
    public int day = 10;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    char[] opens  = new char[] { '(', '[', '{', '<' };    char[] closes = new char[] { ')', ']', '}', '>' };    int[] errorScores = new int[] { 3, 57, 1197, 25137 };    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This challenge wasn't too hard to understand. I knew from the start mostly what I needed to do.
        // I had a slight hiccup though, because I initially was keeping track of how many of each symbol was opened,
        // and subtracting from that when it was closed. If the count for that symbol was ever < 0 that meant it was corrupted,
        // closing before it had an opening symbol. I realized this wasn't going to cut it, because I wouldn't know what the previous
        // opening symbol was.

        // I transitioned to keeping a list of the current open symbols and removing them when they were closed.
        // There was some annoying logic I had to implement which lost me some time, like checking the count 
        // and worrying about the order I had to insert in. I should have used something like a Stack instead,
        // but in the moment I didn't think of it and was racing for the answer.

        int score = 0;
        foreach (string s in stringInput) {
            List<int> charList = new List<int>();
            foreach (char c in s) {
                bool corrupted = false;
                for (int j = 0; j < opens.Length; j++) {
                    if (c == opens[j]) {
                        if (charList.Count > 0)
                            charList.Insert(0, j);
                        else
                            charList.Add(j);
                        break;
                    }
                    if (c == closes[j]) {
                        if (charList.Count > 0 && charList[0] == j)
                            charList.RemoveAt(0);
                        else {
                            corrupted = true;
                            score += errorScores[j];
                        }
                        break;
                    }
                }
                if (corrupted)
                    break;
            }
        }        print(score);    }    void Part2() {        print("Part 2");

        // Fixing all the problems in Part 1 made me take 33 minutes, but after all that logic was in place,
        // finishing Part 2 was easy, only another 10 minutes. I just had to change the scoring calculation. I got wrong answers,
        // but after having similar issues in previous days, I quickly caught on to the fact I needed 
        // to use a long instead of an int. Overall I was happy with this one, with a rank of 5,624 for Part 2.

        List<long> scores = new List<long>();
        foreach (string s in stringInput) {
            long score = 0;
            List<int> charList = new List<int>();
            bool corrupted = false;
            foreach (char c in s) {
                for (int j = 0; j < opens.Length; j++) {
                    if (c == opens[j]) {
                        if (charList.Count > 0)
                            charList.Insert(0, j);
                        else
                            charList.Add(j);
                        break;
                    }
                    if (c == closes[j]) {
                        if (charList.Count > 0 && charList[0] == j)
                            charList.RemoveAt(0);
                        else
                            corrupted = true;
                        break;
                    }
                }
                if (corrupted)
                    break;
            }
            if (!corrupted) {
                foreach (int index in charList) {
                    score *= 5;
                    score += index + 1;
                }
                scores.Add(score);
            }
        }        long[] sorted = scores.ToArray();        System.Array.Sort(sorted);        print(sorted[sorted.Length / 2]);    }}