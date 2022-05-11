using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    
    public float speed;
    public Vector2 input;    
    public float orientation = 1; // -1 is left, +1 is right

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized; // get movement input
        orientation = Mathf.Clamp(orientation + (input.x * 2), -1, 1); // calculate the orientation (left or right) based on input
        
        Vector3 move = transform.forward * input.x + new Vector3(0,input.y,0); // get direction of movement based on forward direction

        controller.Move(move * Time.deltaTime * speed);

        Debug.DrawLine(transform.position, transform.position + move * speed, Color.blue);
    }
}
