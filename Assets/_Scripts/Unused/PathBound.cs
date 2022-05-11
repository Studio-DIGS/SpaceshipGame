using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathBound : MonoBehaviour
{
    public PathCreator pathCreator;
    float distance;
    Vector3 height;

    // Start is called before the first frame update
    void Start()
    {
        distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        transform.position = pathCreator.path.GetPointAtDistance(distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (pathCreator != null)
        {
            
            
            // distance += pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            // transform.position = pathCreator.path.GetPointAtDistance(distance);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distance);
        }
        
    }
}
