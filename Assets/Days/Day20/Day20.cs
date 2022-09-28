using System.Collections;using System.Collections.Generic;using UnityEngine;public class Day20 : MonoBehaviour {
    public int day = 20;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // I understood the initial premise of this challenge pretty well. I just needed to pad the edge of the grid
        // to get the necessary data for the current in-bounds pixels, and any out of bounds pixels could be treated
        // the same as unlit pixels. There was a trick in the input however. A group of 3x3 unlit pixels would be enhanced
        // to a lit pixel, meaning all of the infinite out of bounds pixels would need to change to be lit,
        // which would then need to be evaluated for the next enhancement. I thought there must be a mistake in my input 
        // that was overlooked as a possibility, so I checked what other participants were discussing about this day online.        // There were others similarly confused as me, but more astute participants noted that this was intended for all inputs,        // and that a 3x3 group of lit pixels would also change back to unlit. So instead of there being crazy perturbations in the        // infinite background, it would uniformly swap between lit and unlit, while the original input would enhance as normal.        // After getting this hint I was able to get the solution pretty fast. I just had to swap what the bounds were represented by        // after each enhancement. Without that hint though, I probably would have been stuck for many more hours.        string algorithm = stringInput[0];        GridSystem<bool> grid = new GridSystem<bool>(stringInput[2].Length, stringInput.Length - 1);        string[] imageInput = new string[stringInput.Length - 1];        for (int i = 1; i < stringInput.Length; i++)
            imageInput[i - 1] = stringInput[i];        GridSystem<string> tempGrid = GridSystem<string>.GetStringGrid(imageInput);        foreach (Coordinates c in tempGrid.coordinates)            grid.Set(c, tempGrid.Get(c) == "#");        Coordinates[] dd = Coordinates.DiagonalDirection;        Coordinates[] cd = Coordinates.CompassDirection;        Coordinates[] samples = new Coordinates[] { dd[0], cd[0], dd[1], cd[2], new Coordinates(0, 0), cd[3], dd[2], cd[1], dd[3] };        grid = PadImage(grid, false);        grid = Enhance(grid, false);        grid = PadImage(grid, true);        grid = Enhance(grid, true);        int litCount = 0;        foreach (Coordinates c in grid.coordinates) {
            if (grid.Get(c))
                litCount++;
        }        print(litCount);        GridSystem<bool> Enhance(GridSystem<bool> image, bool outOfBoundsIsLit) {
            GridSystem<bool> nextImage = new GridSystem<bool>(image.Width(), image.Height());            List<Coordinates>[] additionalPixels = new List<Coordinates>[2];
            additionalPixels[0] = new List<Coordinates>();
            additionalPixels[1] = new List<Coordinates>();

            foreach (Coordinates c in image.coordinates) {
                string binary = "";
                foreach (Coordinates s in samples) {
                    Coordinates sample = c + s;
                    if (image.WithinBounds(sample))
                        binary += image.Get(sample) ? "1" : "0";
                    else
                        binary += outOfBoundsIsLit ? "1" : "0";
                }
                int index = System.Convert.ToInt32(binary, 2);
                nextImage.Set(c, algorithm[index] == '#');
            }
            return nextImage;
        }        GridSystem<bool> PadImage(GridSystem<bool> image, bool fillWith) {
            GridSystem<bool> newImage = new GridSystem<bool>(image.Width() + 4, image.Height() + 4);
            foreach (Coordinates c in newImage.coordinates)
                newImage.Set(c, fillWith);

            Coordinates offset = new Coordinates(2, 2);
            foreach (Coordinates c in image.coordinates)
                newImage.Set(c + offset, image.Get(c));

            return newImage;
        }        void OutputImage(GridSystem<bool> image) {
            for (int y = image.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < image.Width(); x++)
                    s += image.Get(new Coordinates(x, y)) ? "#" : " . ";
                print(s);
            }
        }    }    void Part2() {        print("Part 2");        // The new rules in Part 2 just added more steps, so instead of manually doing each step        // I just had to loop through them and set the infinite bounds to be lit based on whether the step        // is odd or even. I was able to complete this part in under 4 minutes!        string algorithm = stringInput[0];        GridSystem<bool> grid = new GridSystem<bool>(stringInput[2].Length, stringInput.Length - 1);        string[] imageInput = new string[stringInput.Length - 1];        for (int i = 1; i < stringInput.Length; i++)
            imageInput[i - 1] = stringInput[i];        GridSystem<string> tempGrid = GridSystem<string>.GetStringGrid(imageInput);        foreach (Coordinates c in tempGrid.coordinates)            grid.Set(c, tempGrid.Get(c) == "#");        Coordinates[] dd = Coordinates.DiagonalDirection;        Coordinates[] cd = Coordinates.CompassDirection;        Coordinates[] samples = new Coordinates[] { dd[0], cd[0], dd[1], cd[2], new Coordinates(0, 0), cd[3], dd[2], cd[1], dd[3] };        int steps = 50;        for (int i = 0; i < steps; i++) {
            bool swapLit = i % 2 == 1;
            grid = PadImage(grid, swapLit);            grid = Enhance(grid, swapLit);
        }        int litCount = 0;        foreach (Coordinates c in grid.coordinates) {
            if (grid.Get(c))
                litCount++;
        }        print(litCount);        GridSystem<bool> Enhance(GridSystem<bool> image, bool outOfBoundsIsLit) {
            GridSystem<bool> nextImage = new GridSystem<bool>(image.Width(), image.Height());            List<Coordinates>[] additionalPixels = new List<Coordinates>[2];
            additionalPixels[0] = new List<Coordinates>();
            additionalPixels[1] = new List<Coordinates>();

            foreach (Coordinates c in image.coordinates) {
                string binary = "";
                foreach (Coordinates s in samples) {
                    Coordinates sample = c + s;
                    if (image.WithinBounds(sample))
                        binary += image.Get(sample) ? "1" : "0";
                    else
                        binary += outOfBoundsIsLit ? "1" : "0";
                }
                int index = System.Convert.ToInt32(binary, 2);
                nextImage.Set(c, algorithm[index] == '#');
            }
            return nextImage;
        }        GridSystem<bool> PadImage(GridSystem<bool> image, bool fillWith) {
            GridSystem<bool> newImage = new GridSystem<bool>(image.Width() + 4, image.Height() + 4);
            foreach (Coordinates c in newImage.coordinates)
                newImage.Set(c, fillWith);

            Coordinates offset = new Coordinates(2, 2);
            foreach (Coordinates c in image.coordinates)
                newImage.Set(c + offset, image.Get(c));

            return newImage;
        }        void OutputImage(GridSystem<bool> image) {
            for (int y = image.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < image.Width(); x++)
                    s += image.Get(new Coordinates(x, y)) ? "#" : " . ";
                print(s);
            }
        }

        //print();
    }}