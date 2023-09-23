using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleBrush : TerrainBrush
{
    [SerializeField] bool isIncreasing = true;
    [SerializeField] bool inverted = false;
    [SerializeField] float increment = 1;
    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float previousHeight = terrain.get(x + xi, z + zi);

                float distanceToCenter = Mathf.Sqrt(xi * xi + zi * zi);

                float circle = distanceToCenter < radius ? 1 : 0;
                if(inverted) circle = 1 - circle;
                circle *= increment;

                float newHeight = isIncreasing ? previousHeight + circle : previousHeight - circle;

                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
