using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class GenerateCircle : MonoBehaviour
{
    private bool closedLoop = true;
    private float controlSpacing = 0.3905242f;
    public float radius;
    private Vector2[] waypoints;

    void Start()
    {
        // add four waypoints according to radius
        waypoints = new Vector2[]
        {
            new Vector2(radius, 0),
            new Vector2(0, radius),
            new Vector2(radius * -1, 0),
            new Vector2(0, radius * -1)
        };

        BezierPath bezierPath = new BezierPath (waypoints, closedLoop, PathSpace.xz);
        GetComponent<PathCreator> ().bezierPath = bezierPath;

        bezierPath.AutoControlLength = controlSpacing;
    }
}
