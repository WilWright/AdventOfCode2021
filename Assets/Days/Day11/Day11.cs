using System.Collections;
    public int day = 11;
    public Part part;
        for (int y = octopi.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < octopi.Width(); x++)
            foreach (Coordinates c in octopi.coordinates)
                Increase(c);
            foreach (Coordinates c in octopi.coordinates)
                CheckReset(c);

            bool all = true;
            foreach (Coordinates c in octopi.coordinates) {
                if (octopi.Get(c) != 0) {
                    all = false;
                    break;
                }
            }
            if (all)
                print(step);
        }
            if (!octopi.WithinBounds(c))
                return;

            int energy = octopi.Get(c);
            energy++;
            octopi.Set(c, energy);

            if (energy == 10) {
                flashes++;
                foreach (Coordinates d in Coordinates.AllDirection)
                    Increase(c + d);
            }
        }
        void CheckReset(Coordinates c) {
            if (octopi.Get(c) > 9)
                octopi.Set(c, 0);
        }
        for (int y = octopi.Height() - 1; y >= 0; y--) {
            for (int x = 0; x < octopi.Width(); x++)
            foreach (Coordinates c in octopi.coordinates)
                Increase(c);
            foreach (Coordinates c in octopi.coordinates)
                CheckReset(c);

            all = true;
            foreach (Coordinates c in octopi.coordinates) {
                if (octopi.Get(c) != 0) {
                    all = false;
                    break;
                }
            }
            if (all)
                print(step + 1);
        }
            if (!octopi.WithinBounds(c))
                return;

            int energy = octopi.Get(c);
            energy++;
            octopi.Set(c, energy);

            if (energy == 10) {
                foreach (Coordinates d in Coordinates.AllDirection)
                    Increase(c + d);
            }
        }
        void CheckReset(Coordinates c) {
            if (octopi.Get(c) > 9)
                octopi.Set(c, 0);
        }