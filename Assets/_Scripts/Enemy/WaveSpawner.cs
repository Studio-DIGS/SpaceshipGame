using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
#region Variables

    public enum SpawnState { SPAWNING, WAITING, COUNTING };
    private enum WaveBucket { MEDIUM, ADVANCED, EXPERT, COMPLETE } ;

    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
        public bool willSpawnBoss;
    }

    [SerializeField]
    private Transform[] easyEnemyBucket, mediumEnemyBucket, advancedEnemyBucket, expertEnemyBucket, bossBucket;

    private List<Transform> fullEnemyBucket = new List<Transform>();
    private int enemyIndex;

    [SerializeField] 
    private int wavesToMediumBucket, wavesToAdvancedBucket, wavesToExpertBucket;
    private int wavesLeft;
    private WaveBucket bucketToAdd = WaveBucket.MEDIUM;

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
#endregion

    private void Start() 
    {
        timeIdlingWaves = maxTimeIdlingWaves;
        waveCountdown = initialWaitTimer;
        wavesLeft = wavesToMediumBucket;
        AddBucket(easyEnemyBucket);
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
                enemyIndex = Random.Range(0, fullEnemyBucket.Count);
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

        wavesLeft--;

        if (wavesLeft <= 0)
        {
            switch(bucketToAdd)
            {
                case WaveBucket.MEDIUM:
                    AddBucket(mediumEnemyBucket);

                    wavesLeft = wavesToAdvancedBucket;
                    bucketToAdd = WaveBucket.ADVANCED;
                    break;
                case WaveBucket.ADVANCED:
                    AddBucket(advancedEnemyBucket);

                    wavesLeft = wavesToExpertBucket;
                    bucketToAdd = WaveBucket.EXPERT;
                    break;
                case WaveBucket.EXPERT:
                    AddBucket(expertEnemyBucket);

                    bucketToAdd = WaveBucket.COMPLETE;
                    break;
                case WaveBucket.COMPLETE:
                    Debug.Log("All buckets have been added.");
                    break;
            }
        }

        if (nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            Debug.Log("RepeatingFinalWave");
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
            SpawnEnemy(fullEnemyBucket[enemyIndex]);
            enemyIndex = Random.Range(0, fullEnemyBucket.Count);
            yield return new WaitForSeconds( 1f / (_wave.rate * baseWaveMultipler) );
        }
        if (_wave.willSpawnBoss)
        {
            SpawnBoss();
        }

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Debug.Log("Spawning Enemy: " + _enemy);
        Vector3 spawnLocation = new Vector3(0, Random.Range(minimumHeight, maximumHeight), 0);
        Instantiate(_enemy, spawnLocation, transform.rotation);
    }

    private bool TimeRunsOut()
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

    private void AddBucket(Transform[] _bucketToAdd)
    {
        for (int i = 0; i < _bucketToAdd.Length; i++)
        {
            fullEnemyBucket.Add(_bucketToAdd[i]);
        }
    }

    void SpawnBoss()
    {
        enemyIndex = Random.Range(0, bossBucket.Length);
        SpawnEnemy(bossBucket[enemyIndex]);
    }
}
