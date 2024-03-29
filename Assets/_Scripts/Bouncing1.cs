using UnityEngine;
using System.Collections;
 
// Makes objects float up & down while gently spinning.
public class Bouncing1 : MonoBehaviour 
{
    // User Inputs
    public float degreesPerSecond;
    public float amplitude;
    public float frequency;
 
    // Position Storage Variables
    Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();
 
    // Use this for initialization
    void Start () {
        // Randomize Values

        degreesPerSecond = Random.Range(0f, degreesPerSecond);

        // Store initial position
        posOffset = transform.position;
    }
     
    // Update is called once per frame
    void Update () {
        // Spin object around Y-Axis
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);
 
        // Float up/down with a Sin()
        tempPos = posOffset;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency * (1/amplitude)) * amplitude;
 
        transform.position = tempPos;
    }
}
    
