using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltitudeThresholdInstanceBrush : InstanceBrush
{

    [SerializeField] int nbInstances = 5;
    [SerializeField] float minAltitude = 0.0f;
    [SerializeField] float maxAltitude = 5.0f;

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
            if(instancePosition.y > minAltitude && instancePosition.y < maxAltitude)
            {
                spawnObject(instancePosition.x, instancePosition.z);
            }
        }
    }
}
