using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Part  { Part1  , Part2 }
public enum File  { Example, Main  }

public class AdventHelper : MonoBehaviour {
    public static string[] GetStringInput(string file, string path = "Inputs/") {
        TextAsset text = Resources.Load(path + file) as TextAsset;
        string[] stringInput = GetLines(text);
        List<string> filteredInput = new List<string>();
        foreach (string s in stringInput) {
            if (s.Length > 0)
                filteredInput.Add(s);
        }
        return filteredInput.ToArray();
    }
    public static int[] GetIntInput(string[] stringInput) {
        int[] intInput = new int[stringInput.Length];
        for (int i = 0; i < stringInput.Length; i++)
            intInput[i] = int.Parse(stringInput[i]);
        return intInput;
    }
    public static int[][] GetIntInput(string[][] stringInput) {
        int[][] intInput = new int[stringInput.Length][];
        for (int i = 0; i < stringInput.Length; i++)
            intInput[i] = GetIntInput(stringInput[i]);
        return intInput;
    }
    static string[] GetLines(TextAsset text) {
        string[] lines = text.text.Split('\r');
        for (int i = 0; i < lines.Length; i++)
            lines[i] = lines[i].Replace("\n", string.Empty);
        return lines;
    }

    public static string[] ParseString(string s, bool parseCharacters = false, params char[] exceptions) {
        List<string> strings = new List<string>();
        string current = "";
        for (int i = 0; i < s.Length; i++) {
            char c = s[i];
            bool valid = false;
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                valid = true;
            else {
                foreach (char e in exceptions) {
                    if (c == e) {
                        valid = true;
                        break;
                    }
                }
            }

            if (parseCharacters) {
                if (valid)
                    strings.Add(c + "");
                continue;
            }

            if (!valid) {
                if (current.Length > 0)
                    strings.Add(current);
                current = "";
            }
            else
                current += c;
        }
        if (current.Length > 0)
            strings.Add(current);

        return strings.ToArray();
    }
    public static string[][] ParseInput(string[] stringInput, bool parseCharacters = false, params char[] exceptions) {
        string[][] strings = new string[stringInput.Length][];
        for (int i = 0; i < stringInput.Length; i++)
            strings[i] = ParseString(stringInput[i], parseCharacters, exceptions);
        return strings;
    }

    public static string JoinStrings(params string[] strings) {
        return JoinStrings("", strings);
    }
    public static string JoinStrings(string delimiter, params string[] strings) {
        string joined = "";
        for (int i = 0; i < strings.Length; i++) {
            joined += strings[i];
            if (i < strings.Length - 1)
                joined += delimiter;
        }
        return joined;
    }

    public static int Sum(params int[] nums) {
        int sum = 0;
        foreach (int i in nums)
            sum += i;
        return sum;
    }
    public static long Sum(params long[] nums) {
        long sum = 0;
        foreach (long i in nums)
            sum += i;
        return sum;
    }
    public static int Product(params int[] nums) {
        int product = 1;
        foreach (int i in nums)
            product *= i;
        return product;
    }
    public static long Product(params long[] nums) {
        long product = 1;
        foreach (long i in nums)
            product *= i;
        return product;
    }

    [MenuItem("AdventHelper/CreateDays")]
    public static void CreateDays() {
        string[] templateText = GetLines(Resources.Load("DayTemplate") as TextAsset);
        for (int i = 1; i < 26; i++) {
            int dayCount = i;

            string newDay = "Day" + dayCount;
            AssetDatabase.CreateFolder("Assets/Days", newDay);
            string dayPath = "Assets/Days/Day" + dayCount + "/" + newDay;

            var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(newScene, dayPath + ".unity");
            
            StreamWriter writer = System.IO.File.CreateText(dayPath + ".cs");
            foreach (string s in templateText) {
                string text = s;
                if (s.Length > 1) {
                    switch (s.Substring(s.Length - 2, 1)) {
                        case "@": text = "public class " + newDay + " : MonoBehaviour {\n"; break;
                        case "#": text = "    public int day" + " = " + dayCount + ";\n";   break;
                    }
                }
                writer.Write(text);
            }
            writer.Flush();
            writer.Close();

            AssetDatabase.CreateFolder("Assets/Resources/Inputs", newDay);
            string inputPath = "Assets/Resources/Inputs/" + newDay + "/" + newDay;
            AssetDatabase.CreateAsset(new TextAsset(), inputPath + "Example.txt");
            AssetDatabase.CreateAsset(new TextAsset(), inputPath +        ".txt");
        }
        AssetDatabase.Refresh();
    }

    [MenuItem("AdventHelper/AttachScripts")]
    public static void AttachScripts() {
        for (int i = 1; i < 26; i++) {
            string day = "Day" + i;
            Scene scene = EditorSceneManager.OpenScene("Assets/Days/" + day + "/" + day + ".unity");
            Camera.main.gameObject.AddComponent(Type.GetType(day));
            EditorSceneManager.SaveScene(scene);
        }
    }

    class Time {
        int hours;
        int minutes;
        int seconds;

        public Time(string hours, string minutes, string seconds) {
            this.hours   = int.Parse(hours  );
            this.minutes = int.Parse(minutes);
            this.seconds = int.Parse(seconds);
        }

        public void Subtract(Time time) {
            hours -= time.hours;

            if (minutes < time.minutes) {
                hours--;

                minutes += 60 - time.minutes;
            }
            else
                minutes -= time.minutes;

            if (seconds < time.seconds) {
                minutes--;

                seconds += 60 - time.seconds;
            }
            else
                seconds -= time.seconds;
        }

        public override string ToString() {
            return Format(hours) + "h " + Format(minutes) + "m " + Format(seconds) + "s";

            string Format(int value) {
                return value < 10 ? "0" + value : value + ""; 
            }
        }
    }

    [MenuItem("AdventHelper/GetStats")]
    public static void GetStats() {
        string[][] stats = ParseInput(GetStringInput("Stats", ""), false, '-');
        string[][] sortedStats = new string[stats.Length][];
        for (int i = 0; i < stats.Length; i++)
            sortedStats[i] = stats[stats.Length - 1 - i];


        string d = "|";
        string output = "";
        int[] ranks = new int[sortedStats.Length];
        for (int i = 0; i < sortedStats.Length; i++) {
            string[] s = sortedStats[i];

            string day = s[0];
            Time time1 = new Time(s[1], s[2], s[3]);
            Time time2 = new Time(s[6], s[7], s[8]);
            int rank1 = int.Parse(s[4]);
            int rank2 = int.Parse(s[9]);
            ranks[i] = rank2;

            output += d + "[" + day + "](https://adventofcode.com/2021/day/" + day + ")" + d + "[Day" + day + "](http://raw.githubusercontent.com/WilWright/AdventOfCode2021/master/Assets/Days/Day" + day + "/Day" + day + ".cs)" + d;
            output += "[Input](https://raw.githubusercontent.com/WilWright/AdventOfCode2021/master/Assets/Resources/Inputs/Day" + day + "/Day" + day + ".txt)" + d;

            string total = time2.ToString();
            time2.Subtract(time1);
            output += "<table><thead><tr><th>Part 1</th><th>Part 2</th><th>Total</th></tr></thead><tbody><tr><td>" + time1.ToString() + "</td><td>" + time2.ToString() + "</td><td>";
            output += total + "</td></tr></tbody></table>" + d;

            output += "<table><thead><tr><th>Part 1</th><th>Part 2</th></tr></thead><tbody><tr><td>" + rank1 + "</td><td>" + rank2 + "</td></tr></tbody></table>" + d;
            if (rank1 < rank2) output += "+";
            output += (rank2 - rank1) + d;

            output += "\n";
        }

        int[] allRanks = GetIntInput(GetStringInput("Ranks", ""));
        int[] sortedAllRanks = new int[allRanks.Length];
        for (int i = 0; i < allRanks.Length; i++)
            sortedAllRanks[i] = allRanks[allRanks.Length - 1 - i];

        for (int i = 0; i < sortedAllRanks.Length; i++)
            ranks[i] = Mathf.RoundToInt((float)ranks[i] / sortedAllRanks[i] * 100);

        int best = 100;
        int bestDay = 0;
        int avg = 0;
        for (int i = 0; i < ranks.Length; i++) {
            if (ranks[i] <= best) {
                best = ranks[i];
                bestDay = i + 1;
            }
            avg += i;
        }
        avg = Mathf.RoundToInt((float)avg / ranks.Length);

        print("Best: " + best + " (Day " + bestDay + ")");
        print("Average: " + avg);
        print(output);
    }
}
