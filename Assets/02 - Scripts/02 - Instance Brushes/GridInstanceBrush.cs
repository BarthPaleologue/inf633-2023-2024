using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInstanceBrush : InstanceBrush {

    [SerializeField] int gap = 17;
    [SerializeField] bool horizontal = true;
    [SerializeField] int distance = 5;
    public override void draw(float x, float z) {
        int gapX=distance, gapZ=gap;
        if(!horizontal) {
            gapX=gap;
            gapZ=distance;
        }
        for (int zi = -radius; zi <= radius; zi+=gapZ)
        {
            for (int xi = -radius; xi <= radius; xi+=gapX)
            {
                spawnObject(x+xi, z+zi);
            }
        }
    }
}
