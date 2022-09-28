using System;
using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day16 : MonoBehaviour {
    public int day = 16;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    Dictionary<char, string> hexToBinary = new Dictionary<char, string>() {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },
        { 'A', "1010" },
        { 'B', "1011" },
        { 'C', "1100" },
        { 'D', "1101" },
        { 'E', "1110" },
        { 'F', "1111" }
    };        [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");        // This challenge had a LOT of information to unpack. The basis of it was reading a binary string to extract different        // chunks of data from it. I knew I would only be reading it from left to right in order, so I came up with GetNext().        // Any time I wanted the next bits to read I could just pass in the amount and GetNext() would give me back the string        // I needed while advancing the index for the next time I use it. This way I didn't have to deal with any Substring() calculations,        // or worry about what my current position was along the string.        // After getting that down I was able to parse the version and type number, and then the length type.        // This went pretty smoothly until I had to parse the operator packets. For a long time I was confused on how the examples were getting the        // values for the sub-packets. In the initial example the literal value was labeled in chunks as A, B, and C. The operator packets were also labeled        // with A and B in another example. In both examples there were also labels for the version and type numbers, V and T.        // I feel the challenge failed to properly explain that the operator packets were entirely new packets with their own version and type numbers,        // instead of just more literal values like they seemed to be labeled as. It was very frustrating when the bit counts didn't add up for me,        // because I was trying to count out the 5 length chunks in the operator packets instead of first counting the 3 length version and type chunks.        // After dealing with that headache for an hour and 40 minutes I just had to fix some minor bugs and it was working.        string input = stringInput[0];        string binary = "";        foreach (char c in input)
            binary += hexToBinary[c];        int index = 0;
        long versionTotal = 0;        ReadPacket();        print(versionTotal);

        void ReadPacket() {
            long packetVersion = BinaryToDecimal(GetNext(3));
            versionTotal += packetVersion;
            long typeID = BinaryToDecimal(GetNext(3));
            switch (typeID) {
                case 4:
                    long literalValue = BinaryToDecimal(GetLiteral());
                    break;

                default:
                    switch (GetNext(1)) {
                        case "0":
                            long subLength = BinaryToDecimal(GetNext(15));
                            long subEnd = index + subLength;
                            while (index < subEnd)
                                ReadPacket();
                            break;

                        case "1":
                            long subAmount = BinaryToDecimal(GetNext(11));
                            for (int i = 0; i < subAmount; i++)
                                ReadPacket();
                            break;
                    }
                    break;
            }
        }

        string GetNext(int amount) {
            string s = "";
            for (int i = 0; i < amount; i++) {
                s += binary[index];
                if (++index >= binary.Length)
                    break;
            }
            return s;
        }

        string GetLiteral() {
            string literal = "";
            while (GetNext(1) == "1")
                literal += GetNext(4);
            literal += GetNext(4);
            return literal;
        }        long BinaryToDecimal(string binaryString) {
            return Convert.ToInt64(binaryString, 2);
        }    }    void Part2() {        print("Part 2");

        // Part 2 went pretty well. Most of the recursive logic was already done from Part 1,
        // I just had to also keep track of the sub-values and return those.

        // After 30 minutes of getting the logic down it almost worked first try. I had to change the values from int to long,
        // and there was some floating point precision errors in min and max, because I was using Mathf.Min/Max
        // and then casting it, so I just implemented that manually.

        // Out of the 16 days so far this challenge took me the third longest to complete both parts, 
        // but despite this I was able to get a good rank of 3,674.        string input = stringInput[0];        string binary = "";        foreach (char c in input)
            binary += hexToBinary[c];        int index = 0;        print(ReadPacket());

        long ReadPacket() {
            long packetVersion = BinaryToDecimal(GetNext(3));
            long typeID = BinaryToDecimal(GetNext(3));
            List<long> subValues = new List<long>();

            if (typeID != 4) {
                switch (GetNext(1)) {
                    case "0":
                        long subLength = BinaryToDecimal(GetNext(15));
                        long subEnd = index + subLength;
                        while (index < subEnd)
                            subValues.Add(ReadPacket());
                        break;

                    case "1":
                        long subAmount = BinaryToDecimal(GetNext(11));
                        for (int i = 0; i < subAmount; i++)
                            subValues.Add(ReadPacket());
                        break;
                }
            }
            
            switch (typeID) {
                case 0:
                    long sum = 0;
                    foreach (long l in subValues)
                        sum += l;
                    return sum;

                case 1:
                    long product = 1;
                    foreach (long l in subValues)
                        product *= l;
                    return product;

                case 2:
                    long min = subValues[0];
                    foreach (long l in subValues) {
                        if (l < min)
                            min = l;
                    }
                    return min;

                case 3:
                    long max = subValues[0];
                    foreach (long l in subValues) {
                        if (l > max)
                            max = l;
                    }
                    return max;

                case 4:
                    return BinaryToDecimal(GetLiteral());

                case 5:
                    return subValues[0] > subValues[1] ? 1 : 0;

                case 6:
                    return subValues[0] < subValues[1] ? 1 : 0;

                case 7:
                    return subValues[0] == subValues[1] ? 1 : 0;
            }
            return -1;
        }

        string GetNext(int amount) {
            string s = "";
            for (int i = 0; i < amount; i++) {
                s += binary[index];
                if (++index >= binary.Length)
                    break;
            }
            return s;
        }

        string GetLiteral() {
            string literal = "";
            while (GetNext(1) == "1")
                literal += GetNext(4);
            literal += GetNext(4);
            return literal;
        }        long BinaryToDecimal(string binaryString) {
            return Convert.ToInt64(binaryString, 2);
        }
    }}