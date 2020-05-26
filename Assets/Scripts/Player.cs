using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 8f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    private float _canFire = -1f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _tripleShotActive;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private Animator _anim;

    void Start()
    {
      transform.position = new Vector3(0, 0, 0);
      _anim = GetComponent<Animator>();
      _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
      if (_spawnManager == null)
      {
        Debug.LogError("The Spawn Manager is NULL.");
      }
    }

    void Update()
    {
      CalculateMovement();
      if (Input.GetKey(KeyCode.Space) && Time.time > _canFire)
      {
        fireLaser();
      }
    }

    void CalculateMovement()
    {
      float horizontalInput = Input.GetAxis("Horizontal");
      float verticalInput = Input.GetAxis("Vertical");

      Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
      transform.Translate(direction *_speed * Time.deltaTime);

      transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0));

      if (transform.position.x >= 11.3f)
      {
        transform.position = new Vector3(-11, transform.position.y, transform.position.z);
      }
      else if (transform.position.x <= -11.3f)
      {
        transform.position = new Vector3(11, transform.position.y, transform.position.z);
      }

      if (horizontalInput < 0)
      {
        _anim.Play("Player_turn_left_anim");
      }
      else if (horizontalInput > 0)
      {
        _anim.Play("Player_turn_right_anim");
      }
      else
      {
        _anim.Play("Player_idle_anim");
      }
    }

    void fireLaser()
    {
      _canFire = Time.time + _fireRate;
      if (_tripleShotActive)
      {
        Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
      } else {
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
      }
    }

    public void Damage()
    {
      _lives--;
      if (_lives < 1)
      {
        _spawnManager.onPlayerDeath();
        Debug.Log("Game Over!");
        Destroy(this.gameObject);
      }
      else
      {
        Debug.Log("Lives Remaining: " + _lives);
      }
    }

    public void TripleShotActive()
    {
      _tripleShotActive = true;
      StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
      yield return new WaitForSeconds(5.0f);
      _tripleShotActive = false;
    }
}
