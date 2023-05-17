using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private int _powerUpID = default;  //  _powerUpID   0 = LaserAlt    1 = Speed   2 = Heal

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            Destroy(this.gameObject);
            if(player != null)
            {
                switch (_powerUpID)
                {
                case 0:
                    player.LaserAltPowerUp();
                    break;
                case 1:
                    player.SpeedPowerUp();
                    break;
                case 2:
                    player.HealPowerUp();
                    break;
                }
            }
        }
    }
}
