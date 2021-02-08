using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    public float spawnEnemyRate = 1f;
    public float spawnPowerUpRate = 1f;
    

    // Start is called before the first frame update
    private void Awake() 
    {
        EventBroker.GameStarted += StartSpawning;
        EventBroker.GameEnded += CancelSpawning;
    }  

    private void Start() 
    {
        StartSpawning();
    } 
        
    public void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(Random.Range(-4, 4), 1, 10), enemyPrefab.transform.rotation);
    }

    public void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-4, 4), 1, 10), powerUpPrefab.transform.rotation);
    }

    public void StartSpawning()
    {        
        InvokeRepeating("SpawnEnemy", 1f, spawnEnemyRate);

        InvokeRepeating("SpawnPowerUp", 1f, Random.Range(1, spawnPowerUpRate));
    }


    public void CancelSpawning()
    {        
        CancelInvoke();
        EventBroker.GameEnded -= CancelSpawning;
        EventBroker.GameStarted -= StartSpawning;
    }

    
}
