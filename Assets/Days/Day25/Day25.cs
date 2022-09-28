using System.Collections;
    public int day = 25;
    public Part part;
        public Coordinates position;
        public Coordinates nextPosition;
        public Coordinates moveDirection;

        public SeaCucumber(Coordinates position, Coordinates moveDirection) {
            this.position = position;
            this.moveDirection = moveDirection;
        }
    }
            switch (grid.Get(c)) {
                case ">":
                    cucumbers.Set(c, new SeaCucumber(c, Coordinates.Right));
                    east.Add(cucumbers.Get(c));
                    break;
                case "v":
                    cucumbers.Set(c, new SeaCucumber(c, Coordinates.Down));
                    south.Add(cucumbers.Get(c));
                    break;
            }
        }
            List<SeaCucumber> updateEast = new List<SeaCucumber>();
            foreach (SeaCucumber sc in east) {
                Coordinates next = sc.position + sc.moveDirection;
                if (!cucumbers.WithinBounds(next))
                    next = new Coordinates(0, next.y);
                if (cucumbers.Get(next) == null) {
                    sc.nextPosition = next;
                    updateEast.Add(sc);
                }
            }

            foreach (SeaCucumber sc in updateEast) {
                cucumbers.Set(sc.position, null);
                cucumbers.Set(sc.nextPosition, sc);
                sc.position = sc.nextPosition;
            }

            List<SeaCucumber> updateSouth = new List<SeaCucumber>();
            foreach (SeaCucumber sc in south) {
                Coordinates next = sc.position + sc.moveDirection;
                if (!cucumbers.WithinBounds(next))
                    next = new Coordinates(next.x, cucumbers.Height() - 1);
                if (cucumbers.Get(next) == null) {
                    sc.nextPosition = next;
                    updateSouth.Add(sc);
                }
            }

            foreach (SeaCucumber sc in updateSouth) {
                cucumbers.Set(sc.position, null);
                cucumbers.Set(sc.nextPosition, sc);
                sc.position = sc.nextPosition;
            }

            steps++;
            if (updateEast.Count == 0 && updateSouth.Count == 0) {
                stopped = true;
                break;
            }
        }
            for (int y = cucumbers.Height() - 1; y >= 0; y--) {
                string s = "";
                for (int x = 0; x < cucumbers.Width(); x++) {
                    Coordinates c = new Coordinates(x, y);
                    SeaCucumber sc = cucumbers.Get(c);
                    if (sc == null)
                        s += " .";
                    else {
                        if (sc.moveDirection == Coordinates.Right)
                            s += ">";
                        else
                            s += "v";
                    }
                }
                print(s);
            }
        }