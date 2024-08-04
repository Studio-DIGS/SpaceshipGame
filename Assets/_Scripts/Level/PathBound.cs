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
    private Vector3 pos3;

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
                objectOnPath.targetDir = GetTargetVector(objectOnPath.target, objectOnPath.offset);
            }
        }
        
    }

    private void CheckCollision()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, collisionSphereRadius, layerMask);
        foreach (var hit in hits)
        {
            move -= GetTargetVector(hit.transform.position, 0f);
        }
    }

    public Vector2 GetTargetVector(Vector3 objectPos, float offset)
    {
        Vector2 targetVector;
        Vector3 virtualPathPos = pathCreator.path.GetPointAtDistance(distance + offset);
        Vector3 virtualPos = new Vector3(virtualPathPos.x, height, virtualPathPos.z);
        Vector3 virtualTangent = pathCreator.path.GetDirectionAtDistance(distance + offset);
        Vector3 delta = objectPos - virtualPos;

        float angle = Vector3.Angle(transform.forward, delta);
        if (angle <= 90f)
        {
            targetVector.x = 1;
        }
        else
        {
            targetVector.x = -1;
        }

        targetVector.y = Mathf.Clamp(delta.y, -1, 1);

        targetVector *= Mathf.Clamp(delta.sqrMagnitude, 0, 1);
        return targetVector;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, collisionSphereRadius);
    }
}
