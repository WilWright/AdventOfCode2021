using System.Collections;
    public int day = 20;
    public Part part;

        // I understood the initial premise of this challenge pretty well. I just needed to pad the edge of the grid
        // to get the necessary data for the current in-bounds pixels, and any out of bounds pixels could be treated
        // the same as unlit pixels. There was a trick in the input however. A group of 3x3 unlit pixels would be enhanced
        // to a lit pixel, meaning all of the infinite out of bounds pixels would need to change to be lit,
        // which would then need to be evaluated for the next enhancement. I thought there must be a mistake in my input 
        // that was overlooked as a possibility, so I checked what other participants were discussing about this day online.
            imageInput[i - 1] = stringInput[i];
            if (grid.Get(c))
                litCount++;
        }
            GridSystem<bool> nextImage = new GridSystem<bool>(image.Width(), image.Height());
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
        }
            GridSystem<bool> newImage = new GridSystem<bool>(image.Width() + 4, image.Height() + 4);
            foreach (Coordinates c in newImage.coordinates)
                newImage.Set(c, fillWith);

            Coordinates offset = new Coordinates(2, 2);
            foreach (Coordinates c in image.coordinates)
                newImage.Set(c + offset, image.Get(c));

            return newImage;
        }
            for (int y = image.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < image.Width(); x++)
                    s += image.Get(new Coordinates(x, y)) ? "#" : " . ";
                print(s);
            }
        }
            imageInput[i - 1] = stringInput[i];
            bool swapLit = i % 2 == 1;
            grid = PadImage(grid, swapLit);
        }
            if (grid.Get(c))
                litCount++;
        }
            GridSystem<bool> nextImage = new GridSystem<bool>(image.Width(), image.Height());
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
        }
            GridSystem<bool> newImage = new GridSystem<bool>(image.Width() + 4, image.Height() + 4);
            foreach (Coordinates c in newImage.coordinates)
                newImage.Set(c, fillWith);

            Coordinates offset = new Coordinates(2, 2);
            foreach (Coordinates c in image.coordinates)
                newImage.Set(c + offset, image.Get(c));

            return newImage;
        }
            for (int y = image.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < image.Width(); x++)
                    s += image.Get(new Coordinates(x, y)) ? "#" : " . ";
                print(s);
            }
        }

        //print();
    }