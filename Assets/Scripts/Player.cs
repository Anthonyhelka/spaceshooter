using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _laserPrefab;
    private SpawnManager _spawnManager;
    private Canvas _canvas;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private GameObject _thrusterVisualizer;
    private Animator _anim;
    [SerializeField] private GameObject _rightEngineVisualizer;
    [SerializeField] private GameObject _leftEngineVisualizer;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _speedMultiplier = 2f;
    [SerializeField] private float _fireRate = 0.25f;
    private float _canFire = -1f;
    private bool _tripleShotActive = false;
    private bool _shieldsActive = false;
    [SerializeField] private int _score = 0;

    void Start()
    {
        transform.position = new Vector3(0, -4.0f, 0);
        _anim = GetComponent<Animator>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _shieldVisualizer.SetActive(false);
        _thrusterVisualizer.SetActive(false);
        _rightEngineVisualizer.SetActive(false);
        _leftEngineVisualizer.SetActive(false);
        _canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL.");
        }
        if (_canvas == null)
        {
            Debug.LogError("The Canvas is NULL.");
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

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -4.5f, 4.5f));

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
        if (_shieldsActive == true)
        {
            _shieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        _lives--;
        _canvas.UpdateLives(_lives);

        if (_lives == 2)
        {
            _rightEngineVisualizer.SetActive(true);
        } 
        else if (_lives == 1)
        {
            _leftEngineVisualizer.SetActive(true);
        }

        if (_lives < 1)
        {
            _spawnManager.onPlayerDeath();
            Destroy(this.gameObject);
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

    public void SpeedBoostActive()
    {
        _speed *= _speedMultiplier;
        _thrusterVisualizer.SetActive(true);
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedMultiplier;
        _thrusterVisualizer.SetActive(false);
    }

    public void ShieldsActive()
    {
        _shieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _canvas.UpdateScore(_score);
    }
}
