using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMesh : MonoBehaviour
{
    public Player player;
    public float smooth;
    
    private bool invincible;
    public int numberOfFlashes;
    public float iFrameTime;

    private Material shipMaterialRef;
    public Material iFrameMaterialRef;

    void Awake()
    {
        shipMaterialRef = GetComponent<Renderer>().material;
    }

    void Update()
    {
        // Consider using Unity events instead of constantly changing the rotation every frame
        float orientation = -1 * player.orientation;
        float angle = Mathf.Clamp(orientation * 179, -179, 0);
        Quaternion targetRot = Quaternion.Euler(0,angle,0);
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if ((other.gameObject.tag == "Enemy"|| other.gameObject.tag == "EnemyProjectile") && invincible == false)
        {
            TakeDamage();
        }
    }

    private IEnumerator iFrames()
    {
        invincible = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            GetComponent<Renderer>().material = iFrameMaterialRef;
            yield return new WaitForSeconds(iFrameTime / (numberOfFlashes * 2));
            GetComponent<Renderer>().material = shipMaterialRef;
            yield return new WaitForSeconds(iFrameTime / (numberOfFlashes * 2));
        }
        invincible = false;
    }

    public void TakeDamage()
    {
        player.healthSystem.Damage(1);
        if (player.healthSystem.GetHealth() <= 0)
        {
            // Death explosion goes here
            SceneManager.LoadScene("GameOver");
            //Destroy(player.gameObject);
        }

        StartCoroutine(iFrames());
    }
}
