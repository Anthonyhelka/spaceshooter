using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;

    void Start()
    {
      _player = GameObject.Find("Player").GetComponent<Player>();
      if (_player == null)
      {
        Debug.LogError("The Player is NULL.");
      }
    }

    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
          Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    { 
      if (other.tag == "Player" || other.tag == "Laser")
      {
        switch(powerupID)
        {
          case 0: 
            _player.TripleShotActive();
            break;
          case 1:
            _player.SpeedBoostActive();
            break;
          case 2:
            _player.ShieldsActive();
            break;
        }
        Destroy(this.gameObject);
      }
    }
}
