using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeThresholdInstanceBrush : InstanceBrush
{

    [SerializeField] int nbInstances = 5;
    [SerializeField] float minSlope = 0.0f;
    [SerializeField] float maxSlope = 30f;

    public override void draw(float x, float z)
    {
        List<Vector2> randomPositions = Distribution.UniformInSquare(nbInstances, radius * 2, new Vector2(x, z));

        foreach (Vector2 randomPosition in randomPositions)
        {
            Vector3 normal = terrain.getNormal(randomPosition.x, randomPosition.y);
            float slope = Vector3.Angle(normal, Vector3.up);
            if (slope < minSlope || slope > maxSlope) continue;

            spawnObject(randomPosition.x, randomPosition.y);
        }
    }
}
