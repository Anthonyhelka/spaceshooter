﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

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
      if (other.tag == "Player")
      {
        Player player = other.transform.GetComponent<Player>();
        if (player != null)
        {
          player.TripleShotActive();
          Destroy(this.gameObject);
        }
      }
    }
}
