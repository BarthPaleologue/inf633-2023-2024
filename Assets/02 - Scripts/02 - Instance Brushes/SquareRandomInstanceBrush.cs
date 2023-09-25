using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareRandomInstanceBrush : InstanceBrush {

    public override void draw(float x, float z) {
      float xDisplace = (Random.value-0.5f)*radius;
      float zDisplace = (Random.value-0.5f)*radius;
      spawnObject(x+xDisplace, z+zDisplace);
    }
}
