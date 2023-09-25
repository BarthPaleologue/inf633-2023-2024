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
        Vector3[] randomPositions = new Vector3[nbInstances];

        for (int i = 0; i < nbInstances; i++)
        {
            float instanceX = x + Random.Range(-radius, radius);
            float instanceZ = z + Random.Range(-radius, radius);
            float instanceY = terrain.get(instanceX, instanceZ);
            randomPositions[i] = new Vector3(instanceX, instanceY, instanceZ);
        }

        for (int i = 0; i < nbInstances; i++)
        {
            Vector3 instancePosition = randomPositions[i];
            Vector3 normal = terrain.getNormal(instancePosition.x, instancePosition.z);
            float slope = Vector3.Angle(normal, Vector3.up);
            if (slope > minSlope && slope < maxSlope)
            {
                spawnObject(instancePosition.x, instancePosition.z);
            }
        }
    }
}
