using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKnockback : MonoBehaviour
{
    private Rigidbody rb;
    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 force = Random.insideUnitSphere.normalized;
            rb.AddForce(transform.forward * magnitude);
            Debug.DrawLine(transform.position, transform.forward, Color.blue);
        }
    }
}
