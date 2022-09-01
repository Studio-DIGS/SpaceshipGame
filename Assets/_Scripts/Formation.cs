using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Formation : MonoBehaviour
{
    private float checkTime = 5.0f;
    private bool isAlive = true;
    private static PathCreator pathCreator;
    private static GameObject player;

    public float playerDistance;

    public float direction;

    private void Awake()
    {
        if (pathCreator == null)
        {
            pathCreator = GameObject.FindWithTag("WorldPath").GetComponent<PathCreator>();
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    private void Start() 
    {
        PickDirection();
        FindPlayerDistance();
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

    private void PickDirection()
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

    private void FindPlayerDistance()
    {
        playerDistance = pathCreator.path.GetClosestDistanceAlongPath(player.transform.position);
    }
}
