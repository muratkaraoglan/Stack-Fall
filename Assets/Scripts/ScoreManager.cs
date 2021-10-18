using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public static ScoreManager Instance { get { return instance; } }
    public Text scoreText;
    public Transform popupText;
    int score = 0;
    private void Awake()
    {
        if ( instance != null && instance != this )
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        AddScore(0);
    }

    private void Update()
    {
        if ( scoreText == null )
        {
            scoreText = GameObject.Find("Score Text").GetComponent<Text>();
            scoreText.text = score.ToString();
        }
    }

    public void AddScore(int scorePoint)
    {
        score += scorePoint;
        if ( PlayerPrefs.GetInt("HighScore", 0) < score )
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        scoreText.text = score.ToString();
    }

    public void ResetScore() => score = 0;

    public void PopupText(Vector3 position, string text)
    {
        Transform popup = Instantiate(popupText, position + Vector3.left, Quaternion.identity);
        popup.GetComponent<PopupText>().displayText = text;
    }

}
