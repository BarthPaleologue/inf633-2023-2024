using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErosionBrush : TerrainBrush
{
    public int erosionRadius = 1;

    private float computeMinHeightAroundPoint(int x, int z)
    {
        List<float> heights = new List<float>();

        for (int zi2 = -erosionRadius; zi2 <= erosionRadius; zi2++)
        {
            for (int xi2 = -erosionRadius; xi2 <= erosionRadius; xi2++)
            {
                heights.Add(terrain.get(x + xi2, z + zi2));
            }
        }
        
        float minHeight = heights[0];
        for (int i = 1; i < heights.Count; i++)
        {
            if (heights[i] < minHeight)
            {
                minHeight = heights[i];
            }
        }

        return minHeight;
    }

    public override void draw(int x, int z)
    {
        float[][] minHeights = new float[radius * 2 + 1][];

        for (int xi = -radius; xi <= radius; xi++)
        {
            minHeights[xi + radius] = new float[radius * 2 + 1];
            for (int zi = -radius; zi <= radius; zi++)
            {
                minHeights[xi + radius][zi + radius] = computeMinHeightAroundPoint(x + xi, z + zi);
            }
        }

        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float minHeight = minHeights[xi + radius][zi + radius];
                float previousHeight = terrain.get(x + xi, z + zi);

                float newHeight = 0.5f * (minHeight + previousHeight);
                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
