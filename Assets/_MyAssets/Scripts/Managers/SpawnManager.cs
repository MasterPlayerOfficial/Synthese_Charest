using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefab = default;
    [SerializeField] private GameObject _enemyContainer = default;
    [SerializeField] private GameObject[] _powerUpPrefab = default;
    [SerializeField] private GameObject _asteroidPrefab = default;
    [SerializeField] private GameObject _asteroidContainer = default;
    [SerializeField] private float _asteroidScore = 750;

    private bool _stopSpawning = false;
    private bool _canReduce = false;
    private float _waitTime = 18f;
    private UIManager _uiManager;
    void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        StartSpawning();
    }

    private void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPURoutine());
        StartCoroutine(SpawnAsteroidRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomEnemy = 0;
            if (_uiManager.GetScore() >= 250)
            {
                randomEnemy = Random.Range(0, _enemyPrefab.Length);
            }
            GameObject newEnemy = Instantiate(_enemyPrefab[randomEnemy], posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3f);
        }
    }

    IEnumerator SpawnPURoutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawning)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            int randomPU = Random.Range(0, _powerUpPrefab.Length);
            Instantiate(_powerUpPrefab[randomPU], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(8f, 14f));
        }
    }

    IEnumerator SpawnAsteroidRoutine()
    {
        yield return new WaitForSeconds(5f);
        while (!_stopSpawning)
        {
            if (_uiManager.GetScore() >= _asteroidScore)
            { 
                Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
                GameObject newAsteroid = Instantiate(_asteroidPrefab, posToSpawn, Quaternion.identity);
                newAsteroid.transform.parent = _asteroidContainer.transform;               
            }
            if (_uiManager.GetScore() % _asteroidScore == 0 && _asteroidScore != 0 && _canReduce == false && _waitTime >= 6f)
            {
                _canReduce = true;
                _waitTime -= 3f;
                if (_waitTime < 3f)
                {
                    _waitTime = 3f;
                }
            }
            else if (_uiManager.GetScore() % _asteroidScore != 0)
            {
                _canReduce = false;
            }
            yield return new WaitForSeconds(_waitTime);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        Destroy(_enemyContainer);
        _uiManager.GameOverSequence();
    }
}
