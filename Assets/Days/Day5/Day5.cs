using System.Collections;
    public int day = 5;
    public Part part;
            coords[i] = new int[4];
            string[] s = stringInput[i].Replace(" -> ", ",").Split(',');
            for (int j = 0; j < s.Length; j++)
                coords[i][j] = int.Parse(s[j]);
        }
            if (coord[0] != coord[2] && coord[1] != coord[3])
                continue;

            int axis = 0;
            for (int i = 0; i < 2; i++) {
                if (coord[i] != coord[i + 2])
                    axis = i;
                if (coord[i] > coord[i + 2]) {
                    int temp = coord[i];
                    coord[i] = coord[i + 2];
                    coord[i + 2] = temp;
                }
            }
            
            if (axis == 0) {
                for (int x = coord[0]; x <= coord[2]; x++) {
                    var c = (x, coord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
            else {
                for (int y = coord[1]; y <= coord[3]; y++) {
                    var c = (coord[0], y);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
        }
            if (kvp.Value > 1)
                points++;
        }
            coords[i] = new int[4];
            string[] s = stringInput[i].Replace(" -> ", ",").Split(',');
            for (int j = 0; j < s.Length; j++)
                coords[i][j] = int.Parse(s[j]);
        }
            if (Mathf.Abs(coord[0] - coord[2]) == Mathf.Abs(coord[1] - coord[3])) {
                int[] tempCoord = new int[4];
                coord.CopyTo(tempCoord, 0);

                var c = (tempCoord[0], tempCoord[1]);
                try { grid[c]++; }
                catch { grid.Add(c, 1); }

                while (tempCoord[0] != coord[2] && tempCoord[1] != coord[3]) {
                    if (tempCoord[0] < coord[2]) tempCoord[0]++;
                    else                         tempCoord[0]--;
                    if (tempCoord[1] < coord[3]) tempCoord[1]++;
                    else                         tempCoord[1]--;

                    c = (tempCoord[0], tempCoord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
                continue;
            }

            int axis = 0;
            for (int i = 0; i < 2; i++) {
                if (coord[i] != coord[i + 2])
                    axis = i;
                if (coord[i] > coord[i + 2]) {
                    int temp = coord[i];
                    coord[i] = coord[i + 2];
                    coord[i + 2] = temp;
                }
            }

            if (axis == 0) {
                for (int x = coord[0]; x <= coord[2]; x++) {
                    var c = (x, coord[1]);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
            else {
                for (int y = coord[1]; y <= coord[3]; y++) {
                    var c = (coord[0], y);
                    try { grid[c]++; }
                    catch { grid.Add(c, 1); }
                }
            }
        }
            if (kvp.Value > 1)
                points++;
        }