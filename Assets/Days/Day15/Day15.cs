using System.Collections;

public class Day15 : MonoBehaviour {
    public int day = 15;
    public Part part;
    Color32 high = new Color32(0, 50, 0, 200);
    Color32 black = new Color32(255, 255, 255, 15);
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
    }

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
        // This means that the lower cost paths will always claim cells before higher cost ones, and the lowest cost path will always reach the
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
    }

        // For Part 2 the only addition was that the grid was multiplied in size and its risk levels increased in a certain pattern. 
        // I thought it wouldn't be too bad, but these next 13 lines of code took me another hour to figure out.
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

        print(totalRisk);
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
    }
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
    }
        return Color32.Lerp(low, high, GetRiskPercent(risk));
    }
        return (float)(risk - 1) / (maxRisk - 1);
    }