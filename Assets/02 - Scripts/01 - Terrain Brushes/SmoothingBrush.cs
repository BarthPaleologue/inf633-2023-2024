using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothingBrush : TerrainBrush
{
    public int smoothRadius = 1;

    private float computeMeanHeightAtPoint(int x, int z)
    {
        float heightAcc = 0.0f;
        int denominatorAcc = 0;
        for (int zi2 = -smoothRadius; zi2 <= smoothRadius; zi2++)
        {
            for (int xi2 = -smoothRadius; xi2 <= smoothRadius; xi2++)
            {
                heightAcc += terrain.get(x + xi2, z + zi2);
                denominatorAcc++;
            }
        }
        float meanHeight = heightAcc / (float)denominatorAcc;
        return meanHeight;
    }

    public override void draw(int x, int z)
    {
        float[][] meanHeights = new float[radius * 2 + 1][];

        for (int xi = -radius; xi <= radius; xi++)
        {
            meanHeights[xi + radius] = new float[radius * 2 + 1];
            for (int zi = -radius; zi <= radius; zi++)
            {
                meanHeights[xi + radius][zi + radius] = computeMeanHeightAtPoint(x + xi, z + zi);
            }
        }

        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float meanHeight = meanHeights[xi + radius][zi + radius];
                terrain.set(x + xi, z + zi, meanHeight);
            }
        }
    }
}
