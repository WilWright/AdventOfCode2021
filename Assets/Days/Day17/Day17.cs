using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day17 : MonoBehaviour {
    public int day = 17;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // This challenge seemed daunting at first, but ended up being not so bad.
        // I wasn't sure if there was a way to calculate the highest y using some sort of quadratic function, 
        // and after coming up with nothing for a while I decided to simulate the probe given an initial velocity.

        // The actual simulation logic wasn't hard, I just implemented what the instructions said.
        // I simulated until the target was hit or either the x or y of the current position was past the target's east or south bounds,
        // because if it was the probe would never hit the target from that point.

        // I did have trouble generating the initial velocities, though. I knew for a fact that the inital x couldn't be more than        // the target's east bounds coordinate, because if it was that meant it would fly past it on the first step.        // The y velocity could have possibly been anything from 0 and up, or maybe there was an upper bound calculation I never thought of.        // After thinking about it for a long time I just decided to take the much simpler option, brute force it.        // I started with -100 to 100 to loop through for the y, and if the answer was wrong after I input it I would just increase the range.        // I only had to increase the range to 1000 and 10000 to get the right answer, and I got the same answer each time,        // which confirmed I only needed up to 1000. It only took a few more seconds to run each simulation anyway.        string[] input = AdventHelper.ParseString(stringInput[0], false, '-', '.', '=');        int[] bounds = new int[4];        string[] xValues = input[2].Split('=')[1].Split('.');        string[] yValues = input[3].Split('=')[1].Split('.');        bounds[0] = int.Parse(xValues[0]);        bounds[1] = int.Parse(xValues[2]);        bounds[2] = int.Parse(yValues[0]);        bounds[3] = int.Parse(yValues[2]);        int highestValue = 0;        for (int x = 0; x <= bounds[1]; x++) {
            for (int y = 0; y < 1000; y++)                Simulate(new Coordinates(x, y));
        }

        print(highestValue);        void Simulate(Coordinates velocity) {
            Coordinates pos = new Coordinates(0, 0);
            bool hitTarget = false;
            int highestY = 0;
            while (pos.x <= bounds[1] && pos.y >= bounds[2]) {
                pos += velocity;

                if (pos.y > highestY)
                    highestY = pos.y;

                if (WithinTarget(pos)) {
                    hitTarget = true;
                    break;
                }

                if (velocity.x > 0) velocity.x--;
                if (velocity.x < 0) velocity.x++;
                velocity.y--;
            }
            if (hitTarget) {
                if (highestY > highestValue)
                    highestValue = highestY;
            }
        }        bool WithinTarget(Coordinates c) {
            return c.x >= bounds[0] && c.x <= bounds[1] && c.y >= bounds[2] && c.y <= bounds[3];
        }    }    void Part2() {        print("Part 2");        // After spending 1 hour and 15 minutes on Part 1, Part 2 only took another 10 minutes.        // My logic was pretty much already done, I just needed to keep track of the velocities        // when I hit the target. I did have to add -1000 to the y generation though,        // because instead of getting the highest y position, I needed to know all velocities that hit the target.        string[] input = AdventHelper.ParseString(stringInput[0], false, '-', '.', '=');        int[] bounds = new int[4];        string[] xValues = input[2].Split('=')[1].Split('.');        string[] yValues = input[3].Split('=')[1].Split('.');        bounds[0] = int.Parse(xValues[0]);        bounds[1] = int.Parse(xValues[2]);        bounds[2] = int.Parse(yValues[0]);        bounds[3] = int.Parse(yValues[2]);

        int initialVelocities = 0;        for (int x = 0; x <= bounds[1]; x++) {
            for (int y = -1000; y < 1000; y++)                Simulate(new Coordinates(x, y));
        }

        print(initialVelocities);        void Simulate(Coordinates velocity) {
            Coordinates initialVel = velocity;
            Coordinates pos = new Coordinates(0, 0);
            while (pos.x <= bounds[1] && pos.y >= bounds[2]) {
                pos += velocity;

                if (WithinTarget(pos)) {
                    initialVelocities++;
                    break;
                }

                if (velocity.x > 0) velocity.x--;
                if (velocity.x < 0) velocity.x++;
                velocity.y--;
            }
        }        bool WithinTarget(Coordinates c) {
            return c.x >= bounds[0] && c.x <= bounds[1] && c.y >= bounds[2] && c.y <= bounds[3];
        }
    }}