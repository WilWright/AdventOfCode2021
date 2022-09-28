using System;
using System.Collections;
    public int day = 16;
    public Part part;
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
    };
            binary += hexToBinary[c];
        long versionTotal = 0;

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
        }
            return Convert.ToInt64(binaryString, 2);
        }

        // Part 2 went pretty well. Most of the recursive logic was already done from Part 1,
        // I just had to also keep track of the sub-values and return those.

        // After 30 minutes of getting the logic down it almost worked first try. I had to change the values from int to long,
        // and there was some floating point precision errors in min and max, because I was using Mathf.Min/Max
        // and then casting it, so I just implemented that manually.

        // Out of the 16 days so far this challenge took me the third longest to complete both parts, 
        // but despite this I was able to get a good rank of 3,674.
            binary += hexToBinary[c];

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
        }
            return Convert.ToInt64(binaryString, 2);
        }
    }