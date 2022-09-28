using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day18 : MonoBehaviour {
    public int day = 18;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class SnailFishNumber {
        public class Pair {
            public Pair parent;
            public Pair[] children;
            public int num = -1;

            public Pair(Pair parent) {
                this.parent = parent;
            }
        }

        public Pair root;
        public List<Pair> leaves;
        public List<Pair[]> leafPairs;

        public SnailFishNumber(string input) {
            input = input.Substring(1, input.Length - 2);
            root = new Pair(null);
            Pair current = root;
            bool goRight = false;
            foreach (char c in input) {
                if (c == ',') {
                    goRight = true;
                    continue;
                }

                if (current.children == null)
                    current.children = new Pair[] { new Pair(current), new Pair(current) };

                switch (c) {
                    case '[': current = current.children[goRight ? 1 : 0]; break;
                    case ']': current = current.parent; break;
                    default: current.children[goRight ? 1 : 0].num = int.Parse(c + ""); break;
                }

                goRight = false;
            }
        }

        public void Add(SnailFishNumber snailFishNumber) {
            Pair newRoot = new Pair(null);
            root.parent = newRoot;
            snailFishNumber.root.parent = newRoot;
            newRoot.children = new Pair[] { root, snailFishNumber.root };
            root = newRoot;
        }

        public int GetMagnitude() {
            while (leafPairs.Count > 0) {
                foreach (Pair[] p in leafPairs) {
                    p[0].parent.num = p[0].num * 3 + p[1].num * 2;
                    p[0].parent.children = null;
                    p[0].parent = p[1].parent = null;
                }
                UpdateLeafPairs();
            }
            return root.num;
        }

        public void Evaluate() {
            UpdateLeafPairs();
            foreach (Pair[] p in leafPairs) {
                int depth = 0;
                Pair parent = p[0].parent;
                while (parent != null) {
                    depth++;
                    parent = parent.parent;
                }

                if (depth == 5) {
                    Explode(p);
                    return;
                }
            }

            foreach (Pair p in leaves) {
                if (p.num > 9) {
                    Split(p);
                    break;
                }
            }
        }

        public void Explode(Pair[] leafPair) {
            Pair left = GetNextLeaf(leafPair[0], -1);
            if (left != null)
                left.num += leafPair[0].num;
            Pair right = GetNextLeaf(leafPair[1], 1);
            if (right != null)
                right.num += leafPair[1].num;

            leafPair[0].parent.num = 0;
            leafPair[0].parent.children = null;

            Evaluate();
        }

        public void Split(Pair leaf) {
            leaf.children = new Pair[] { new Pair(leaf), new Pair(leaf) };
            int half = leaf.num / 2;
            leaf.children[0].num = half;
            leaf.children[1].num = half + leaf.num % 2;
            leaf.num = -1;

            Evaluate();
        }

        public Pair GetNextLeaf(Pair start, int index) {
            if (start.num == -1)
                return null;

            for (int i = 0; i < leaves.Count; i++) {
                if (leaves[i] == start) {
                    try { return leaves[i + index]; }
                    catch { return null; }
                }
            }
            return null;
        }

        public void UpdateLeaves() {
            leaves = new List<Pair>();
            GetNext(root);
            void GetNext(Pair p) {
                if (p.children != null) {
                    for (int i = 0; i < p.children.Length; i++) {
                        if (p.children[i].children != null)
                            GetNext(p.children[i]);
                        else
                            leaves.Add(p.children[i]);
                    }
                }
            }
        }

        public void UpdateLeafPairs() {
            UpdateLeaves();
            leafPairs = new List<Pair[]>();
            for (int i = 0; i < leaves.Count - 1; i++) {
                Pair parent = leaves[i].parent;
                if (parent.children[0].num != -1 && parent.children[1].num != -1) {
                    leafPairs.Add(parent.children);
                    i++;
                }
            }
        }

        public string GetLeaves() {
            string s = "";
            foreach (Pair p in leaves)
                s += p.num + ", ";
            return s;
        }
        public string GetPairs() {
            string s = "";
            foreach (Pair[] p in leafPairs) {
                s += "[" + p[0].num + ", " + p[1].num + "]";
            }
            return s;
        }
        public string GetNumber() {
            string s = "";
            foreach (Pair p in leaves) {
                Pair parent = p;
                while (parent.parent != null && parent == parent.parent.children[0]) {
                    s += "[";
                    parent = parent.parent;
                }

                s += p.num;
                parent = p;
                
                while (parent.parent != null && parent == parent.parent.children[1]) {
                    s += "]";
                    parent = parent.parent;
                }

                if (p != leaves[leaves.Count - 1])
                    s += ",";
            }
            return s;
        }    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This was the toughest challenge so far. Like Day 16 this one had a lot of information to unpack.
        // I knew the basis of the solution would be building a binary tree and doing operations on it,
        // but I had no idea where to start. I didn't even begin writing code until about an hour in.
        // My first big hurdle was figuring out how to parse the input to get the right pairs and depth levels.

        // I started out with the Pair class, which would contain its parent, and either a number or its children (2 other Pairs).
        // Then I opened up good ol' MsPaint and drew out the tree by hand for the input. This helped me visualize how I could logically
        // combine the pairs from left to right in the correct order. After another hour I was able to parse the input successfully.
        // I noticed that whenever a comma was present that meant the next element would be on the right side of the branch, whether it was a number or pair.
        // I would also go up or down in depth depending on which bracket type was next.

        // After getting that down I had to think about how I would do the Explode() and Split() logic. Since the numbers had to move left and right,
        // I figured it was best to put the tree back in a form that looked more like the input so I could easily get the numbers that were next to each other.
        // I would need to gather all the leaves in order, AKA a tree traversal. This is one of the only times I searched for help online, because I didn't really know
        // how to do this. From the quick search I was able to get the logic for UpdateLeaves(), and then from there I got UpdateLeafPairs() which allowed me to see
        // pairs that were strictly 2 numbers rather than 2 pairs or a number and a pair. With these two collections of pairs I could easily implement the Explode() and Split() logic.

        // About 2.5 hours in now I was running into some more problems. My code was working for some of the examples, but not all of them.
        // I went through a ton of debugging and manually worked through the examples by hand, but I just couldn't figure out what was wrong.
        // I was able to print out the flattened list of leaves, as well as the pairs, but to truly debug properly I knew I would need to
        // print out the tree in the form of the input, with all the correct brackets and commas included. I couldn't think of an easy way to do this,
        // so I kept on going through examples by hand. About another 2 hours of this passed and I wasn't really getting anywhere, and also very tired.
        // At this point I decide to sleep on it and try again the next day, which is something I also haven't had to do yet until this challenge.

        // Next day:
        // I started by going through the problem again from top to bottom with a fresh mind. Going over my logic I still can't seem to find what the issue is.
        // I decided to work through how to print out the tree in a proper format, and now that I wasn't so tired it didn't take that long to figure out.
        // I simply had to go up the tree from each leaf while sticking on the same side to determine if I should insert a '[' or ']'.
        // With this now working I could immediately see the problem. There weren't any issues with my evalutaion logic, but the initial adding of two snailfish numbers.
        // When putting the roots of each number as a pair under a new root, I forgot to set their parent to the new root, so each one was being treated separately as two whole trees.

        // With this fixed I could finally get to what the problem wanted from me, which was to calculate the magnitude using the pairs in the tree.
        // The systems in place made this easy, I just had to send the numbers up through the parents and recalculate the leaves each step. 
        // It only took about 30 minutes in total to complete after starting Part 1 back up, and then I could move on to Part 2.

        List<SnailFishNumber> numbers = new List<SnailFishNumber>();        foreach (string s in stringInput)            numbers.Add(new SnailFishNumber(s));

        SnailFishNumber sfn = null;
        if (numbers.Count > 1) {
            for (int i = 0; i < numbers.Count - 1; i++) {
                sfn = numbers[i];
                sfn.Add(numbers[i + 1]);
                sfn.Evaluate();
                numbers[i + 1] = sfn;
            }
        }
        else {
            sfn = numbers[0];
            sfn.Evaluate();
        }

        print(sfn.GetMagnitude());
    }    void Part2() {        print("Part 2");        // Part 2 only took 6 minutes to finish. I had all the tools I needed and just had to add each combination of        // snailfish numbers together and get the largest magnitude. I was surprised my rank didn't end up being        // that bad (11,608), despite finishing 18 hours after the challenge opened up. This must have been a tough one        // for a lot of other participants as well.        // I'm a little disappointed in myself for not catching my mistakes the night before, but I think the end        // result turned out well. However, thinking back on the problem I came up with a different solution that would have been        // possibly much easier. I didn't even need this whole tree system, I could just read the string from left to right        // and insert/modify numbers where needed, along with new brackets. I'm not sure if calculating the magnitude would be        // a lot more tricky without the paired parent-child connections, but I think overcoming that hurdle would have been easier        // than the ones I had to overcome with my initial solution.        int largestMagnitude = -1;        foreach (string s in stringInput) {
            foreach (string ss in stringInput) {
                if (s == ss)
                    continue;

                SnailFishNumber sfn = new SnailFishNumber(s);
                sfn.Add(new SnailFishNumber(ss));
                sfn.Evaluate();
                int magnitude = sfn.GetMagnitude();
                if (magnitude > largestMagnitude)
                    largestMagnitude = magnitude;
            }
        }        print(largestMagnitude);    }}