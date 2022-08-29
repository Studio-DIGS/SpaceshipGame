using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private float checkTime = 5.0f;
    private bool isAlive = true;

    public float direction;

    private void Awake()
    {
        if (Random.Range(0.0f, 1.0f) >= 0.5f)
        {
            direction = 1.0f;
        }
        else
        {
            direction = -1.0f;
        }
    }

    private void Start() 
    {
        StartCoroutine(CheckIfEmpty());
    }

    private IEnumerator CheckIfEmpty()
    {
        while (isAlive)
        {
            if (transform.childCount == 0)
            {
                isAlive = false;
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(checkTime);
        }
    }
}
