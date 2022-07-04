using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPlane : MonoBehaviour
{
    [SerializeField] Vector3 center;
    [SerializeField] Vector3 size;
    [SerializeField] List<GameObject> enemy;
    private bool spawnRightSide = false;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            int formation_num = Random.Range(0, enemy.Count);
            if (Random.Range(0f, 1.0f) >= 0.5) {
                spawnRightSide = true;
            } else {
                spawnRightSide = false;
            }
            
            spawnEnemy(formation_num);
        }
    }

    public void spawnEnemy(int num) 
    {
        Vector3 pos = transform.localPosition + center + new Vector3(Random.Range(-size.x / 2 , (-size.x / 2) - 1), Random.Range(-size.y / 2 , size.y / 2), 0); //pos represents left spawn side

        if (spawnRightSide) 
        {
            pos.x *= -1;
        }
        Instantiate(enemy[num], pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.localPosition + center, size);
    }

}
