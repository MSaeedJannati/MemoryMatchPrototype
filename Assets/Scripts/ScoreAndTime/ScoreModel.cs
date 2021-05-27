using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreModel : MonoBehaviour
{
    #region Variables
    [SerializeField] int levelTime;
    [SerializeField] int remainingTime;
    [SerializeField] int levelIndex;
    [SerializeField] int remainingCards;
    [SerializeField] int initCardCount;
    [SerializeField] int score;
    [SerializeField] bool IsPaused;
    #endregion
    #region Functions

    #region Getters
    public int GetLevelTime() => levelTime;
    public int GetRemainingTime() => remainingTime;
    public int GetLevelIndex() => levelIndex;
    public int GetRemainingCards() => remainingCards;
    public int GetInitCardCount() => initCardCount;
    public int GetScore() => score;
    public bool GetIsPaused() => IsPaused;
    #endregion

    #region Setters
    public void SetLevelTime(int time)
    {
        levelTime = time;
    }
    public void SetRemainingTime(int time)
    {
        remainingTime = time;
    }
    public void SetLevelIndex(int index)
    {
        levelIndex = index;
    }
    public void SetRemainingCards(int cardsCount)
    {
        remainingCards = cardsCount;
    }
    public void SetinitCardCount(int cardsCount)
    {
        initCardCount = cardsCount;
    }
    public void SetScore(int currentScore)
    {
        score = currentScore;
    }
    public void SetIsPaused(bool value)
    {
        IsPaused = value;
    }
    #endregion

    #endregion
}
