using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncrementalBrush : TerrainBrush
{
    public bool isIncreasing = true;
    public float increment = 1;
    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float previousHeight = terrain.get(x + xi, z + zi);
                float newHeight = isIncreasing ? previousHeight + increment : previousHeight - increment;
                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
