using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private float _speed = 4.0f;

    void Start()
    {
      _player = GameObject.Find("Player").GetComponent<Player>();
      float randomX = Random.Range(-8f, 8f);
      transform.position = new Vector3(randomX, 8, 0);
    }

    void Update()
    {
      transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
      if (transform.position.y < -5f)
      {
        float randomX = Random.Range(-8f, 8f);
        transform.position = new Vector3(randomX, 8, 0);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Player")
      {
        if (_player != null)
        {
          _player.Damage();
        }
        Destroy(this.gameObject);
      }
      else if (other.tag == "Laser")
      {
        Destroy(other.gameObject);
        if (_player != null)
        {
          _player.AddScore(Random.Range(5, 12));
        }
        Destroy(this.gameObject);
      }
    }
}
