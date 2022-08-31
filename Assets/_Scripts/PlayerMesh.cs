using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMesh : MonoBehaviour
{
    public Player player;
    public float smooth;
    
    public int numberOfFlashes;
    public float iFrameTime;

    private Material shipMaterialRef;
    public Material iFrameMaterialRef;

    public CameraShake cameraShake;

    private float previousTimeHit = 0.0f;


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

        previousTimeHit += Time.deltaTime;
        if (previousTimeHit >= player.timeToRegen)
        {
            player.healthSystem.Heal(1);
            previousTimeHit = 0.0f;
        }
        
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRot, Time.deltaTime * smooth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Enemy"|| other.gameObject.tag == "EnemyProjectile") && player.invincible == false)
        {
            TakeDamage();
        }
    }

    private IEnumerator iFrames()
    {
        player.invincible = true;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            GetComponent<Renderer>().material = iFrameMaterialRef;
            yield return new WaitForSeconds(iFrameTime / (numberOfFlashes * 2));
            GetComponent<Renderer>().material = shipMaterialRef;
            yield return new WaitForSeconds(iFrameTime / (numberOfFlashes * 2));
        }
        player.invincible = false;
    }

    public void TakeDamage()
    {
        previousTimeHit = 0.0f;
        StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
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
