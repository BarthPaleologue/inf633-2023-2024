using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinDistanceInstanceBrush : InstanceBrush {

    public float minDistance = 3.0f;

    public override void draw(float x, float z) {

      List<Vector2> positions = getPositionsInSquare(radius, x, z, terrain);

      Vector2 position = new Vector2(0,0);
      for(int i=0; i<positions.Count; i++) {
        Vector2 pos = positions[i];
        float dist = Vector2.Distance(pos, position);
        if(dist < minDistance) {
          Vector2 repel = position-pos;
          repel *= minDistance-dist;
          position = position + repel;
        }
      }

      if(checkDistances(positions, position, radius, minDistance)) {
        spawnObject(x+position.x, z+position.y);
      }
      else {
        Debug.Log("Cannot spawn: No space left");
      }
    }

    public static bool checkDistances(
      List<Vector2> positions, Vector2 position,
      float radius, float minDistance
    ) {
      for(int i=0; i<positions.Count; i++) {
        Vector2 pos = positions[i];
        float dist = Vector2.Distance(pos, position);
        if(dist < minDistance) {
          return false;
        }
      }
      if(Vector2.Distance(Vector2.zero, position) > radius) {
        return false;
      }
      return true;
    }

    public static List<Vector2> getPositionsInSquare(
      float radius, float x, float z,
      CustomTerrain terrain
    ) {
      int count = terrain.getObjectCount();
      List<Vector2> positions = new List<Vector2>();
      for(int i=0; i<count; i++) {
        Vector3 pos = terrain.getObjectLoc(i);
        if(Mathf.Abs(pos.x-x)<radius && Mathf.Abs(pos.z-z)<radius) {
          positions.Add(new Vector2(pos.x-x, pos.z-z));
        }
      }
      return positions;
    }
}
