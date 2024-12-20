using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI gameScoretext;
    [SerializeField] private TextMeshProUGUI menubestScoretext;

    [Header(" Settings")]
    [SerializeField] private float scoremultiplier;
    private int score;
    private int bestScore;

    [Header(" Data ")]
    private const string bestScoreKey = "bestScoreKey";


    private void Awake()
    {
        Loaddata();
        MergeManager.onmergeprocess += MergeProcessedCallback;
        GameManager.onGameStateChanged += OnGameStateChangedCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onmergeprocess -= MergeProcessedCallback;
        GameManager.onGameStateChanged -= OnGameStateChangedCallback;

    }
    // Start is called before the first frame update
    void Start()
    {
        Updatescoretext();

        UpdateBestScoretext();

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Gameover:
                CalculateBestScore();
                break;   
        }

    }

    private void MergeProcessedCallback(Sattype sattype, Vector2 unused)
    {
        int scoretoAdd = (int)sattype;
        score += (int)(scoretoAdd * scoremultiplier);
    
        Updatescoretext();

    }

    private void Updatescoretext()
    {
         gameScoretext.text = score.ToString();

    }

    private void UpdateBestScoretext()
    {
        menubestScoretext.text = bestScore.ToString();
    }

    private void CalculateBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            SaveData();
        }


    }

    private void Loaddata()
    {
        bestScore = PlayerPrefs.GetInt(bestScoreKey);

    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(bestScoreKey, bestScore);

    }
}

    
