using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _points = 50;
    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _damage = 5.0f;
    [SerializeField] private int _enemyID = default;
    [SerializeField] private GameObject _enemyLaserPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;

    private UIManager _uiManager;
    private float _fireRate;
    private float _canFire;

    private void Start()
    {
        _uiManager = FindObjectOfType<UIManager>().GetComponent<UIManager>();
        _canFire = Random.Range(0.5f, 1f);
    }
    void Update()
    {
        CalculateMovement();
        EnemyFire();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void EnemyFire()
    {
        if (_uiManager.GetScore() >= 500)
        {
            if (Time.time > _canFire)
            { 
                _fireRate = Random.Range(1f, 3f);
                _canFire = Time.time + _fireRate;
                GameObject newLaser = Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0f, -1f, 0f), Quaternion.identity);
                newLaser.GetComponent<Laser>().SetEnemyType(_enemyID, _damage);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            player.Damage(_damage);
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0f);
        }
        else if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject, 0f);
            _uiManager.ChangerScore(_points);
        }
    }
}
