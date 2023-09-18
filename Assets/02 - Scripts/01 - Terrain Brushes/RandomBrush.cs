using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBrush : TerrainBrush
{
    public float noiseExtend = 1.0f;
    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float previousHeight = terrain.get(x + xi, z + zi);
                float increment = Random.Range(-noiseExtend, noiseExtend);
                float newHeight = previousHeight + increment;
                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
