using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStuff : MonoBehaviour
{
    public GameObject floatingObject;
    public int _amount;
    public float minScale;
    public float maxScale;
    public int inRange;
    public int outRange;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < _amount; i++)
        {
            Vector3 _pos = Random.insideUnitSphere * Random.Range(inRange, outRange);
            Quaternion _rot = Random.rotation;
            var newFloatObject = Instantiate(floatingObject, _pos, _rot);
            newFloatObject.transform.parent = gameObject.transform;
            newFloatObject.transform.localScale *= Random.Range(minScale, maxScale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}


