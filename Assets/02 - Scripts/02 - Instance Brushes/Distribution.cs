using System.Collections.Generic;
using UnityEngine;

public enum DistributionType {
    UNIFORM_SQUARE
}

public class Distribution
{
    public static List<Vector2> UniformInSquare(int nbPoints, int squareSide, Vector2 origin = new Vector2())
    {
        List<Vector2> points = new List<Vector2>();
        for (int i = 0; i < nbPoints; i++)
        {
            float x = origin.x + Random.Range(-(float)squareSide / 2.0f, (float)squareSide / 2.0f);
            float y = origin.y + Random.Range(-(float)squareSide / 2.0f, (float)squareSide / 2.0f);
            points.Add(new Vector2(x, y));
        }
        return points;
    }
}