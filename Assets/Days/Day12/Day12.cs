using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day12 : MonoBehaviour {
    public int day = 12;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class Cave {
        public List<string> caves = new List<string>();
        public bool visited;
        public bool twice;

        public Cave() { }
        public Cave(Cave p) {
            twice = p.twice;
            visited = p.visited;
            foreach (string s in p.caves)
                caves.Add(s);
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This challenge was really fun. Within the first 10 minutes I had a good idea of what the logic
        // should look like, but actually writing it down and making sure it worked correctly was tricky (took 1 hour and 30 minutes).
        // I kept running into infinite loops, so it was hard to debug where I was missing data, or had multiple copies of the same data.

        // My initial idea was that going down each path was like making a series of choices,
        // where each choice can "split the timeline". So to get the right number of paths I had to branch out the "timeline"
        // at every cave and evaluate its next choices.

        // The way I implemented this was by starting with connecting all the caves from the input.
        // Each cave had its own key in the dictionary, which contained all the other caves it was connected to
        // and if it has been visited. When a cave is selected to be traversed it gets marked visited,
        // and then goes through each connected cave to traverse them as well. Each time a connection is traversed though,
        // a copy of the current cave network (dictionary) is made, with each of its visits intact, to be sent down the new branch.
        // If a cave has already been visited, that branch ceases to continue. A path becomes valid only when it has reached "end".

        Dictionary<string, Cave> caveNodes = new Dictionary<string, Cave>();        foreach (string[] s in AdventHelper.ParseInput(stringInput)) {
            if (!caveNodes.ContainsKey(s[0]))
                caveNodes.Add(s[0], new Cave());
            caveNodes[s[0]].caves.Add(s[1]);

            if (!caveNodes.ContainsKey(s[1]))
                caveNodes.Add(s[1], new Cave());
            caveNodes[s[1]].caves.Add(s[0]);
        }
        
        int pathCount = 0;
        Traverse("start", caveNodes);        void Traverse(string cave, Dictionary<string, Cave> branch) {
            Cave c = branch[cave];

            if (c.visited)
                return;

            if (cave[0] >= 'a')
                c.visited = true;

            foreach (string s in c.caves) {
                if (s == "end") {
                    pathCount++;
                    continue;
                }

                Dictionary<string, Cave> branchPaths = new Dictionary<string, Cave>();
                foreach (var kvp in branch)
                    branchPaths.Add(kvp.Key, new Cave(kvp.Value));

                Traverse(s, branchPaths);
            }
        }        print(pathCount);    }    void Part2() {        print("Part 2");

        // Part 2 was mostly the same, with the stipulation that any small cave can now be visited twice,
        // but once that happens it can't happen again in the same path.
        // I was overcomplicating the problem at first and thinking about completely rewriting the visit logic,
        // or wrapping the dictionary with data to check for a twice visit, but eventually realized I could
        // just add a check in the Cave class. Once one of the caves was visited twice, 
        // I could mark them all twice and check that in combination with their visit states.         Dictionary<string, Cave> caveNodes = new Dictionary<string, Cave>();        foreach (string[] s in AdventHelper.ParseInput(stringInput)) {
            if (!caveNodes.ContainsKey(s[0]))
                caveNodes.Add(s[0], new Cave());
            caveNodes[s[0]].caves.Add(s[1]);

            if (!caveNodes.ContainsKey(s[1]))
                caveNodes.Add(s[1], new Cave());
            caveNodes[s[1]].caves.Add(s[0]);
        }
        
        int pathCount = 0;
        Traverse("start", caveNodes);        void Traverse(string cave, Dictionary<string, Cave> branch) {
            Cave c = branch[cave];

            if (c.visited) {
                if (c.twice || cave == "start")
                    return;
                else {
                    foreach (var kvp in branch)
                        kvp.Value.twice = true;
                }
            }

            if (cave[0] >= 'a')
                c.visited = true;

            foreach (string s in c.caves) {
                if (s == "end") {
                    pathCount++;
                    continue;
                }

                Dictionary<string, Cave> branchNodes = new Dictionary<string, Cave>();
                foreach (var kvp in branch)
                    branchNodes.Add(kvp.Key, new Cave(kvp.Value));

                Traverse(s, branchNodes);
            }
        }        print(pathCount);    }}