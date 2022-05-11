using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PlayerRB : MonoBehaviour
{
    public float speed;
    public Vector2 move;

    // private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
        move = input;
    }

}
