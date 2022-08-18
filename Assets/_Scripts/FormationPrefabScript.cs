using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class FormationPrefabScript : MonoBehaviour
{
    //GettingToPath Vars
    float distance;
    private bool isOnLeft;
    private GameObject path;
    private List<Enemy> enemy;
    [SerializeField] private float speed = 30.0f;

    //Points Vars
    public float multiplier = 1f;

    private void Awake() 
    {
        path = GameObject.FindWithTag("WorldPath");
    }

    void Start()
    {
        distance = path.GetComponent<GenerateCircle>().radius;
        int spawnLeft = Random.Range(0, 2);
        if (spawnLeft != 0) 
        {
            isOnLeft = true;
            transform.Rotate(0, 180, 0);
        } 
        else 
        {
            isOnLeft = false;
        }
    }

    void Update()
    {
            if (isOnLeft) 
            {
                foreach (Transform enemy in transform) 
                {
                    if(Mathf.Abs(distance - enemy.transform.position.x) >= 0.5f)
                    {
                        enemy.transform.position += Vector3.right * speed * Time.deltaTime;
                    }
                    else
                    {
                        // enemy.GetComponent<Enemy1>().BeginPathing();
                    }
                }
            } 
            else 
            {
                foreach (Transform enemy in transform) 
                {
                    if(Mathf.Abs(distance + enemy.transform.position.x) >= 0.5f)
                    {
                        enemy.transform.position += Vector3.left * speed * Time.deltaTime;
                    }
                    else
                    {
                        // enemy.GetComponent<Enemy1>().BeginPathing();
                    }
                }
            }
            if (transform.childCount == 0)
            {
                Destroy(gameObject);
            }
    }
}
