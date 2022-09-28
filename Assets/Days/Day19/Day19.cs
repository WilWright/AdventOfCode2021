using System.Collections;
    public int day = 19;
    public Part part;
        public GameObject scanner;
        public List<GameObject> beacons = new List<GameObject>();

        public ScannerObject(int index) {
            scanner = new GameObject("Scanner" + index);
        }

        public void AddBeacon(int[] coordinates) {
            GameObject beacon = new GameObject("Beacon" + beacons.Count);
            beacon.transform.SetParent(scanner.transform);
            float scale = 1;
            beacon.transform.localPosition = new Vector3(coordinates[0] / scale, coordinates[1] / scale, coordinates[2] / scale);
            beacons.Add(beacon);
        }

        public bool CompareBeacons(ScannerObject s, List<Vector3> rotations) {
            foreach (Vector3 r in rotations) {
                scanner.transform.eulerAngles = r;
                Vector3 vector = EvaluateDistances(s);
                if (vector != Vector3.zero) {
                    scanner.transform.position = vector;
                    return true;
                }
            }
            return false;
        }
        public Vector3 EvaluateDistances(ScannerObject s) {
            Dictionary<int, int> distanceCounts = new Dictionary<int, int>();
            foreach (GameObject a in beacons) {
                foreach (GameObject b in s.beacons) {
                    int distance = (int)Vector3.Distance(a.transform.position, b.transform.position);
                    try {
                        if (++distanceCounts[distance] >= 12)
                            return b.transform.position - a.transform.position;
                    }
                    catch { distanceCounts.Add(distance, 1); }
                }
            }
            return Vector3.zero;
        }
    }
        DestroyImmediate(scannerObjectsHolder);
        scannerObjectsHolder = new GameObject("ScannerObjects");
        scannerObjectsExample = new List<ScannerObject>();
        doneScannerObjectsExample = new List<ScannerObject>();

        string filePath = "Day" + day + "/" + "Day" + day;

        List<ScannerObject> scanners = new List<ScannerObject>();
            ScannerObject s = new ScannerObject(scanners.Count);
            while (!stringInput[i].StartsWith("--")) {
                s.AddBeacon(AdventHelper.GetIntInput(AdventHelper.ParseString(stringInput[i], false, '-')));
                if (++i >= stringInput.Length)
                    break;
            }
            scanners.Add(s);
        }

        foreach (ScannerObject s in scanners) {
            s.scanner.transform.SetParent(scannerObjectsHolder.transform);
            scannerObjectsExample.Add(s);
        }
        doneScannerObjectsExample.Add(scanners[0]);
        scannerObjectsExample.Remove(scanners[0]);
        currentScanner = 1;
    }
        rotations = new List<Vector3>();
        int[] angles = new int[] { 0, 90, 180, 270 };
        foreach (int x in angles) {
            foreach (int y in angles) {
                foreach (int z in angles)
                    rotations.Add(new Vector3(x, y, z));
            }
        }

        for (int i = 0; i < scannerObjectsExample.Count; i++) {
            foreach (ScannerObject b in doneScannerObjectsExample) {
                if (scannerObjectsExample[i].CompareBeacons(b, rotations)) {
                    print("Match " + doneScannerObjectsExample.Count + " " + scannerObjectsExample.Count);
                    doneScannerObjectsExample.Add(scannerObjectsExample[i]);
                    scannerObjectsExample.Remove(scannerObjectsExample[i]);
                    return;
                }
            }
        }
        print("No Match");
    }
        Dictionary<string, int> beaconLocations = new Dictionary<string, int>();
        foreach (ScannerObject so in doneScannerObjects) {
            foreach (GameObject go in so.beacons) {
                Vector3 pos = go.transform.position;
                string key = Mathf.RoundToInt(pos.x) + "," + Mathf.RoundToInt(pos.y) + "," + Mathf.RoundToInt(pos.z);
                try { beaconLocations.Add(key, 1); }
                catch { }
            }
        }
        print(beaconLocations.Count);
    }
        int largestDistance = 0;
        foreach (ScannerObject a in doneScannerObjects) {
            foreach (ScannerObject b in doneScannerObjects) {
                if (a == b)
                    continue;

                Vector3 aPos = a.scanner.transform.position;
                Vector3 bPos = b.scanner.transform.position;
                Vector3 manhatten = new Vector3(Mathf.Abs(aPos.x - bPos.x),
                                                Mathf.Abs(aPos.y - bPos.y),
                                                Mathf.Abs(aPos.z - bPos.z));
                int distance = Mathf.RoundToInt(manhatten.x + manhatten.y + manhatten.z);
                if (distance > largestDistance)
                    largestDistance = distance;
            }
        }
        print(largestDistance);
    }

        // I understoon what this challenge needed me to do, but I had a hard time coming up with
        // any solutions at first. I decided to manually play around with GameObjects, and rotate them
        // using the Transform component. I payed attention to the values in global and local form,
        // and noticed that overlapping objects that are children of parents with a different rotation
        // had the same values but in a different order (e.g. the x, y, z of Object 1 correlates with the y, z, x of Object 2).

        // Using this I came up with set patterns for each rotation, but it wasn't working. After some testing I realized my theory
        // wasn't true in all cases, and that my initial experimenting was just a coincidence with the patterns.
        // Later I realized that to get the right patterns, I needed to find the offsets from a single beacon to all others in order
        // to get the correct relative information. This still wasn't working.

        // I put that solution on hold and went back to using GameObjects. I set it up so that the input would generate all the
        // scanners for me, and then I would be able to rotate them by eye and find matches. This wasn't feasible though, because
        // pretty much every scanner had the same configuration of beacons with very slight variations. I guess the creators foresaw this brute force solution.
        // Doing this did help me debug my previous issues though. In the example, the scanners are all in order, so I thought they were next to each other,
        // but this wasn't the case (e.g I thought Scanner 0 was next to Scanner 1, which was next to Scanner 2, but Scanner 0 could be next to any other).
        // In order to get the correct offsets, instead of getting Scanner 0's offset to each, I would need every scanner's offsets to all other scanners.
        // Then I could find which sets of offsets match each other. I was finally able to get the solution, but at this point it was 4am, and I decided to go to bed
        // before starting Part 2.

        
        //print();
    }

        // Next day:
        // I realized the code still wouldn't work in all cases, and was just a coincidence with the example. I went back to using GameObjects
        // and tried to automate it this time instead of doing it by eye. If I rotated each scanner in all possible axis combinations
        // against every other scanner, I would eventually get a set of beacons that have the same distance values to each other. 
        // Doing all the scanners at once took really long and caused Unity to crash, so I had to run each scanner manually and wait for each one to finish.
        // Since I was now using GameObjects the Manhatten Distance was easy to calculate since I just had to offset the scanners after matching and use their
        // actual Transform position.

        //print();
    }