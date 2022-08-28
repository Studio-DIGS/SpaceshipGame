using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnTimer : MonoBehaviour
{
    [SerializeField] float time;
    public ParticleSystem despawnParticles;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, time);

    }

}
