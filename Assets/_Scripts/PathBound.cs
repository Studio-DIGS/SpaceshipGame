using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathBound : MonoBehaviour
{
    private GameObject path;
    private PathCreator pathCreator;
    public ObjectOnPath objectOnPath;

    private float distance;
    private float height;

    void Start()
    {
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();

        distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        height = transform.position.y;        
        
    }

    void LateUpdate()
    {
        if (pathCreator != null && gameObject != null)
        {
            Vector2 move = objectOnPath.move;
            distance += move.x * Time.deltaTime;
            height += move.y * Time.deltaTime;

            Vector3 pathPos = pathCreator.path.GetPointAtDistance(distance);
            transform.position = new Vector3(pathPos.x, height, pathPos.z);
            transform.forward = pathCreator.path.GetDirectionAtDistance(distance);
        }
        
    }
}
