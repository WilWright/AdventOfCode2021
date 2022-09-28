using System.Collections;
    public int day = 9;
    public Part part;

        // Before the challenge started I was working on the string parser to make life a lot easier, but had to work on it a little
        // more while doing Part 1, so I had a slower time than I would've liked. The contents of Part 1 were very familiar, though.
        // In my game Wilbur's Quest I do something very similar where I check adjacent blocks to see if they're the same type,
        // and if they need to be tiled together. Here instead I would just need to check integers rather than block types.

        // After getting wrong answers I had to do some debugging. I had gotten tripped up on (next < current) vs. (next <= current).
        // There were a couple indications in the problem of why it needed to be like this, but like on day 7, the image I built in my head
        // had worked perfectly fine for the example input, but there were cases where it didn't work for the main input. It seemed like there
        // were quite a few other participants that had this problem as well, so I also think this challenge could have used some more explicit instructions.

        (int x, int y)[] directions = new (int x, int y)[] { (0, 1), (0, -1), (-1, 0), (1, 0) };
            string[] s = AdventHelper.ParseString(stringInput[i]);
            stringInput[i] = s[0];
        }
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = int.Parse(stringInput[i][j] + "");
        }
            for (int y = 0; y < grid.GetLength(1); y++) {
                bool lowest = true;
                int current = grid[x, y];

                foreach (var d in directions) {
                    (int x, int y) n = (x + d.x, y + d.y);
                    if (WithinBounds(n) && grid[n.x, n.y] <= current)
                        lowest = false;
                }
                if (lowest)
                    sum += current + 1;
            }
        }
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < grid.GetLength(0) && coordinate.y < grid.GetLength(1);
        }

        print(sum);
            string[] s = AdventHelper.ParseString(stringInput[i]);
            stringInput[i] = s[0];
        }

        int[,] grid = new int[stringInput.Length, stringInput[0].Length];
            for (int j = 0; j < grid.GetLength(1); j++)
                grid[i, j] = int.Parse(stringInput[i][j] + "");
        }

        int[] largestBasins = new int[3];
            for (int y = 0; y < grid.GetLength(1); y++) {
                bool lowest = true;
                int current = grid[x, y];

                foreach (var d in directions) {
                    (int x, int y) n = (x + d.x, y + d.y);
                    if (WithinBounds(n) && grid[n.x, n.y] <= current)
                        lowest = false;
                }

                if (lowest) {
                    int size = SearchBasin((x, y));
                    int smallest = -1;
                    for (int i = 0; i < largestBasins.Length; i++) {
                        if (smallest == -1)
                            smallest = largestBasins[i];
                        smallest = Mathf.Min(smallest, largestBasins[i]);
                    }
                    if (size > smallest) {
                        for (int i = 0; i < largestBasins.Length; i++) {
                            if (largestBasins[i] == smallest) {
                                largestBasins[i] = size;
                                break;
                            }
                        }
                    }
                }
            }
        }
            return coordinate.x >= 0 && coordinate.y >= 0 && coordinate.x < grid.GetLength(0) && coordinate.y < grid.GetLength(1);
        }

        int SearchBasin((int x, int y) lowest) {
            List<(int x, int y)> check = new List<(int x, int y)>();
            check.Add(lowest);
            int index = 0;
            int size = 0;

            do {
                (int x, int y) current = check[index];
                size++;

                foreach (var d in directions) {
                    (int x, int y) n = (current.x + d.x, current.y + d.y);
                    if (WithinBounds(n) && !check.Contains(n)) {
                        if (grid[n.x, n.y] < 9)
                            check.Add(n);
                    }
                }
            } while (++index < check.Count);

            return size;
        }

        int product = 1;
        foreach (int i in largestBasins)
            product *= i;

        print(product);
    }