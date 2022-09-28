using System.Collections;
    public int day = 14;
    public Part part;

        // This challenge was a tricky one. I understood the problem pretty well, but getting the logic down was hard.
        // The solution I eventually came up with was to do the problem literally.
        // I would search the template string character by character and find the pairs.
        // If one was found, I took note of it and the index it should be inserted in at.
        // At the end of each search I built a new template by going through both the current template and new elements in order.

        string[][] input = AdventHelper.ParseInput(stringInput);
            inputs.Add(AdventHelper.ParseString(s));
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
        (string element, int count) least = (template[0] + "", elementCounts[template[0] + ""]);
            if (kvp.Value > most.count) {
                most.element = kvp.Key;
                most.count = kvp.Value;
                continue;
            }
            if (kvp.Value < least.count) {
                least.element = kvp.Key;
                least.count = kvp.Value;
            }
        }

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

        string[][] input = AdventHelper.ParseInput(stringInput);
            inputs.Add(AdventHelper.ParseString(s));
            string key = template[i] + "" + template[i + 1] + "";
            try { pairCounts[key]++; }
            catch { pairCounts.Add(key, 1); };
        }
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
        (string element, long count) least = (template[0] + "", elementCounts[template[0] + ""]);
            if (kvp.Value > most.count) {
                most.element = kvp.Key;
                most.count = kvp.Value;
                continue;
            }
            if (kvp.Value < least.count) {
                least.element = kvp.Key;
                least.count = kvp.Value;
            }
        }
    }