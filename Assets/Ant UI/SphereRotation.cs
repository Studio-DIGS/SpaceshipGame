using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereRotation : MonoBehaviour
{   [SerializeField] private float speedSphere = 5;
    public Transform sphere;

    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(transform.position, transform.up, speedSphere*Time.deltaTime);
    }
}
