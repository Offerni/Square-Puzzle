﻿using GooglePlayGames;
using GooglePlayGames.BasicApi;
using SquareConstants;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour {
    private int score;
    private int bestScore;
    private int scoreAux;
    public int deadCount = 0;
    private AdManager adManager;
    public int adCount;

    private void Awake() {
        SetUpSingleton();
        RandomizeAdCount();
    }

    public int GetScore() {
        return score;
    }

    public void AddScore(int scorePoint) {

        this.score += scorePoint;
        if (this.score > this.bestScore) {
            bestScore = score;
        }
    }

    public void ResetScore() {
        this.score = 0;
    }

    public int GetBestScore() {
        return this.bestScore;
    }

    private void SetUpSingleton() {
        int count = FindObjectsOfType<GameSession>().Length;

        if (count > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetLeaderBoardBestScore() {
        bestScore = 0;
    }

    public void ResetDeadCount() {
        deadCount = 0;
    }

    public void RandomizeAdCount() {
        adCount = UnityEngine.Random.Range(4, 8);
    }

    public void SaveScore() {
        if (Social.localUser.authenticated) {
            // Note: make sure to add 'using GooglePlayGames'
            if (SceneManager.GetActiveScene().name == "Level1") {
                Social.ReportScore(score,
               GPGSIds.leaderboard_top_players,
               (bool success) => {
                   Debug.Log("(Square) Leaderboard update success: " + success);
               });
            } else if (SceneManager.GetActiveScene().name == "Level2") {
                Social.ReportScore(score,
               GPGSIds.leaderboard_top_impossible_players,
               (bool success) => {
                   Debug.Log("(Square) Leaderboard update success: " + success);
               });
            }
        }
    }
}
