using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab = default;
    [SerializeField] private GameObject _laserAltPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _currentHealth = 0f;
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private bool _isLaserAltActive = false;
    [SerializeField] private float _healAmount = 5f;
    [SerializeField] private AudioClip _gameOverSound = default;

    private float _canFire = -1.0f;
    private float _initialSpeed;
    private SpawnManager _spawnManager;
    private HealthBar _healthBar;
    private Animator _anim;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _healthBar = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        _anim = GetComponent<Animator>();
    }
    private void Start()
    {
        transform.position = new Vector3(0f, -3.0f, 0f);
    }

    private void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3 (horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);

        if (horizontalInput < 0)
        {
            _anim.SetBool("TurnLeft", true);
            _anim.SetBool("TurnRight", false);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", true);
        }
        else
        {
            _anim.SetBool("TurnLeft", false);
            _anim.SetBool("TurnRight", false);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.1f, 2.3f), 0f);

        if (transform.position.x >= 10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0f);
        }
        else if (transform.position.x <= -10f)
        {
            transform.position = new Vector3(10f, transform.position.y, 0f);
        }
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
        {
            _canFire = Time.time + _fireRate;
            if (_isLaserAltActive)
            {
                Instantiate(_laserAltPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            }
            else
            { 
                Instantiate(_laserPrefab, transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity);
            }
        }
    }

    public void Damage(float damage)
    {
        _currentHealth -= damage;
        _healthBar.SetHealth(_currentHealth);

        if (_currentHealth < 1)
        {
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_gameOverSound, Camera.main.transform.position, 0.6f);
            Destroy(this.gameObject);
        }
    }

    public float GetMaxHealth()
    {
        return _maxHealth;
    }

    public void LaserAltPowerUp()
    {
        _isLaserAltActive = true;
        StartCoroutine(AltLaserRoutine());
    }

    IEnumerator AltLaserRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isLaserAltActive = false;
    }

    public void SpeedPowerUp()
    {
        _initialSpeed = _speed;
        _speed = _initialSpeed + 5f;
        StartCoroutine(SpeedRoutine());
    }

    IEnumerator SpeedRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speed = _initialSpeed;
    }

    public void HealPowerUp()
    {
        _currentHealth += _healAmount;
        if( _currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        _healthBar.SetHealth(_currentHealth);
    }
}
