using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day21 : MonoBehaviour {
    public int day = 21;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class Die {
        public int rollCount;
        public int currentRoll = 1;
        int rollStep = 3;

        public int Roll() {
            int rollTotal = 0;
            for (int i = 0; i < rollStep; i++) {
                rollTotal += currentRoll;
                if (++currentRoll > 100)
                    currentRoll = 1;
            }
            rollCount += rollStep;
            return rollTotal;
        }
    }    class Player {
        public int boardSpace;
        public int score;

        public Player(int boardSpace) {
            this.boardSpace = boardSpace;
        }

        public void Roll(Die die) {
            boardSpace += die.Roll();
            while (boardSpace > 10)
                boardSpace -= 10;
            score += boardSpace;
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // Part 1 of this challenge was easy, just a simple implentation of the rules.        // I got my best rank so far, 1,825.        Player[] players = new Player[] { new Player(int.Parse(stringInput[0][stringInput[0].Length - 1] + "")), new Player(int.Parse(stringInput[1][stringInput[1].Length - 1] + "")) };        Die die = new Die();        Player winner = null;        Player loser = null;        while (winner == null) {
            players[0].Roll(die);
            if (players[0].score >= 1000) {
                winner = players[0];
                loser = players[1];
                break;
            }
            players[1].Roll(die);
            if (players[1].score >= 1000) {
                winner = players[1];
                loser = players[0];
                break;
            }
        }        print(loser.score * die.rollCount);    }    void Part2() {        print("Part 2");

        // Part 2 really ramped up the complexity. For a long time a tried to think of a solution
        // similar to Day 6's lanternfish, where I would need to keep track of groups of positions and scores
        // instead of individual games. I couldn't figure out a way to link together the amount of games won though.
        // Then I tried generating all paths and keeping track of the amount of steps it took to win.
        // When comparing paths I could then check which ones took less steps, meaning that player won.

        // I was getting way too low of a value for my answer, though, and realized I misunderstood the rules.
        // I thought that the new die counted as three rolls for the three universes that it split into, but it actually had
        // to be rolled 2 more times in each split. So instead of 3 possible outcomes for each player's turn,
        // it became 36. It was getting really late at this point, and after changing the logic for the rolls
        // I ran it one last time. After an hour of running it still wasn't giving an answer. I knew I would need to rethink my solution,
        // so I went to bed at this point.

        // Next day:
        // I still couldn't figure out a good solution, so I looked online for a hint.
        // Apparently the solution still required brute force, but with a twist. This is when I learned about memoization
        // and dynamic programming. By storing the state of each outcome, I could check if a state has been encountered before,
        // and then instead of recalculating the whole path I could use the answer that previous state already calculated.
        // With this simple addition to my algorithm I was able to get the answer within seconds.

        Dictionary<string, (ulong, ulong)> gameStates = new Dictionary<string, (ulong, ulong)>();
        (ulong, ulong) u = TakeTurn(int.Parse(stringInput[0][stringInput[0].Length - 1] + ""), 0, int.Parse(stringInput[1][stringInput[1].Length - 1] + ""), 0, true);
        print(u.Item1 > u.Item2 ? u.Item1 : u.Item2);

        (ulong, ulong) TakeTurn(int space1, int score1, int space2, int score2, bool player1Turn) {
            string state = space1 + "|" + score1 + "|" + space2 + "|" + score2 + "|" + player1Turn;
            try { return gameStates[state]; }
            catch { }

            if (score1 >= 21)
                return (1, 0);
            if (score2 >= 21)
                return (0, 1);

            (ulong, ulong) score = (0, 0);
            if (player1Turn) {
                for (int a = 1; a <= 3; a++) {
                    for (int b = 1; b <= 3; b++) {
                        for (int c = 1; c <= 3; c++) {
                            int nextSpace = space1 + a + b + c;
                            if (nextSpace > 10)
                                nextSpace -= 10;

                            (ulong, ulong) nextScore = TakeTurn(nextSpace, score1 + nextSpace, space2, score2, false);
                            score = (score.Item1 + nextScore.Item1, score.Item2 + nextScore.Item2);
                        }
                    }
                }
            }
            else {
                for (int a = 1; a <= 3; a++) {
                    for (int b = 1; b <= 3; b++) {
                        for (int c = 1; c <= 3; c++) {
                            int nextSpace = space2 + a + b + c;
                            if (nextSpace > 10)
                                nextSpace -= 10;

                            (ulong, ulong) nextScore = TakeTurn(space1, score1, nextSpace, score2 + nextSpace, true);
                            score = (score.Item1 + nextScore.Item1, score.Item2 + nextScore.Item2);
                        }
                    }
                }
            }

            gameStates.Add(state, score);
            return score;
        }
    }}