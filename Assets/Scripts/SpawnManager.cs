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
    private float _spawnTime = 0.5f;
    private bool _stopSpawning = false;

    void Start()
    {
      StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
      while (_stopSpawning == false) {
        Vector3 positionToSpawn = new Vector3(Random.Range(-8.0f, 8.0f), 8, 0);
        GameObject newEnemy = Instantiate(_enemyPrefab, positionToSpawn, Quaternion.identity);
        newEnemy.transform.parent = _enemyContainer.transform;
        yield return new WaitForSeconds(_spawnTime);
      }
    }

    public void onPlayerDeath()
    {
      _stopSpawning = true;
    }
}
