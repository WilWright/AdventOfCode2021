using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day6 : MonoBehaviour {
    public int day = 6;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    class BigInt {
        List<int> digitCounts  = new List<int>();

        public BigInt(int initial) {
            digitCounts.Add(initial);
        }

        public void Add(int amount, int current = 0) {
            int magnitude = 10 * (current + 1);
            int addDigits = amount / magnitude;
            int overflow = amount % magnitude;

            digitCounts[current] += overflow;
            if (addDigits > 0) {
                if (++current >= digitCounts.Count)
                    digitCounts.Add(0);
                Add(addDigits, current);
            }
        }
        public void Add(BigInt bigInt) {
            for (int i = 0; i < bigInt.digitCounts.Count; i++) {
                if (i >= digitCounts.Count)
                    digitCounts.Add(0);
                
                digitCounts[i] += bigInt.digitCounts[i];
            }
            ConvertCountsToDigits();
        }

        public void ConvertCountsToDigits() {
            for (int i = 0; i < digitCounts.Count; i++) {
                while (digitCounts[i] > 9) {
                    digitCounts[i] -= 10;

                    if (i + 1 >= digitCounts.Count)
                        digitCounts.Add(0);

                    digitCounts[i + 1]++;
                }
            }
        }

        public override string ToString() {
            string s = "";
            for (int i = digitCounts.Count - 1; i >= 0; i--)
                s += digitCounts[i];
            return s;
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        stringInput = stringInput[0].Split(',');        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This one was pretty simple, I just followed the instructions and implemented the logic,
        // counting down each fish, and then resetting it and adding a new one when it reaches 0.
        // So far this is the fastest time (13 minutes and 30 seconds) and best rank (4,583) I've gotten.

        int days = 80;        List<int> fish = new List<int>();
        foreach (int i in intInput)
            fish.Add(i);                for (int i = 0; i < days; i++) {
            List<int> newFish = new List<int>();
            for (int j = 0; j < fish.Count; j++) {
                if (--fish[j] < 0) {
                    newFish.Add(8);
                    fish[j] = 6;
                }
                newFish.Add(fish[j]);
            }
            fish = newFish;
        }        print(fish.Count);    }    void Part2() {        print("Part 2");

        // For the second part the added challenge was just to up the days from 80 to 256.
        // When trying the same solution, Unity kept crashing because I assume the list became too large, 
        // I was running out of memory, or the calculation was taking forever.
        // I quickly came up with the idea to use an int[] and keep track of how many groupings of each cycle there were instead of individual fish.
        // For example: instead of an inital input of 3,4,3,1,2 -> 2,3,2,0,2 -> 1,2,1,6,1,8,
        // the array would look like [0][1][1][2][1][0][0][0][0] -> [1][1][2][1][0][0][0][0][0] -> [1][2][1][0][0][0][1][0][1].
        // This way I would only have to worry about a capacity of 9 the whole time.

        // After getting wrong answers and doing some debugging however, I realized this also wasn't enough, 
        // as the ints inside the array were overflowing due to the exponential nature of the challenge (I'm guessing this was the purpose of this part).
        // Unfortunately I've never had to deal with numbers this big, so I wasn't really sure how to tackle it.
        // After a lot more thinking and debugging I was able to get over this hurdle by creating the BigInt,
        // which stores how many of each digit the number contains (backwards). 
        // For example: 231 would look like [1][3][2], then adding 1,497 would look like [8][12][6][1], and then would convert to [8][2][7][1] for 1,728.
        // Now a number like 4,245,657,126 would only take a capacity of 10 with ints from 0-9 in each element.

        // This took a lot of time to get right, a whopping 1 hour and 30 minutes. In previous challenges my rank had improved by about 1,000 or so between Part 1 and 2,
        // but this time it worsened by about 6,000. I think a lot of participants, especially veterans of Advent of Code, 
        // were privy to these kinds of problems already and more easily figured out what was going on in Part 2 and knew how to deal with it.

        // Aaaannd after looking at some other participants' solutions I realized I could just have changed the original int[] to a long[],
        // instead of putting a bunch of time into creating BigInt. Something that slipped my mind since first studying primitive types in Programming 1.
        // All a part of the learning process I guess!

        BigInt[] fish = new BigInt[9];        for (int i = 0; i < fish.Length; i++)            fish[i] = new BigInt(0);        foreach (int i in intInput)
            fish[i].Add(1);

        int days = 256;
        for (int i = 0; i < days; i++) {
            BigInt newFish = fish[0];
            for (int index = 0; index < fish.Length - 1; index++)
                fish[index] = fish[index + 1];
            fish[6].Add(newFish);
            fish[8] = newFish;
        }

        BigInt totalFish = new BigInt(0);
        foreach (BigInt bi in fish)
            totalFish.Add(bi);

        print(totalFish);

        /*
         
        long[] fish = new long[9];        foreach (int i in intInput)
            fish[i]++;

        int days = 256;
        for (int i = 0; i < days; i++) {
            long newFish = fish[0];
            for (int index = 0; index < fish.Length - 1; index++)
                fish[index] = fish[index + 1];
            fish[6] += newFish;
            fish[8] = newFish;
        }

        long totalFish = 0;
        foreach (long l in fish)
            totalFish += l;

        print(totalFish);

        */
    }}