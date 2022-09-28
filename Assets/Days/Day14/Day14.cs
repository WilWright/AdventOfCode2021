using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day14 : MonoBehaviour {
    public int day = 14;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This challenge was a tricky one. I understood the problem pretty well, but getting the logic down was hard.
        // The solution I eventually came up with was to do the problem literally.
        // I would search the template string character by character and find the pairs.
        // If one was found, I took note of it and the index it should be inserted in at.
        // At the end of each search I built a new template by going through both the current template and new elements in order.

        string[][] input = AdventHelper.ParseInput(stringInput);        List<string[]> inputs = new List<string[]>();        Dictionary<string, int> elementCounts = new Dictionary<string, int>();        foreach (string s in stringInput)
            inputs.Add(AdventHelper.ParseString(s));        string template = inputs[0][0];        inputs.RemoveAt(0);        int steps = 10;        for (int step = 0; step < steps; step++) {
            List<(int index, string element)> newElements = new List<(int index, string element)>();
            foreach (string[] s in inputs) {
                if (step == 0) {
                    try { elementCounts.Add(s[1], 1); }
                    catch { }
                }
                for (int i = 0; i < template.Length - 1; i++) {
                    if (s[0][0] == template[i] && s[0][1] == template[i + 1]) {
                        elementCounts[s[1]]++;
                        newElements.Add((i, s[1]));
                    }
                }
            }
            string newTemplate = "";
            for (int i = 0; i < template.Length; i++) {
                newTemplate += template[i];
                foreach (var ne in newElements) {
                    if (ne.index == i)
                        newTemplate += ne.element;
                }
            }
            template = newTemplate;
        }

        (string element, int count) most = (template[0] + "", elementCounts[template[0] + ""]);
        (string element, int count) least = (template[0] + "", elementCounts[template[0] + ""]);        foreach (var kvp in elementCounts) {
            if (kvp.Value > most.count) {
                most.element = kvp.Key;
                most.count = kvp.Value;
                continue;
            }
            if (kvp.Value < least.count) {
                least.element = kvp.Key;
                least.count = kvp.Value;
            }
        }        print(most.count - least.count + 1);    }    void Part2() {        print("Part 2");

        // The solution for Part 1 took a long time to calculate, so I knew that going from a step count of 10 to 40
        // would be incredibly slow, and I would need to take a new approach. After a bit of thinking I realized this challenge
        // was similar to Day 6, the lanternfish one, where they would exponentially grow in population.

        // So in the same way I went from tracking individual fish to groups of fish with the same spawn rate,
        // I could keep track of how many pairs of certain elements I have instead of searching for them through an ever growing string.
        // Starting with the example template NNCB, I would have pairs NN:1, NC:1, CB:1. The first insertion rule that would happen is CB -> H. 
        // Since CB is being split essentially, I subtract however many there are, because that amount of pairs currently no longer exists.
        // Then I make the new pairs CH and HB (from NNCHB). So after the first insertion I now have NN:1, NC:1, CB:0, CH:1, HB: 1.
        // With each instruction I essentially multiply it by how many of those pairs there are.

        // It took a long time to really nail this down, I thought my rank wouldn't be that great, but it actually improved!
        // I went from 6,756 @ 43 minutes to 4,886 @ 1 hour and 40 minutes.

        string[][] input = AdventHelper.ParseInput(stringInput);        List<string[]> inputs = new List<string[]>();        Dictionary<string, long> elementCounts = new Dictionary<string, long>();        Dictionary<string, long> pairCounts = new Dictionary<string, long>();        foreach (string s in stringInput)
            inputs.Add(AdventHelper.ParseString(s));        string template = inputs[0][0];        inputs.RemoveAt(0);        for (int i = 0; i < template.Length - 1; i++) {
            string key = template[i] + "" + template[i + 1] + "";
            try { pairCounts[key]++; }
            catch { pairCounts.Add(key, 1); };
        }        int steps = 40;        for (int step = 0; step < steps; step++) {
            List<(string name, long count)> pairs = new List<(string name, long count)>();
            foreach (var kvp in pairCounts) {
                if (kvp.Value > 0)
                    pairs.Add((kvp.Key, kvp.Value));
            }
            foreach (string[] s in inputs) {
                if (step == 0) {
                    try { elementCounts.Add(s[1], 1); }
                    catch { }
                }

                foreach (var p in pairs) {
                    if (s[0] == p.name) {
                        pairCounts[p.name] -= p.count;
                        long count = p.count;

                        string key1 = p.name[0] + s[1];
                        try { pairCounts[key1] += count; }
                        catch { pairCounts.Add(key1, count); };

                        string key2 = s[1] + p.name[1];
                        try { pairCounts[key2] += count; }
                        catch { pairCounts.Add(key2, count); };

                        elementCounts[s[1]] += count;
                    }
                }
            }
        }

        (string element, long count) most = (template[0] + "", elementCounts[template[0] + ""]);
        (string element, long count) least = (template[0] + "", elementCounts[template[0] + ""]);        foreach (var kvp in elementCounts) {
            if (kvp.Value > most.count) {
                most.element = kvp.Key;
                most.count = kvp.Value;
                continue;
            }
            if (kvp.Value < least.count) {
                least.element = kvp.Key;
                least.count = kvp.Value;
            }
        }        print(most.count - least.count + 1);
    }}