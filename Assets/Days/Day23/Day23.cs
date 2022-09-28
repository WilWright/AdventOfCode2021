using System.Collections;
    public int day = 23;
    public Part part;
        public string symbol;
        public int cost;
        public GameObject gameObject;
        public Coordinates position;
        public int score;
        public Color32 color;
        public Text text;

        public Amphipod(string symbol, int cost, GameObject gameObject, Coordinates initialPosition) {
            this.symbol = symbol;
            this.cost = cost;
            this.gameObject = gameObject;
            position = initialPosition;
            text = gameObject.GetComponent<Text>();
            color = text.color;
        }
    }
        public string symbol;
        public Coordinates position;
        public int cost;
        public int moves;

        public AmphipodSim(string symbol, Coordinates position, int cost, int moves) {
            this.symbol = symbol;
            this.position = position;
            this.cost = cost;
            this.moves = moves;
        }
    }
        switch (part) {
                DoPart1();
                break;
                Part2();
                break;
    }

        // The algorithm for this challenged seemed like it would be complicated,
        // needing various path finding methods.
        // Since the complexity of the game was small, I decided to just implement
        // a playable version in Unity and do it by hand while keeping track of each
        // move's score.

        stringInput[3] += "  ";
        stringInput[4] += "  ";
        stringInput[5] += "  ";
        stringInput[6] += "  ";
        room = GridSystem<string>.GetStringGrid(AdventHelper.ParseInput(stringInput, true, '#', '.', ' '));
            string symbol = room.Get(c);
            GameObject go = Instantiate(template, board.transform);
            Text t = go.GetComponent<Text>();
            t.text = room.Get(c);
            go.transform.localPosition = new Vector2(c.x * symbolSpacing.x, c.y * symbolSpacing.y);

            try { counts[symbol]++; }
            catch { continue; }

            int index = symbolTypes.IndexOf(symbol);
            if (amphipods[index] == null)
                amphipods[index] = new Amphipod[4];
            t.color = counts[symbol] % 2 == 1 ? color1 : color2;
            Amphipod a = new Amphipod(symbol, (int)Mathf.Pow(10, index), go, c);
            amphipods[index][counts[symbol] - 1] = a;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SwitchAmphipod(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            SwitchAmphipod(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SwitchAmphipod(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            SwitchAmphipod(3);

        if (Input.GetKeyDown(KeyCode.UpArrow))
            MoveAmphipod(Coordinates.Up);
        if (Input.GetKeyDown(KeyCode.DownArrow))
            MoveAmphipod(Coordinates.Down);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            MoveAmphipod(Coordinates.Left);
        if (Input.GetKeyDown(KeyCode.RightArrow))
            MoveAmphipod(Coordinates.Right);

    }
        a.gameObject.transform.localPosition = new Vector2(a.position.x * symbolSpacing.x, a.position.y * symbolSpacing.y);
    }
        Coordinates next = currentAmphipod.position + direction;
        if (room.Get(next) != ".")
            return;

        room.Set(currentAmphipod.position, ".");
        currentAmphipod.position = next;
        room.Set(currentAmphipod.position, currentAmphipod.symbol);
        UpdatePosition(currentAmphipod);
        currentAmphipod.score += currentAmphipod.cost;
        totalScore += currentAmphipod.cost;
        total.text = totalScore + "";
        EvaluateBoard();
    }
        if (currentAmphipod == amphipods[index][currentSelection]) {
            if (++currentSelection > 3)
                currentSelection = 0;
        }

        currentAmphipod.text.color = currentAmphipod.color;
        currentAmphipod = amphipods[index][currentSelection];
        currentAmphipod.text.color = Color.green;
    }
        List<string> symbols = new List<string>();
        for (int i = 3; i <= 9; i += 2) {
            symbols.Add(room.Get(new Coordinates(i, 1)));
            symbols.Add(room.Get(new Coordinates(i, 2)));
            symbols.Add(room.Get(new Coordinates(i, 3)));
            symbols.Add(room.Get(new Coordinates(i, 4)));
        }
        string[] win = new string[] { "A", "A", "A", "A", "B", "B", "B", "B", "C", "C", "C", "C", "D", "D", "D", "D" };
        for (int i = 0; i < win.Length; i++) {
            if (symbols[i] != win[i])
                return;
        }
        if (totalScore < smallestScore || smallestScore == 0) {
            smallestScore = totalScore;
            print("New Smallest: " + smallestScore);
            smallest.text = smallestScore + "";
            totalScore = 0;
        }
    }
        stringInput[4] += "  ";
        stringInput[5] += "  ";
        stringInput[6] += "  ";
        GridSystem<string> room = GridSystem<string>.GetStringGrid(AdventHelper.ParseInput(stringInput, true, '#', '.', ' '));
        //availableSpots.Add(new Coordinates(1, 5));
        for (int i = 2; i <= 10; i += 2)
            availableSpots.Add(new Coordinates(i, 5));
        availableSpots.Add(new Coordinates(11, 5));
            string symbol = room.Get(c);
            switch (symbol) {
                case "A":
                case "B":
                case "C":
                case "D":
                    amphipodSims.Add(new AmphipodSim(symbol, c, (int)Mathf.Pow(10, symbolTypes.IndexOf(symbol)), 0));
                    //winSpots.Add(c);
                    room.Set(c, ".");
                    break;
            }
        }
        List<Coordinates> allSpots = new List<Coordinates>();
        Coordinates[] winSpots = new Coordinates[] { new Coordinates(3, 1), new Coordinates(5, 1), new Coordinates(7, 1), new Coordinates(9, 1) };
        foreach (Coordinates c in winSpots)
        int[] costs = new int[]{ 1, 10, 100, 1000 };
        for (int x = 3; x <= 9; x += 2) {
            string symbol = costs[index++] + "";
            for (int y = 1; y <= 4; y++)
                room.Set(new Coordinates(x, y), symbol);
        }

        List<AmphipodSim> ordered = new List<AmphipodSim>();
        for (int i = 0; i < costs.Length; i++) {
            for (int j = 0; j < 4; j++) {
                foreach (AmphipodSim aSim in amphipodSims) {
                    if (aSim.cost == costs[i]) {
                        ordered.Add(aSim);
                        amphipodSims.Remove(aSim);
                        break;
                    }
                }
            }
        }
        //AmphipodSim asm = ordered[0];
        //ordered[0] = ordered[12];
        //ordered[12] = asm;
        //Coordinates[] setWins = new Coordinates[] { new Coordinates(3, 1), new Coordinates(3, 2), new Coordinates(3, 3), new Coordinates(3, 4),
        //                                            new Coordinates(5, 1), new Coordinates(5, 2), new Coordinates(5, 3), new Coordinates(5, 4),
        //                                            new Coordinates(7, 1), new Coordinates(7, 2), new Coordinates(7, 3), new Coordinates(7, 4),
        //                                            new Coordinates(9, 1), new Coordinates(9, 2), new Coordinates(9, 3), new Coordinates(9, 4),};
        //for (int i = 0; i < setWins.Length; i++)
        //    ordered[i] = new AmphipodSim(ordered[i].symbol, setWins[i], ordered[i].cost, 0);
        //AmphipodSim[] sims = new AmphipodSim[] { ordered[12], ordered[0], ordered[5], ordered[6], ordered[1], ordered[8], ordered[9], ordered[7], ordered[15], ordered[7], ordered[6], ordered[5], ordered[10],
        //ordered[3], ordered[15], ordered[4], ordered[13], ordered[14], ordered[1], ordered[0], ordered[14], ordered[3], ordered[12] };
        int[] indeces = new int[] { 12, 0, 5, 6, 1, 8, 9, 7, 15, 7, 6, 5, 10, 3, 15, 4, 13, 14, 1, 0, 14, 3, 12 };
        Coordinates[] locations = new Coordinates[] { new Coordinates(11, 5), new Coordinates(1, 5), new Coordinates(10, 5), new Coordinates(8, 5), new Coordinates(2, 5), new Coordinates(7, 2), new Coordinates(7, 3),
        new Coordinates(6, 5), new Coordinates(4, 5), new Coordinates(5, 1), new Coordinates(5, 2), new Coordinates(5, 3), new Coordinates(7, 4), new Coordinates(10, 5), new Coordinates(9, 1), new Coordinates(5, 4),
        new Coordinates(9, 2), new Coordinates(4, 5),  new Coordinates(3, 2), new Coordinates(3, 3), new Coordinates(9, 3), new Coordinates(3, 4), new Coordinates(9, 4) };
        HashSet<string> states = new HashSet<string>();
        int smallestTotal = 1000000;
        int moveCount = 0;
        //List<AmphipodSim> lastBoard = null;
        Queue<List<AmphipodSim>> lastBoards = new Queue<List<AmphipodSim>>();
        Move(ordered, 0);
        //foreach (List<AmphipodSim> a in lastBoards)
        //    PrintBoard(a);

        //int testTotal = 0;
        //for (int i = 0; i < indeces.Length; i++) {
        //    //List<AmphipodSim> testSims = new List<AmphipodSim>();
        //    PrintBoard(ordered);
        //    testTotal += PathFind(ordered[indeces[i]], locations[i], ordered, locations[i].y < 5);
        //    ordered[indeces[i]] = new AmphipodSim(ordered[indeces[i]].symbol, locations[i], ordered[indeces[i]].cost, ordered[indeces[i]].moves + 1);
        //}
        //bool won = true;
        //foreach (AmphipodSim aSim in ordered) {
        //    if (room.Get(aSim.position) != aSim.cost + "") {
        //        won = false;
        //        break;
        //    }
        //}
        //if (won) {
        //    //print("win " + totalCost + " " + smallestTotal);
        //    if (testTotal < smallestTotal)
        //        smallestTotal = testTotal;
        //}
        //print(testTotal);

        print(smallestTotal + " " + moveCount);



        //int PathFind(AmphipodSim aSim, Coordinates position, List<AmphipodSim> aSims, bool toWinSpot) {
        //    Coordinates moves = new Coordinates(0, 0);
        //    print("Start " + aSim.cost + ": " + aSim.position);
        //    if (toWinSpot) {
        //        moves.y += PathY(5);
        //        print("Y " + moves.y);
        //        if (moves.y == -1)
        //            return -1;
        //    }
        //    if (aSim.position.y > position.y) {
        //        moves.x += PathX(position.x);
        //        if (moves.x == -1)
        //            return -1;
        //        print("X " + moves.x);

        //        moves.y += PathY(position.y);
        //        if (moves.y == -1)
        //            return -1;
        //        print("Y " + moves.y);
        //    }
        //    else {
        //        moves.y += PathY(position.y);
        //        if (moves.y == -1)
        //            return -1;
        //        print("Y " + moves.y);

        //        moves.x += PathX(position.x);
        //        if (moves.x == -1)
        //            return -1;
        //        print("X " + moves.x);

        //    }
        //    print("Moved " + aSim.cost + ": " +  position + " | " + (moves.x + moves.y));
        //    return moves.x * aSim.cost + moves.y * aSim.cost;

        //    int PathX(int x) {
        //        int step = aSim.position.x > x ? -1 : 1;
        //        int move = 0;
        //        while (aSim.position.x != x) {
        //            aSim.position.x += step;
        //            move++;
        //            if (!CanMove())
        //                return -1;
        //        }
        //        return move;
        //    }
        //    int PathY(int y) {
        //        int step = aSim.position.y > y ? -1 : 1;
        //        int move = 0;
        //        while (aSim.position.y != y) {
        //            aSim.position.y += step;
        //            move++;
        //            if (!CanMove())
        //                return -1;
        //        }
        //        return move;
        //    }

        //    bool CanMove() {
        //        foreach (AmphipodSim a in aSims) {
        //            if (aSim.position == a.position)
        //                return false;
        //        }
        //        return true;
        //    }
        //}

        void Move(List<AmphipodSim> aSims, int totalCost) {
            //print("new");
            moveCount++;
            if (totalCost >= smallestTotal)
                return;
            //if (++moveCount > 100)
            //    return;
            //PrintBoard(aSims);
            bool won = true;
            foreach (AmphipodSim aSim in aSims) {
                if (room.Get(aSim.position) != aSim.cost + "") {
                    won = false;
                    break;
                }
            }
            if (won) {
                //print("win " + totalCost + " " + smallestTotal);
                if (totalCost < smallestTotal)
                    smallestTotal = totalCost;
                return;
            }

            for (int i = 0; i < aSims.Count; i++) {
                if (totalCost >= smallestTotal)
                    return;

                if (aSims[i].moves > 1)
                    continue;

                foreach (Coordinates spot in allSpots) {
                    if (totalCost >= smallestTotal)
                        return;

                    bool isWinSpot = spot.y < 5;
                    Coordinates nextSpot = spot;
                    if (isWinSpot) {
                        if (room.Get(spot) != aSims[i].symbol)
                            continue;

                        bool canMove = true;
                        for (int y = 1; y < 5; y++) {
                            bool found = false;
                            Coordinates check = new Coordinates(spot.x, y);
                            foreach (AmphipodSim a in aSims) {
                                if (a.position == check) {
                                    if (a.cost != aSims[i].cost) {
                                        canMove = false;
                                        break;
                                    }
                                    found = true;
                                    break;
                                }
                            }
                            if (!canMove)
                                break;
                            if (!found) {
                                canMove = true;
                                nextSpot = check;
                                break;
                            }
                            else
                                canMove = false;
                        }
                        if (!canMove)
                            continue;
                    }

                    int pathCost = PathFind(aSims[i], nextSpot, isWinSpot);
                    //print(pathCost + " " + totalCost + " " + spot);
                    if (pathCost > -1) {
                        List<AmphipodSim> newSims = new List<AmphipodSim>();
                        string state = "";
                        foreach (AmphipodSim aSim in aSims) {
                            if (aSim.position == aSims[i].position) {
                                //print(aSim.symbol);
                                int moves = isWinSpot ? 2 : aSim.moves + 1;
                                newSims.Add(new AmphipodSim(aSim.symbol, nextSpot, aSim.cost, moves));
                                state += nextSpot + ":" + moves + "|";
                            }
                            else {
                                newSims.Add(aSim);
                                state += aSim.position + ":" + aSim.moves + "|";
                            }
                        }
                        //print(state);
                        if (!states.Add(state)) {
                            //print("same state");
                            return;
                        }
                        //if (lastBoards.Count == 10)
                        //    lastBoards.Dequeue();
                        //lastBoards.Enqueue(newSims);
                        //try { states.Add(state); }
                        //catch { print("same state"); return; }
                        Move(newSims, totalCost + pathCost);
                        //print(state);
                        PrintBoard(newSims);
                    }
                }

                int PathFind(AmphipodSim aSim, Coordinates position, bool toWinSpot) {
                    Coordinates moves = new Coordinates(0, 0);
                    //print("Start " + aSim.cost + ": " + aSim.position);
                    if (toWinSpot) {
                        moves.y += PathY(5);
                        if (moves.y == -1)
                            return -1;
                    }

                    if (aSim.position.y > position.y) {
                        moves.x += PathX(position.x);
                        if (moves.x == -1)
                            return -1;
                        //print("X " + moves.x);

                        moves.y += PathY(position.y);
                        if (moves.y == -1)
                            return -1;
                        //print("Y " + moves.y);
                    }
                    else {
                        moves.y += PathY(position.y);
                        if (moves.y == -1)
                            return -1;
                        //print("Y " + moves.y);

                        moves.x += PathX(position.x);
                        if (moves.x == -1)
                            return -1;
                        //print("X " + moves.x);

                    }
                    //print("Moved " + aSim.cost + ": " + aSim.position + " -> " + position + "|" + (moves.x + moves.y));
                    return moves.x * aSim.cost + moves.y * aSim.cost;

                    int PathX(int x) {
                        int step = aSim.position.x > x ? -1 : 1;
                        int move = 0;
                        while (aSim.position.x != x) {
                            aSim.position.x += step;
                            move++;
                            if (!CanMove())
                                return -1;
                        }
                        return move;
                    }
                    int PathY(int y) {
                        int step = aSim.position.y > y ? -1 : 1;
                        int move = 0;
                        while (aSim.position.y != y) {
                            aSim.position.y += step;
                            move++;
                            if (!CanMove())
                                return -1;
                        }
                        return move;
                    }

                    bool CanMove() {
                        foreach (AmphipodSim a in aSims) {
                            if (aSim.position == a.position)
                                return false;
                        }
                        return true;
                    }
                }
            }
        }

        void PrintBoard(List<AmphipodSim> aSims) {
            string state = "";
            foreach (AmphipodSim a in aSims) {
                state += a.position + "|" + a.moves + "|";
            }
            print(state + " " + states.Add(state));
            for (int y = room.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < room.Width(); x++) {
                    Coordinates c = new Coordinates(x, y);
                    bool found = false;
                    foreach (AmphipodSim aSim in aSims) {
                        if (aSim.position == c) {
                            found = true;
                            s += " " + aSim.symbol;
                            break;
                        }
                    }
                    if (found)
                        continue;

                    switch (room.Get(c)) {
                        case "#":
                            s += " #";
                            break;

                        default:
                            s += " . ";
                            break;
                    }
                }
                print(s);
            }
        }

        //HashSet<string> states = new HashSet<string>();
        //int smallestTotal = 1000000;
        //bool moving = false;
        //ulong flags = 0;
        //StartCoroutine(Move(amphipodSims, 0));
        //print(smallestTotal);

        //IEnumerator Move(List<AmphipodSim> aSims, int totalCost) {
        //    print("Move: " + totalCost);
        //    flags++;
        //    for (int i = 0; i < winSpots.Count; i++) {
        //        int symbolIndex = i;
        //        if (symbolIndex > symbolIndeces.Count)
        //            symbolIndex %= symbolIndeces.Count;

        //        bool won = true;
        //        foreach (AmphipodSim aSim in aSims) {
        //            if (winSpots[i] == aSim.position) {
        //                if (aSim.moves < 2 || aSim.cost != Mathf.Pow(10, symbolIndex))
        //                    won = false;
        //                break;
        //            }
        //        }
        //        if (won) {
        //            smallestTotal = totalCost;
        //            moving = false;
        //            yield break;
        //        }
        //    }

        //    for (int i = 0; i < aSims.Count; i++) {
        //        if (totalCost >= smallestTotal) {
        //            moving = false;
        //            yield break;
        //        }

        //        if (aSims[i].moves > 1)
        //            continue;

        //        foreach (Coordinates spot in availableSpots) {
        //            if (totalCost >= smallestTotal) {
        //                moving = false;
        //                yield break;
        //            }

        //            int pathCost = 0;
        //            bool pathing = true;
        //            StartCoroutine(PathFind(aSims[i], spot));
        //            yield return new WaitWhile(() => pathing);

        //            if (pathCost > -1) {
        //                List<AmphipodSim> newSims = new List<AmphipodSim>();
        //                foreach (AmphipodSim aSim in aSims)
        //                    newSims.Add(aSim);
        //                newSims[i] = new AmphipodSim(spot, aSims[i].cost, aSims[i].moves + 1);
        //                string state = "";
        //                foreach (AmphipodSim aSim in newSims)
        //                    state += aSim.position + "|" + aSim.moves + "|";

        //                try { states.Add(state); }
        //                catch { moving = false; }
        //                if (!moving)
        //                    yield break;

        //                moving = true;
        //                StartCoroutine(Move(aSims, totalCost + pathCost));
        //                yield return new WaitWhile(() => moving);
        //            }

        //            IEnumerator PathFind(AmphipodSim aSim, Coordinates position) {
        //                ulong moveFlags = 0;
        //                foreach (Coordinates c in Coordinates.CompassDirection) {
        //                    StartCoroutine(MoveNext(aSim, c, aSim.cost));
        //                    yield return new WaitWhile(() => moveFlags > 0);
        //                }

        //                yield return new WaitWhile(() => moveFlags > 0);

        //                pathing = false;

        //                IEnumerator MoveNext(AmphipodSim a, Coordinates move, int cost) {
        //                    moveFlags++;

        //                    a.position += move;
        //                    if (a.position == position) {
        //                        pathCost = cost;
        //                        moveFlags--;
        //                        yield break;
        //                    }

        //                    foreach (AmphipodSim a2 in aSims) {
        //                        if (a.position == a2.position) {
        //                            moveFlags--;
        //                            yield break;
        //                        }
        //                    }
        //                    if (a.moves > 0) {
        //                        foreach (Coordinates c in winSpots) {
        //                            if (a.position == c) {
        //                                moveFlags--;
        //                                yield break;
        //                            }
        //                        }
        //                    }
        //                    if (room.Get(a.position) == "#") {
        //                        moveFlags--;
        //                        yield break;
        //                    }

        //                    foreach (Coordinates c in Coordinates.CompassDirection) {
        //                        if (c == -move)
        //                            continue;

        //                        ulong currentFlags = moveFlags;
        //                        StartCoroutine(MoveNext(aSim, c, cost + aSim.cost));
        //                        yield return new WaitWhile(() => moveFlags > currentFlags);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    void Done() {
        //        moving = false;
        //        if (--flags == 0)
        //            print(smallestTotal);
        //    }
        //}


        //print();
    }