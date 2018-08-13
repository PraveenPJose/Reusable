using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class WaveCreater : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING,
        OVER
    }
     SpawnState spawnState = SpawnState.COUNTING;

    [System.Serializable]
	public class Wave
    {
        public string name;
        public GameObject[] enemy;
        public int count;
        public float rate;
    }
    public Wave[] waves;
    public float timeBetweenWaves=5f;
    public Transform[] spwanPoints;
    public UnityEvent allWaveOver,oneWaveCompleted;

    int nextWave = 0;
    float searchEnemy=1f;
    float waveCountDown;

    void Start ()
    {
        waveCountDown = timeBetweenWaves;
    }
	

	void Update ()
    {
        switch(spawnState)
        {
            case SpawnState.OVER:
                {
                    break;
                }
            case SpawnState.WAITING:
                {
                    if (!EnemyIsAlive())
                    {
                        WaveCompleted();
                    }
                    break;
                }
            default:
                {
                    if (waveCountDown <= 0)
                    {
                        if (spawnState != SpawnState.SPAWNING)
                        {
                            StartCoroutine(SpwanWave(waves[nextWave]));
                            
                        }
                    }
                    else
                    {
                        waveCountDown -= Time.deltaTime;
                    }
                    break;
                }
        }

	}

    IEnumerator SpwanWave(Wave _wave)
    {
        print("Spawning Wave"+_wave.name);
        spawnState = SpawnState.SPAWNING;
        for(int i=0;i<_wave.count;i++)
        {
            Transform enemy = _wave.enemy[Random.Range(0, _wave.enemy.Length)].transform;
            SpawnEnemy(enemy);
            yield return new WaitForSeconds(_wave.rate);
        }
        spawnState = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Transform tempPos = spwanPoints[Random.Range(0, spwanPoints.Length)];
        Instantiate(_enemy, tempPos.position, Quaternion.identity);
    }

    bool EnemyIsAlive()
    {
        searchEnemy -= Time.deltaTime;
       
        if (searchEnemy <= 0)
        {
            searchEnemy = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
            
        }
        return true;
    }

    void WaveCompleted()
    {
        print("Wave Completed");
        spawnState = SpawnState.COUNTING;
        waveCountDown = timeBetweenWaves;
        if((nextWave+1)>=waves.Length)
        {
            print("All Waves Completed");
            allWaveOver.Invoke();
            spawnState = SpawnState.OVER;
            StopAllCoroutines();
        }
        else
        {
            oneWaveCompleted.Invoke();
            nextWave++;
        }
       
    }
}
