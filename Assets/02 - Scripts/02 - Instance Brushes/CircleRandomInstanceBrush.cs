using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRandomInstanceBrush : InstanceBrush
{
    [SerializeField] int instanceNumber = 10;

    public override void draw(float x, float z)
    {
        float randAngle = Random.value*360;
        float randRadius = Random.value*radius;
        for(int i=0; i<instanceNumber; i++) {
            float xIndex=randRadius*Mathf.Cos(randAngle);
            float zIndex=randRadius*Mathf.Sin(randAngle);
            spawnObject(x + xIndex, z + zIndex);
        }
    }
}
