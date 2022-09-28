using System.Collections;
    public int day = 22;
    public Part part;
        static int range = 50;
        public int[][] coordinates = new int[3][];
        public bool on;

        public Instruction(string[] instruction) {
            on = instruction[0] == "on";
            coordinates = new int[][] {
                new int[] { int.Parse(instruction[2]), int.Parse(instruction[3]) },
                new int[] { int.Parse(instruction[5]), int.Parse(instruction[6]) },
                new int[] { int.Parse(instruction[8]), int.Parse(instruction[9]) }
            };
        }

        public bool IsInitializer() {
            foreach (int[] i in coordinates) {
                if (i[0] < -range || i[1] > range)
                    return false;
            }
            return true;
        }
    }
        public int[][] coordinates;
        public bool on;
        public BigInteger volume;

        public Cube(Instruction instruction) {
            coordinates = instruction.coordinates;
            on = instruction.on;
            UpdateVolume();
        }
        public Cube(int[][] coordinates, bool on) {
            this.coordinates = coordinates;
            this.on = on;
            UpdateVolume();
        }

        void UpdateVolume() {
            volume = 1;
            for (int i = 0; i < coordinates.Length; i++) {
                BigInteger min = coordinates[i][0];
                BigInteger max = coordinates[i][1];
                volume *= max - min + 1;
            }
        }
    }
            instructions.Add(new Instruction(AdventHelper.ParseString(s, false, '-')));
            if (!i.IsInitializer())
                continue;

            for (int a = i.coordinates[0][0]; a <= i.coordinates[0][1]; a++) {
                for (int b = i.coordinates[1][0]; b <= i.coordinates[1][1]; b++) {
                    for (int c = i.coordinates[2][0]; c <= i.coordinates[2][1]; c++)
                        grid[a + offset, b + offset, c + offset] = i.on;
                }
            }
        }
            if (b)
                cubeCount++;
        }

        // For Part 2 I initially tried my Part 1 solution, but there was not nearly
        // enough memory for the expanded input. I then switched to using dictionary so 
        // I wouldn't have to hold space for empty cells, but iterating through the axis still took
        // way too long. I then thought about using actual GameObjects and doing collision detection,
        // but they would eventually become arbitrary shapes with many faces and verteces,
        // becoming too hard to keep track of.

        // I then opened up Minecraft and began experimenting with adding and removing blocks in 3D space,
        // following along with the example. I eventually figured out that instead of taking chunks out of the cubes
        // to make new arbitrary shapes, I could just keep track of the overlapping parts as a separate cube,
        // and then subtract them from the total volume of all input cubes at the end.
        // Using the min and max of each axis range, I could check that for any axis, if they don't overlap the
        // current chain of cubes, then I can just skip the calculation for that overlap.
        // Each instruction would operate on the previous cube in the chain, and add or subtract how much of its
        // volume overlapped. When the instructions are finished I add up all the differences to get the final volume.
        // At this point there was still a lot of debugging to do so I decided to go to bed, but I knew the solution was in reach.

        // Next day:
        // I just had a little more debugging to do, which didn't take long with a fresh mind.
        // Most of my wrong answers from the previous day were because a ulong wasn't enough to hold the volume values.
        // After changing to using a BigInteger in place I was able to get the correct answer. This solution is the
        // one I'm most proud of for figuring out so far.

        List<Instruction> instructions = new List<Instruction>();
            instructions.Add(new Instruction(AdventHelper.ParseString(s, false, '-')));

            Cube cube = new Cube(i);
            List<Cube> newCubes = new List<Cube>();
            if (cube.on)
                newCubes.Add(cube);
            foreach (Cube c in cubeChain) {
                bool skip = false;
                int[][] overlap = new int[3][];
                for (int o = 0; o < overlap.Length; o++) {
                    overlap[o] = new int[] { Mathf.Max(c.coordinates[o][0], cube.coordinates[o][0]),
                                             Mathf.Min(c.coordinates[o][1], cube.coordinates[o][1]) };
                    if (overlap[o][0] > overlap[o][1]) {
                        skip = true;
                        break;
                    }
                }
                if (skip)
                    continue;

                newCubes.Add(new Cube(overlap, c.on ? false : true));
            }
            foreach (Cube c in newCubes)
                cubeChain.Add(c);
        }
            if (c.on)
                cubeCount += c.volume;
            else
                cubeCount -= c.volume;
        }
    }