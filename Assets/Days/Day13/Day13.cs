using System.Collections;

public class Day13 : MonoBehaviour {
    public int day = 13;
    public Part part;
        
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
        }
            if (b)
                dots++;
        }

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

        Dictionary<string, bool> coordinates = new Dictionary<string, bool>();
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
        }
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

        Texture2D texture = new Texture2D(size[0], size[1]);
            for (int i = 0; i < 2; i++)
                d[i] -= negativeOffset[i];
            texture.SetPixel(d[0], d[1], Color.black);
        }
        image.rectTransform.sizeDelta = new Vector2(size[0] * 10, size[1] * 10);
    }