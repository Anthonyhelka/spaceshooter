using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private float _enemySpawnTime = 0.5f;

    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;

    void Start()
    {
      StartCoroutine(SpawnEnemyRoutine());
      StartCoroutine(SpawnPowerupRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
      while (_stopSpawning == false) {
        Vector3 positionToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 8, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        yield return new WaitForSeconds(_enemySpawnTime);
      }
    }

    IEnumerator SpawnPowerupRoutine()
    {
      while(_stopSpawning == false)
      {
        Vector3 positionToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 8, 0);
        int randomPowerup = Random.Range(0, 3);
        GameObject newPowerup = Instantiate(_powerups[randomPowerup], positionToSpawn, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
      }
    }

    public void onPlayerDeath()
    {
      _stopSpawning = true;
    }
}
