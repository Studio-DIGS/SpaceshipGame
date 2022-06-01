using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class Bullet : MonoBehaviour
{
    private GameObject path;
    private PathCreator pathCreator;

    private float distance;
    private float height;
    private float shootDir;

    public void Setup(float shootDir)
    {
        this.shootDir = shootDir;
    }

    void Start()
    {
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();

        distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        
        height = transform.position.y;
    }

    void FixedUpdate()
    {
        if (pathCreator != null)
        {
            distance += shootDir * Time.deltaTime;
            Vector3 pathPos = pathCreator.path.GetPointAtDistance(distance);
            transform.position = new Vector3(pathPos.x, height, pathPos.z);
        }
        // transform.position += transform.forward * shootDir * Time.deltaTime;
    }
}
