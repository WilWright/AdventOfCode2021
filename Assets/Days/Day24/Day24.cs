using System.Collections;
    public int day = 24;
    public Part part;
        static bool debug = true;
        public static bool invalid = false;
        //public static Queue<int> modelNumber;
        public static string[] unknowns = new string[4];
        public static int[] variables = new int[4];
        //public static List<int>[] variables = new List<int>[4];
        public string type;
        public int a;
        public int b = -1;
        public int constant;
        //public List<int> constant;

        public Instruction(string[] input) {
            type = input[0];
            switch (input[1]) {
                case "w": a = 0; break;
                case "x": a = 1; break;
                case "y": a = 2; break;
                case "z": a = 3; break;
            }

            if (input.Length < 3)
                return;
            switch (input[2]) {
                case "w": b = 0; break;
                case "x": b = 1; break;
                case "y": b = 2; break;
                case "z": b = 3; break;
                default: constant = int.Parse(input[2]); break;
                //default: constant = new List<int>() { int.Parse(input[2]) }; break;
            }
        }

        //public static void Reset() {
        //    invalid = false;
        //    variables = new List<int>[4];
        //    for (int i = 0; i < variables.Length; i++) {
        //        variables[i] = new List<int>();
        //        if (i > 0)
        //            variables[i].Add(0);
        //    }
        //    for (int i = 1; i <= 9; i++)
        //        variables[0].Add(i);
        //}

        //List<int> GetB() {
        //    return b == -1 ? constant : variables[b];
        //}
        int GetB() {
            return b == -1 ? constant : variables[b];
        }
        public void Compute() {
            switch (type) {
                case "add":
                    Add();
                    break;
                case "mul":
                    Mul();
                    break;
                case "div":
                    Div();
                    break;
                case "mod":
                    Mod();
                    break;
                case "eql":
                    Eql();
                    break;
            }

            ////Debug("\t" + type);
            //if (type == "inp")
            //    return;

            //HashSet<int> newNums = new HashSet<int>();
            //List<int> B = GetB();
            //foreach (int a in variables[a]) {
            //    foreach (int b in B) {
            //        try {
            //            switch (type) {
            //                case "add":
            //                    newNums.Add(a + b);
            //                    break;
            //                case "mul":
            //                    newNums.Add(a * b);
            //                    break;
            //                case "div":
            //                    if (b == 0)
            //                        continue;
            //                    newNums.Add(a / b);
            //                    break;
            //                case "mod":
            //                    if (b <= 0)
            //                        continue;
            //                    newNums.Add(a % b);
            //                    break;
            //                case "eql":
            //                    newNums.Add(a == b ? 1 : 0);
            //                    break;
            //            }
            //        }
            //        catch { }
            //    }
            //}
            //List<int> setNums = new List<int>();
            //foreach (int i in newNums)
            //    setNums.Add(i);
            //variables[a] = setNums;
        }

        void Inp() {
            //variables[a] = modelNumber.Dequeue();
            Debug(variables[a] + "");
        }
        void Add() {
            //Dictionary<>
            //List<int> B = GetB();
            //for (int i = 0; i < variables[a].Count; i++) {
            //    foreach (int b in B)
            //        variables[a]
            //}
            Debug(variables[a] + " + " + GetB());
            variables[a] += GetB();
            Debug("= " + variables[a]);
        }
        void Mul() {
            Debug(variables[a] + " * " + GetB());
            variables[a] *= GetB();
            Debug("= " + variables[a]);
        }
        void Div() {
            int b = GetB();
            Debug(variables[a] + " / " + b);
            if (b == 0) {
                invalid = true;
                return;
            }

            variables[a] /= b;
            Debug("= " + variables[a]);
        }
        void Mod() {
            int b = GetB();
            Debug(variables[a] + " % " + b);
            if (b <= 0) {
                invalid = true;
                return;
            }
            variables[a] %= GetB();
            Debug("= " + variables[a]);
        }
        void Eql() {
            Debug(variables[a] + " == " +  GetB());
            variables[a] = variables[a] == GetB() ? 1 : 0;
            Debug("= " + variables[a]);
        }

        void Debug(string message) {
            if (debug)
                print(message);
        }
    }

        // This challenge the hardest so far. I initially implemented all the ALU instructions
        // and then just tried to brute force all numbers to get the correct model number.
        // This was taking way too long, so I decided to look at the actual input instructions
        // to see if I could figure out what was going on at a higher level.

        // I tried going through it algebraically, substituting the variables for equations,
        // but it got way too complex and compounded into very large expressions.
        // I was seeing some patterns, but couldn't make any sense of them. It seemed like it
        // always boiled down to checking if the current value was equal to the current input,
        }

        for (int i = 1; i <= 9; i++) {
            for (int i2 = 1; i2 <= 9; i2++) {
                for (int i3 = 1; i3 <= 9; i3++) {
                    print((((i + 8) % 26) * 26) + ((i2 + 11) % 26) + ((i3 + 2) % 26) - 10);
                }
            }
        }

        bool debug = true;
        //EvaluateInstructions();
        //print(9 % 26);
        //ComputeModelString("11111111111111");
        //ComputeModelString("22222222222222");
        //ComputeModelString("33333333333333");
        //ComputeModelString("44444444444444");
        //ComputeModelString("55555555555555");
        //ComputeModelString("66666666666666");
        //ComputeModelString("77777777777777");
        //ComputeModelString("88888888888888");
        //ComputeModelString("99999999999999");

        void Debug(string message) {
            if (debug)
                print(message);
        }
        //ulong step = 11111111111111;
        //ulong max = 99999999999999;
        //ulong min = 55555555555555;


        //void EvaluateInstructions() {
        //    Instruction.Reset();
        //    foreach (Instruction i in instructions)
        //        i.Compute();

        //    foreach (List<int> l in Instruction.variables) {
        //        string s = "";
        //        foreach (int i in l)
        //            s += i + " | ";
        //        print(s);
        //    }
        //}


        //print((step / 2) + " " + (step / 2 + step % 2));
        //bool done = false;
        //while (!done) {

        //}

        //print(ComputeModelString("13579246899999"));
        //ComputeModelString("0");
        //ComputeModelString("1");
        //ComputeModelString("2");
        //ComputeModelString("3");
        //ComputeModelString("4");
        //ComputeModelString("5");
        //ComputeModelString("6");
        //ComputeModelString("7");
        //ComputeModelString("8");
        //print(ComputeModelNumbers());
        //string ComputeModelString(string num) {
        //    Debug("");
        //    Debug("");
        //    Debug("");
        //    Debug("Try: " + num);
        //    int[] intNums = new int[num.Length];
        //    for (int i = 0; i < num.Length; i++)
        //        intNums[i] = int.Parse(num[i] + "");
        //    return ComputeModelNumber(intNums);
        //}
        //    Queue<int> modelNumber = new Queue<int>();

        //    foreach (int num in nums)
        //        modelNumber.Enqueue(num);

        //    Instruction.variables = new int[4];
        //    Instruction.modelNumber = modelNumber;
        //    foreach (Instruction inst in instructions) {
        //        inst.Compute();
        //        if (Instruction.invalid)
        //            break;
        //    }

        //    if (Instruction.invalid) {
        //        Debug("Invalid");
        //        Instruction.invalid = false;
        //        return null;
        //    }
        //    Debug("Vars: " + Instruction.variables[0] + " " + Instruction.variables[1] + " " + Instruction.variables[2] + " " + Instruction.variables[3]);
        //    if (Instruction.variables[3] == 0) {
        //        string modelString = "";
        //        foreach (int num in nums)
        //            modelString += nums + "";
        //        return modelString;
        //    }
        //    return null;
        //}
        //    for (int a = 9; a >= 1; a--) {
        //        for (int b = 9; b >= 1; b--) {
        //            for (int c = 9; c >= 1; c--) {
        //                for (int d = 9; d >= 1; d--) {
        //                    for (int e = 9; e >= 1; e--) {
        //                        for (int f = 9; f >= 1; f--) {
        //                            for (int g = 9; g >= 1; g--) {
        //                                for (int h = 9; h >= 1; h--) {
        //                                    for (int i = 9; i >= 1; i--) {
        //                                        for (int j = 9; j >= 1; j--) {
        //                                            for (int k = 9; k >= 1; k--) {
        //                                                for (int l = 9; l >= 1; l--) {
        //                                                    for (int m = 9; m >= 1; m--) {
        //                                                        for (int n = 9; n >= 1; n--) {
        //                                                            Queue<int> modelNumber = new Queue<int>();
        //                                                            int[] nums = new int[] { a, b, c, d, e, f, g, h, i, j, k, l, m, n };
        //                                                            foreach (int num in nums)
        //                                                                modelNumber.Enqueue(num);

        //                                                            Instruction.variables = new int[4];
        //                                                            Instruction.modelNumber = modelNumber;
        //                                                            foreach (Instruction inst in instructions) {
        //                                                                inst.Compute();
        //                                                                if (Instruction.invalid)
        //                                                                    break;
        //                                                            }

        //                                                            if (Instruction.invalid) {
        //                                                                Instruction.invalid = false;
        //                                                                continue;
        //                                                            }

        //                                                            if (Instruction.variables[3] == 0) {
        //                                                                string modelString = "";
        //                                                                foreach (int num in nums)
        //                                                                    modelString += nums + "";
        //                                                                return modelString;
        //                                                            }
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
            //for (int a = 1; a <= 9; a++) {
            //    for (int b = 1; b <= 9; b++) {
            //        for (int c = 1; c <= 9; c++) {
            //            for (int d = 1; d <= 9; d++) {
            //                for (int e = 1; e <= 9; e++) {
            //                    for (int f = 1; f <= 9; f++) {
            //                        for (int g = 1; g <= 9; g++) {
            //                            for (int h = 1; h <= 9; h++) {
            //                                for (int i = 1; i <= 9; i++) {
            //                                    for (int j = 1; j <= 9; j++) {
            //                                        for (int k = 1; k <= 9; k++) {
            //                                            for (int l = 1; l <= 9; l++) {
            //                                                for (int m = 1; m <= 9; m++) {
            //                                                    for (int n = 1; n <= 9; n++) {
                                                                    
            //                                                    }
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        //}