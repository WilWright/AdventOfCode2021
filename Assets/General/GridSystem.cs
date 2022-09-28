using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem<T> {
    public T[,] grid;
    public Coordinates[] coordinates;

    public GridSystem(int width, int height) {
        grid = new T[width, height];
        SetCoordinates();
    }
    
    public static GridSystem<string> GetStringGrid(string[] stringInput) {
        GridSystem<string> stringGrid = new GridSystem<string>(stringInput[0].Length, stringInput.Length);
        for (int y = stringGrid.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < stringGrid.Width(); x++)
                stringGrid.Set(new Coordinates(x, y), stringInput[stringGrid.Height() - 1 - y][x] + "");
        }
        stringGrid.SetCoordinates();
        return stringGrid;
    }
    public static GridSystem<string> GetStringGrid(string[][] stringInput) {
        GridSystem<string> stringGrid = new GridSystem<string>(stringInput[0].Length, stringInput.Length);
        for (int y = stringGrid.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < stringGrid.Width(); x++)
                stringGrid.Set(new Coordinates(x, y), stringInput[stringGrid.Height() - 1 - y][x] + "");
        }
        stringGrid.SetCoordinates();
        return stringGrid;
    }
    public static GridSystem<int> GetIntGrid(string[] stringInput) {
        GridSystem<int> intGrid = new GridSystem<int>(stringInput[0].Length, stringInput.Length);
        for (int y = intGrid.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < intGrid.Width(); x++)
                intGrid.Set(new Coordinates(x, y), int.Parse(stringInput[intGrid.Height() - 1 - y][x] + ""));
        }
        intGrid.SetCoordinates();
        return intGrid;
    }
    public static GridSystem<int> GetIntGrid(string[][] stringInput) {
        GridSystem<int> intGrid = new GridSystem<int>(stringInput[0].Length, stringInput.Length);
        for (int y = intGrid.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < intGrid.Width(); x++)
                intGrid.Set(new Coordinates(x, y), int.Parse(stringInput[intGrid.Height() - 1 - y][x] + ""));
        }
        intGrid.SetCoordinates();
        return intGrid;
    }

    public int Width()  { return grid.GetLength(0); }
    public int Height() { return grid.GetLength(1); }
    
    public T Get(Coordinates c) {
        return grid[c.x, c.y];
    }
    public void Set(Coordinates c, T element) {
        grid[c.x, c.y] = element;
    }
    public T RemoveAt(Coordinates c) {
        T element = Get(c);
        Set(c, default);
        return element;
    }

    public bool WithinBounds(Coordinates c) {
        return c.x >= 0 && c.x < Width() && c.y >= 0 && c.y < Height();
    }
    
    void SetCoordinates() {
        List<Coordinates> coordinates = new List<Coordinates>();
        for (int y = Height() - 1; y >= 0; y--) {
            for (int x = 0; x < Width(); x++)
                coordinates.Add(new Coordinates(x, y));
        }

        this.coordinates = coordinates.ToArray();
    }
}
