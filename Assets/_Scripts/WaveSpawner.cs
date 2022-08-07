using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
    }

    public Transform[] enemyList;
    private int enemyIndex;

    public Wave[] waves;
    private int nextWave = 0;

    public float minimumHeight = -30f;
    public float maximumHeight = 30f;

    public float initialWaitTimer = 3f;
    public float baseWaveMultipler = 5f;
    public float timeBetweenWaves = 5f;
    public float maxTimeIdlingWaves = 10f;
    public float waveCountdown;

    private float timeIdlingWaves;
    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.COUNTING;

    private void Start() 
    {
        timeIdlingWaves = maxTimeIdlingWaves;
        waveCountdown = initialWaitTimer;
    }

    private void Update() 
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive() || TimeRunsOut())
            {
                WaveCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                enemyIndex = Random.Range(0, enemyList.Length);
                StartCoroutine( SpawnWave( waves[nextWave] ) );
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");

        timeIdlingWaves = maxTimeIdlingWaves;

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete! Looping.");
            return;
        }

        nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(enemyList[enemyIndex]);
            enemyIndex = Random.Range(0, enemyList.Length);
            yield return new WaitForSeconds( 1f / (_wave.rate * baseWaveMultipler) );
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy: " + _enemy);
        Vector3 spawnLocation = new Vector3(0, Random.Range(minimumHeight, maximumHeight), 0);
        Instantiate(_enemy, spawnLocation, transform.rotation);
    }

    bool TimeRunsOut()
    {
        timeIdlingWaves -= Time.deltaTime;
        if (timeIdlingWaves <= 0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
