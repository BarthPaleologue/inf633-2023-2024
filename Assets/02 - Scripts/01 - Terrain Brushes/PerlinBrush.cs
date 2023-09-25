using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinBrush : TerrainBrush
{
    public bool isIncreasing = true;
    public float increment = 1;
    public float noiseFrequency = 0.1f;
    public int nbOctaves = 1;
    public override void draw(int x, int z)
    {
        for (int zi = -radius; zi <= radius; zi++)
        {
            for (int xi = -radius; xi <= radius; xi++)
            {
                float previousHeight = terrain.get(x + xi, z + zi);
                float perlin = 0.0f;
                float heightAcc = 0.0f;
                for(int i = 0; i < nbOctaves; i++)
                {
                    float localFrequency = noiseFrequency * Mathf.Pow(2.0f, (float)i);
                    perlin += Mathf.PerlinNoise((x + xi) * localFrequency, (z + zi) * localFrequency) / Mathf.Pow(2.0f, (float)i);
                    heightAcc += 1.0f / Mathf.Pow(2.0f, (float)i);
                }
                perlin /= heightAcc;

                float newHeight = isIncreasing ? previousHeight + increment * perlin : previousHeight - increment * perlin;
                terrain.set(x + xi, z + zi, newHeight);
            }
        }
    }
}
