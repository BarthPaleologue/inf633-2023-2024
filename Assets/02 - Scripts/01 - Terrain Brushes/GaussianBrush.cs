using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianBrush : TerrainBrush
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

                float distanceToCenter = Mathf.Sqrt(xi * xi + zi * zi);

                float gaussian = Mathf.Exp(-(distanceToCenter * distanceToCenter) / (2 * radius * radius));

                float gaussianIncrement = gaussian * increment;

                float newHeight = isIncreasing ? previousHeight + gaussianIncrement : previousHeight - gaussianIncrement;

                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
