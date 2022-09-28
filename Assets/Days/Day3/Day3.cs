using System.Collections;
    public int day = 3;
    public Part part;
            for (int i = 0; i < s.Length; i++) {
                switch (s[i]) {
                    case '1': gamma[i]++; break;
                    case '0': gamma[i]--; break;
                }
            }
        }
            if (gamma[i] > 0) { gamma[i] = 1; epsilon[i] = 0; }
            else              { gamma[i] = 0; epsilon[i] = 1; }
        }
            int power = (int)Mathf.Pow(2, bitCount - i - 1);
            decGamma += gamma[i] * power;
            decEps += epsilon[i] * power;
        }

        print(decGamma*decEps);
    }

        // Part 2 added quite a lot of complexity to the challenge, needing us to filter down
        // the bit strings based on Part 1's criteria of most/least common bits for 
        // oxygen generator and CO2 scrubber ratings.
            scrubber.Add(s);
        }
            if (oxygen.Count == 1 && scrubber.Count == 1)
                break;

            List<string> oxygenOnes = new List<string>();
            List<string> oxygenZeros = new List<string>();
            int oxyScore = 0;
            foreach (string s in oxygen) {
                switch (s[i]) {
                    case '1': oxyScore++; oxygenOnes.Add(s); break;
                    case '0': oxyScore--; oxygenZeros.Add(s); break;
                }
            }
            if (oxygen.Count > 1) oxygen = oxyScore >= 0 ? oxygenOnes : oxygenZeros;

            List<string> scrubberOnes = new List<string>();
            List<string> scrubberZeros = new List<string>();
            int scrubScore = 0;
            foreach (string s in scrubber) {
                switch (s[i]) {
                    case '1': scrubScore++; scrubberOnes.Add(s); break;
                    case '0': scrubScore--; scrubberZeros.Add(s); break;
                }
            }
            if (scrubber.Count > 1) scrubber = scrubScore >= 0 ? scrubberZeros : scrubberOnes;
        }
            int power = (int)Mathf.Pow(2, bitCount - i - 1);
            decOxy += int.Parse(oxygen[0][i] + "") * power;
            decScrub += int.Parse(scrubber[0][i] + "") * power;
        }
        print(decOxy*decScrub);
    }