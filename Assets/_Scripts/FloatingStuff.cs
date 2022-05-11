using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStuff : MonoBehaviour
{
    public float inRange;
    public float outRange;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            child.position = Random.insideUnitSphere * Random.Range(inRange, outRange);
            child.rotation = Random.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
