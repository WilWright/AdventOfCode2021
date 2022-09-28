using System.Collections;using System.Collections.Generic;using UnityEngine;using UnityEngine.UI;

public class Day15 : MonoBehaviour {
    public int day = 15;
    public Part part;    public File file;    public bool convertInputToInt;    string[] stringInput;    int[] intInput;            public RawImage image;    public float waitTime;    Texture2D texture;    Color32 low  = new Color32(0, 200, 0, 200);
    Color32 high = new Color32(0, 50, 0, 200);    Color32 white = new Color32(255, 255, 255, 200);
    Color32 black = new Color32(255, 255, 255, 15);    int fades = 0;    public int threshhold = 5;    public int maxRisk = 9;    public Coordinates startPoint;    public Coordinates endPoint;    class SearchNode {
        public Coordinates coordinates;
        public SearchNode parent;
        public int risk;
        public int wait;
        public int kills;

        public SearchNode(SearchNode parent, Coordinates coordinates, int risk) {
            this.parent = parent;
            this.coordinates = coordinates;
            this.risk = wait = risk;
        }

        public void KillPath() {
            if (++kills >= 4 && parent != null)
                parent.KillPath();
        }
    }    [ContextMenu("Start")]    void Start() {        string filePath = "Day" + day + "/" + "Day" + day;        switch (file) {            case File.Example: stringInput = AdventHelper.GetStringInput(filePath + "Example"); waitTime *= 0.1f;   break;            case File.Main   : stringInput = AdventHelper.GetStringInput(filePath            ); waitTime *= 0.001f; break;        }        if (convertInputToInt)            intInput = AdventHelper.GetIntInput(stringInput);        if (UnityEditor.EditorApplication.isPlaying)            StartCoroutine(Visualize(stringInput));        switch (part) {            case Part.Part1: Part1(); break;            case Part.Part2: Part2(); break;        }        print("");    }    void Part1() {        print("Part 1");

        // I was a little worried about this challenge because it was all about path finding,
        // and I haven't had any experience implementing something like that before.
        // I tried to think of my own solution at first, but after blanking for 30 minutes I decided to look up A*.

        // After a bit of reading I could see it would take a long time for me to fully understand the algorithm
        // and be able to implement it. Things like a priority queue and heuristic search seemed complicated.
        // Skimming over these concepts got my gears turning however, and I was able to come up with something else.

        // I don't know if I ended up doing essentially what A* does, but this is what I got:
        // I would begin with the starting position and search adjacent cells. If those cells were not yet claimed,
        // I add a new node to search with on the next step. Each node keeps track of its cell's location and risk level,
        // as well as the node that created it. In order to maintain the cost of the path, new nodes are set to wait for
        // however many steps the risk level is. Every step I decrement all current node waits by 1, and when they reach 0
        // they are ready to search their adjacent cells. If they are still waiting, they just get added back into the next step search.
        // This means that the lower cost paths will always claim cells before higher cost ones, and the lowest cost path will always reach the        // end goal first. If all of a node's adjacent cells are claimed, then it just doesn't get added back into the search list, which means I don't        // have to worry about updating it anymore and that path essentially dies.        // After the end cell is found, I can back track through the chain of nodes that was built up for that path and calculate the total risk level.        // I thought I would be going against a lot of participants who were familiar with pathfinding, but after 1 hour and 30 minutes I still got a decent rank of 5,140.        GridSystem<int> grid = GridSystem<int>.GetIntGrid(stringInput);
        GridSystem<bool> claims = new GridSystem<bool>(grid.Width(), grid.Height());
        List<SearchNode> searchNodes = new List<SearchNode>();
        Coordinates start = new Coordinates(0, grid.Height() - 1);
        Coordinates end = new Coordinates(grid.Width() - 1, 0);
        searchNodes.Add(new SearchNode(null, start, 0));
        claims.Set(start, true);

        SearchNode endNode = null;
        while (endNode == null) {
            List<SearchNode> nextNodes = new List<SearchNode>();
            foreach (SearchNode sn in searchNodes) {
                if (--sn.wait <= 0) {
                    if (sn.coordinates == end) {
                        endNode = sn;
                        break;
                    }
                    foreach (Coordinates c in Coordinates.CompassDirection) {
                        Coordinates nextC = sn.coordinates + c;
                        if (grid.WithinBounds(nextC) && !claims.Get(nextC)) {
                            claims.Set(nextC, true);
                            nextNodes.Add(new SearchNode(sn, nextC, grid.Get(nextC)));
                        }
                    }
                }
                else
                    nextNodes.Add(sn);
            }
            searchNodes = nextNodes;
        }

        SearchNode nextNode = endNode;
        int totalRisk = 0;
        while (nextNode.parent != null) {
            totalRisk += nextNode.risk;
            nextNode = nextNode.parent;
        }

        print(totalRisk);
    }    void Part2() {        print("Part 2");

        // For Part 2 the only addition was that the grid was multiplied in size and its risk levels increased in a certain pattern. 
        // I thought it wouldn't be too bad, but these next 13 lines of code took me another hour to figure out.        // It just took me a long time to get the right math down, with a lot of debugging in between.        // The rest of the logic afterward didn't need to be changed. I was worried that my solution might be slow since it wasn't        // a tried and true algorithm like A*, but even with a 500 by 500 grid size it was able to get the answer in less than a second.        GridSystem<int> pregrid = GridSystem<int>.GetIntGrid(stringInput);
        GridSystem<int> grid = new GridSystem<int>(pregrid.Width() * 5, pregrid.Height() * 5);
        foreach (Coordinates c in pregrid.coordinates) {
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    Coordinates newC = new Coordinates(c.x + x * pregrid.Width(), c.y + y * pregrid.Height());
                    int newRisk = pregrid.Get(c) + x + (4 - y);
                    if (newRisk > 9)
                        newRisk %= 9;
                    grid.Set(newC, newRisk);
                }
            }
        }

        GridSystem<bool> claims = new GridSystem<bool>(grid.Width(), grid.Height());
        List<SearchNode> searchNodes = new List<SearchNode>();
        Coordinates start = new Coordinates(0, grid.Height() - 1);
        Coordinates end = new Coordinates(grid.Width() - 1, 0);
        searchNodes.Add(new SearchNode(null, start, 0));
        claims.Set(start, true);

        SearchNode endNode = null;
        while (endNode == null) {
            List<SearchNode> nextNodes = new List<SearchNode>();
            foreach (SearchNode sn in searchNodes) {
                if (--sn.wait <= 0) {
                    if (sn.coordinates == end) {
                        endNode = sn;
                        break;
                    }
                    foreach (Coordinates c in Coordinates.CompassDirection) {
                        Coordinates nextC = sn.coordinates + c;
                        if (grid.WithinBounds(nextC) && !claims.Get(nextC)) {
                            claims.Set(nextC, true);
                            nextNodes.Add(new SearchNode(sn, nextC, grid.Get(nextC)));
                        }
                    }
                }
                else
                    nextNodes.Add(sn);
            }
            searchNodes = nextNodes;
        }

        SearchNode nextNode = endNode;
        int totalRisk = 0;
        while (nextNode.parent != null) {
            totalRisk += nextNode.risk;
            nextNode = nextNode.parent;
        }

        print(totalRisk);    }    IEnumerator Visualize(string[] stringInput) {
        GridSystem<int> pregrid = GridSystem<int>.GetIntGrid(stringInput);
        GridSystem<int> grid = new GridSystem<int>(pregrid.Width() * 5, pregrid.Height() * 5);
        texture = new Texture2D(grid.Width(), grid.Height());
        texture.filterMode = FilterMode.Point;
        image.texture = texture;
        
        foreach (Coordinates c in pregrid.coordinates) {
            for (int y = 0; y < 5; y++) {
                for (int x = 0; x < 5; x++) {
                    Coordinates newC = new Coordinates(c.x + x * pregrid.Width(), c.y + y * pregrid.Height());
                    int newRisk = pregrid.Get(c) + x + (4 - y);
                    if (newRisk > 9)
                        newRisk %= 9;

                    if (maxRisk > 9) {
                        if (newRisk > threshhold)
                            newRisk = maxRisk;
                    }

                    grid.Set(newC, newRisk);
                    texture.SetPixel(newC.x, newC.y, GetRiskColor(newRisk));
                }
            }
        }
        texture.Apply();

        GridSystem<bool> claims = new GridSystem<bool>(grid.Width(), grid.Height());
        List<SearchNode> searchNodes = new List<SearchNode>();
        Coordinates start = new Coordinates((int)Mathf.Lerp(0, grid.Width() - 1, (float)startPoint.x / 100), (int)Mathf.Lerp(0, grid.Height() - 1, (float)startPoint.y / 100));
        Coordinates end   = new Coordinates((int)Mathf.Lerp(0, grid.Width() - 1, (float)endPoint  .x / 100), (int)Mathf.Lerp(0, grid.Height() - 1, (float)endPoint  .y / 100));
        searchNodes.Add(new SearchNode(null, start, 0));
        searchNodes.Add(new SearchNode(null, new Coordinates(0, 0), 0));
        searchNodes.Add(new SearchNode(null, new Coordinates(499, 0), 0));
        searchNodes.Add(new SearchNode(null, new Coordinates(499, 499), 0));
        claims.Set(start, true);

        Color32 waitLow  = new Color32(0, 200, 246, 255);
        Color32 waitHigh = new Color32(0, 30, 50, 255);

        SearchNode endNode = null;
        while (endNode == null) {
            List<SearchNode> nextNodes = new List<SearchNode>();
            foreach (SearchNode sn in searchNodes) {
                if (--sn.wait <= 0) {
                    texture.SetPixel(sn.coordinates.x, sn.coordinates.y, white);
                    if (sn.coordinates == end)
                        endNode = sn;

                    bool none = true;
                    foreach (Coordinates c in Coordinates.CompassDirection) {
                        Coordinates nextC = sn.coordinates + c;
                        if (grid.WithinBounds(nextC) && !claims.Get(nextC)) {
                            none = false;
                            claims.Set(nextC, true);
                            nextNodes.Add(new SearchNode(sn, nextC, grid.Get(nextC)));
                        }
                        else
                            sn.KillPath();
                    }
                    if (none && endNode != sn)
                        FadeNode(sn);
                }
                else {
                    texture.SetPixel(sn.coordinates.x, sn.coordinates.y, Color32.Lerp(waitLow, waitHigh, GetRiskPercent(sn.wait)));
                    nextNodes.Add(sn);
                }
            }

            searchNodes = nextNodes;
            texture.Apply();
            yield return new WaitForSeconds(waitTime);
        }

        SearchNode nextNode = endNode;
        while (nextNode.parent != null) {
            nextNode.kills = -50;
            texture.SetPixel(nextNode.coordinates.x, nextNode.coordinates.y, GetRiskColor(nextNode.risk));
            nextNode = nextNode.parent;
        }

        foreach (SearchNode sn in searchNodes) {
            for (int i = 0; i < 4; i++)
                sn.KillPath();
            FadeNode(sn);
        }
        texture.SetPixel(end.x, end.y, Color.white);
        texture.Apply();

        while (fades > 0) {
            texture.Apply();
            yield return new WaitForSeconds(waitTime);
        }
    }    void FadeNode(SearchNode searchNode) {
        SearchNode nextNode = searchNode;
        float offset = 0;
        while (nextNode.parent != null) {
            if (nextNode.kills < 4)
                break;

            StartCoroutine(Fade(nextNode.coordinates, nextNode.risk, offset));
            offset += 0.02f;
            nextNode = nextNode.parent;
        }

        IEnumerator Fade(Coordinates c, int risk, float waitOffset) {
            fades++;
            yield return new WaitForSeconds(waitOffset);

            Color32 color = GetRiskColor(risk);
            float time = 0;
            float speed = 1f;
            while (time < 1) {
                time += Time.deltaTime * speed;
                Color32 fade = Color32.Lerp(color, black, time);
                texture.SetPixel(c.x, c.y, fade);
                yield return null;
            }

            fades--;
        }
    }    Color32 GetRiskColor(int risk) {
        return Color32.Lerp(low, high, GetRiskPercent(risk));
    }    float GetRiskPercent(int risk) {
        return (float)(risk - 1) / (maxRisk - 1);
    }}