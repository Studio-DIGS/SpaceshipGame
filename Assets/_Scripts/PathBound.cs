using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathBound : MonoBehaviour
{
    private GameObject path;
    private PathCreator pathCreator;
    public ObjectOnPath objectOnPath;
    private Vector2 targetDir;

    [HideInInspector] public float distance, height;

    void OnEnable()
    {
        path = GameObject.FindWithTag("WorldPath");
        pathCreator = path.GetComponent<PathCreator>();

        distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        height = transform.position.y;        
        
    }

    void LateUpdate()
    {
        if (pathCreator != null && objectOnPath != null)
        {
            if (objectOnPath.onPath)
            {
                Vector2 move = objectOnPath.move;
                distance += move.x * Time.deltaTime;
                height += move.y * Time.deltaTime;

                Vector3 pathPos = pathCreator.path.GetPointAtDistance(distance);
                transform.position = new Vector3(pathPos.x, height, pathPos.z);
                transform.forward = pathCreator.path.GetDirectionAtDistance(distance);
            }

            if (objectOnPath.target != null)
            {
                GetTargetVector(objectOnPath.target);
                objectOnPath.targetDir = this.targetDir;
            }
        }
        
    }

    void GetTargetVector(Vector3 objectPos)
    {
        Vector3 delta = objectPos - transform.position;
        Vector3 tangent = pathCreator.path.GetDirectionAtDistance(distance);

        float angle = Vector3.Angle(tangent, delta);
        if (angle <= 90f)
        {
            targetDir.x = 1;
        }
        else
        {
            targetDir.x = -1;
        }

        targetDir.y = Mathf.Clamp(delta.y, -1, 1);

        // return targetDir;
    }
}
