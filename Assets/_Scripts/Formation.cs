using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Formation : MonoBehaviour
{
    private float checkTime = 5.0f;
    private bool isAlive = true;

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
