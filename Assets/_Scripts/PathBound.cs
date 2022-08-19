using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathBound : MonoBehaviour
{
    private GameObject path;
    private PathCreator pathCreator;
    public ObjectOnPath objectOnPath;
    private Vector2 move;
    public float collisionSphereRadius = 4.0f;
    public LayerMask layerMask;
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
                this.move = objectOnPath.move;
                CheckCollision();

                distance += this.move.x * Time.deltaTime;
                height += this.move.y * Time.deltaTime;

                Vector3 pathPos = pathCreator.path.GetPointAtDistance(distance);
                transform.position = new Vector3(pathPos.x, height, pathPos.z);
                transform.forward = pathCreator.path.GetDirectionAtDistance(distance);
            }

            if (objectOnPath.target != null)
            {
                objectOnPath.targetDir = GetTargetVector(objectOnPath.target);
            }
        }
        
    }

    private void CheckCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, collisionSphereRadius, layerMask);
        foreach (var hit in hits)
        {
            move -= GetTargetVector(hit.transform.position);
        }
    }

    public Vector2 GetTargetVector(Vector3 objectPos)
    {
        Vector2 targetVector;
        Vector3 delta = objectPos - transform.position;
        Vector3 tangent = pathCreator.path.GetDirectionAtDistance(distance);

        float angle = Vector3.Angle(tangent, delta);
        if (angle <= 90f)
        {
            targetVector.x = 1;
        }
        else
        {
            targetVector.x = -1;
        }

        targetVector.y = Mathf.Clamp(delta.y, -1, 1);

        return targetVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionSphereRadius);
    }
}
