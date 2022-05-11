using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PathContain : MonoBehaviour
{
    public PathCreator pathCreator;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {

    }


    void LateUpdate() // using LateUpdate so this calculation will go after the original physics calculations
    {
        foreach (Transform child in transform)
        {
            
            rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 rawVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Gets velocity of rigidbody based on Unity3D physics

                float distance = pathCreator.path.GetClosestDistanceAlongPath(child.transform.position); // Gets position along path
                Vector3 displacement = pathCreator.path.GetPointAtDistance(distance) - new Vector3(child.transform.position.x, 0f, child.transform.position.z);
                Vector3 tangent = pathCreator.path.GetDirectionAtDistance(distance); // Gets tangent vector of the path at that position
                child.transform.forward = tangent;

                Vector3 newVelocity = Vector3.Project(rawVelocity, tangent); // calculates the projection of the raw velocity onto the tangent vector

                Vector3 correctionVelocity = newVelocity - rawVelocity;
                // Debug.DrawLine(new Vector3(newVelocity.x, child.transform.position.y, newVelocity.y), new Vector3(correctionVelocity.x, child.transform.position.y, correctionVelocity.y), Color.red);

                // rb.velocity += displacement;
                rb.AddRelativeForce(displacement, ForceMode.VelocityChange);
                // Debug.DrawLine(child.transform.position, new Vector3(newVelocity.x, child.transform.position.y, newVelocity.y), Color.black);



                Debug.DrawLine(child.transform.position, child.transform.position + displacement, Color.yellow);
                // Debug.DrawLine(child.transform.position, );
            }
        }
    }
}
