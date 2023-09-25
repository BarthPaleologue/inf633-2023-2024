using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveInstanceBrush : InstanceBrush
{

    public override void draw(float x, float z)
    {
        float objectCount = terrain.getObjectCount();
        for(int i = 0; i < objectCount; i++) {
            TreeInstance instance = terrain.getObject(i);
            Vector3 instancePosition = new Vector3(terrain.getObjectLoc(i).x, 0.0f, terrain.getObjectLoc(i).z);
            float distanceToBrush = Vector3.Distance(instancePosition, new Vector3(x, 0.0f, z));
            if(distanceToBrush < radius) {
                terrain.DeleteObject(i);
                i--;
                objectCount--;
            }
        }
    }
}
