using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    private int _score;

    void Start()
    {
      _scoreText.text = "Score: " + 0;
    }

    public void UpdateScore(int playerScore)
    {
      _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
      _livesImg.sprite = _liveSprites[currentLives];
    }
}