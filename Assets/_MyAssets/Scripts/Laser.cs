using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;
    [SerializeField] private string _name = default;

    private UIManager _uiManager;
    private float _enemySpeed = 0;
    private int _enemyID = 0;
    private float _enemyDamage = 0;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        if (_name == "Player")
        {
            PlayerLaserMovement();
        }
        else
        {
            EnemyLaserMovement();
        }

    }

    private void PlayerLaserMovement()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void EnemyLaserMovement()
    {
        SetEnemySpeed();
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _name != "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage(_enemyDamage);
            Destroy(gameObject);
        }
    }

    private void SetEnemySpeed()
    {
        switch(_enemyID)
        {
            case 0:
                _enemySpeed = _uiManager.GetVitesseEnnemi1() + 6.0f;
                break;
            case 1:
                _enemySpeed = _uiManager.GetVitesseEnnemi2() + 6.0f;
                break;
        }
    }

    public void SetEnemyType(int id, float damage)
    {
        _enemyID = id;
        _enemyDamage = damage;
    }
}
