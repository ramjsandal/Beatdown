using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    public TMP_Text scoreText;

    private int score;
    protected int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreText.text = value.ToString();
        }
    }

    public bool levelOver = false;

    public AudioSource audioSource;

    public GameObject gameoverUI;

    public TMP_Text gameoverText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }

    }

    private void Start()
    {
        levelOver = false;
    }

    public void GainPoints(int points)
    {
        if (!levelOver)
        {
            Score += points;
        }
    }

    public void LosePoints(int points)
    {
        if (!levelOver)
        {
            Score -= points;
        }
    }

    private void Update()
    {
        if (levelOver)
        {
            return;
        }

        if (!audioSource.isPlaying)
        {
            EndLevel(true);
            return;
        }

        if (Score < 0)
        {
            EndLevel(false); 
        }
    }

    public void EndLevel(bool win)
    {
        levelOver = true;
        gameoverUI.SetActive(true);

        if (win)
        {
            gameoverText.text = "LEVEL PASSED";
        } else
        {
            gameoverText.text = "LEVEL FAILED";
        }
    }

}
