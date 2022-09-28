using System.Collections;
    public int day = 17;
    public Part part;

        // This challenge seemed daunting at first, but ended up being not so bad.
        // I wasn't sure if there was a way to calculate the highest y using some sort of quadratic function, 
        // and after coming up with nothing for a while I decided to simulate the probe given an initial velocity.

        // The actual simulation logic wasn't hard, I just implemented what the instructions said.
        // I simulated until the target was hit or either the x or y of the current position was past the target's east or south bounds,
        // because if it was the probe would never hit the target from that point.

        // I did have trouble generating the initial velocities, though. I knew for a fact that the inital x couldn't be more than
            for (int y = 0; y < 1000; y++)
        }

        print(highestValue);
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
        }
            return c.x >= bounds[0] && c.x <= bounds[1] && c.y >= bounds[2] && c.y <= bounds[3];
        }

        int initialVelocities = 0;
            for (int y = -1000; y < 1000; y++)
        }

        print(initialVelocities);
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
        }
            return c.x >= bounds[0] && c.x <= bounds[1] && c.y >= bounds[2] && c.y <= bounds[3];
        }
    }