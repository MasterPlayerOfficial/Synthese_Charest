using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Player _player;
    private Slider _healthBar;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _healthBar = GetComponent<Slider>();
        _healthBar.maxValue = _player.GetMaxHealth();
        _healthBar.value = _player.GetMaxHealth();
    }

    public void SetHealth(float health)
    {
        _healthBar.value = health;
    }
}
