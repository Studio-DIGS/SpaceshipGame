using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathBound : MonoBehaviour
{
    private GameObject path;
    private PathCreator pathCreator;

    void Start()
    {
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();
        
    }

    void LateUpdate()
    {
        if (pathCreator != null)
        {
            float distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
            transform.forward = pathCreator.path.GetDirectionAtDistance(distance);

            Vector3 correction = pathCreator.path.GetPointAtDistance(distance) - transform.position;
            Vector3 target = new Vector3(correction.x, 0, correction.z);
            transform.position += target;
        }
        
    }
}
