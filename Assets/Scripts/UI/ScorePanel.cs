using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScorePanel : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int targetScore = 0;
    int increaseScore = 1;
    float currentScore = 0.0f;
    public int increaseInterval = 5;

    private void Start()
    {
        scoreText = transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        Player player = FindObjectOfType<Player>();
        player.onScoreChange += SetTargetScore;

    }

    private void Update()
    {
        if (currentScore < targetScore)
        {
            increaseScore = targetScore /increaseInterval;
            currentScore += Time.deltaTime * increaseScore;
            
            currentScore = Mathf.Min(currentScore, targetScore);// 두값을 비교하여 더 작은수를 반환
            
            scoreText.text = $"{currentScore:f0}";
        }
    }

    void SetTargetScore(int newscore)
    {
        //scoreText.text = newscore.ToString();
        targetScore = newscore;
    }
}
