using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRandomInstanceBrush : InstanceBrush {

    public int count = 5;

    public override void draw(float x, float z) {
      for(int i=0; i<count; i++) {
        float xDisplace = (Random.value-0.5f)*radius;
        float zDisplace = (Random.value-0.5f)*radius;
        spawnObject(x+xDisplace, z+zDisplace);
      }
    }
}
