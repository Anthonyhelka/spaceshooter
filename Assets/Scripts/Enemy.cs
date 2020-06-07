using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;
    private BoxCollider2D _bc;
    private Animator _anim;
    [SerializeField] private float _speed = 4.0f;

    void Start()
    {   
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.LogError("The Player is NULL.");
        }
        _bc = GetComponent<BoxCollider2D>();
        if (_bc == null)
        {
            Debug.LogError("The BoxCollider2D is NULL.");
        }
        _anim = GetComponent<Animator>();
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }
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
            _player.Damage();
            StartCoroutine(DeathRoutine());
      }
        else if (other.tag == "Laser")
        {
            _player.AddScore(Random.Range(5, 12));
            StartCoroutine(DeathRoutine());
        }
    }

    IEnumerator DeathRoutine()
    {
        while (true)
        {
            _bc.enabled = false;
            _anim.SetTrigger("onEnemyDeath");
            yield return new WaitForSeconds(1.4f);
            Destroy(this.gameObject);
        }
    }
}
