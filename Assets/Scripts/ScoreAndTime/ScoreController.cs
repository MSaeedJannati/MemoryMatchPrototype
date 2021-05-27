using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    #region Variables
    WaitForSeconds oneSec = new WaitForSeconds(1.0f);
    
    #endregion
    #region MonobehaviourCallBacks
    private void OnEnable()
    {
        GameManager.Match += Matched;
        GameManager.WrongMatch += WrongMatch;
        GameStateManager.onWin += OnWin;
        ScoreApp.OnPuase += Pause;
    }
    private void OnDisable()
    {
        GameManager.Match -= Matched;
        GameManager.WrongMatch -= WrongMatch;
        GameStateManager.onWin -= OnWin;
        ScoreApp.OnPuase -= Pause;
    }
    #endregion
    #region Functions
    public void init()
    {
        int score = ScoreApp.instance.ModelRef.GetScore();
        ScoreApp.instance.ViewRef.SetScoreText(score.ToString());

        int cards = ScoreApp.instance.ModelRef.GetRemainingCards();
        string goalText = $"{cards}/{ScoreApp.instance.ModelRef.GetInitCardCount()}";
        ScoreApp.instance.ViewRef.SetGoalText(goalText);

        int lvl = ScoreApp.instance.ModelRef.GetLevelIndex();
        ScoreApp.instance.ViewRef.SetLevelText(lvl.ToString());

        StartCoroutine(TimreCoroutine());
    }
    public void Matched(Vector3 pos)
    {
        CardsCountChange();
        int deltaScore = ScoreApp.instance.ModelRef.GetBaseScore() * (1 + ScoreApp.instance.ModelRef.GetCombo());
        ScoreApp.instance.ModelRef.IncreaseCombo();
        ScoreChanged(deltaScore);
        CreateScoreText(pos, deltaScore);
    }
    public void WrongMatch()
    {
        ScoreApp.instance.ModelRef.ResetCombo();
    }
    public void OnWin()
    {
        ScoreApp.instance.ModelRef.SetIsPaused(true);

    }
     void ScoreChanged(int deltaScore)
    {
        int score = ScoreApp.instance.ModelRef.GetScore()+ deltaScore;
        ScoreApp.instance.ModelRef.SetScore(score);
        ScoreApp.instance.ViewRef.SetScoreText(score.ToString());
    }
     void CardsCountChange()
    {
        int cards = ScoreApp.instance.ModelRef.GetRemainingCards() - 2;
        ScoreApp.instance.ModelRef.SetRemainingCards(cards);
        string goalText = $"{cards}/{ScoreApp.instance.ModelRef.GetInitCardCount()}";
        ScoreApp.instance.ViewRef.SetGoalText(goalText);
    }
    public void checkForLowTime(int remainingTime)
    {
        if (remainingTime < 30)
        {
            ScoreApp.instance.ViewRef.SetTimerTextColour(Color.red);
        }
    }
    public void Pause(bool paused)
    {
        ScoreApp.instance.ModelRef.SetIsPaused(paused);
        
    }
    public void CreateScoreText(Vector3 pos,int deltaScore)
    {
        if (ObjectPool.Instantiate(
            ScoreApp.instance.ModelRef.scorePopPrefab, 
            pos, Quaternion.identity).
            TryGetComponent<ScorePopHandler>(out var scoreTxt))
        {
            scoreTxt.SetText(deltaScore);
        }
    }
    #endregion
    #region Coroutines
    IEnumerator TimreCoroutine()
    {
        int totalTime = ScoreApp.instance.ModelRef.GetLevelTime();
        int remainingTime= ScoreApp.instance.ModelRef.GetRemainingTime();
        float timerFillAmount;
        string TimeString = string.Empty;
        ScoreApp.instance.ViewRef.SetTimerTextColour(Color.white);
        while (remainingTime > 0)
        {
            remainingTime--;
            ScoreApp.instance.ModelRef.SetRemainingTime(remainingTime);
            TimeString = $"{remainingTime/60}\':{remainingTime%60}\"";
            timerFillAmount = (float)remainingTime / totalTime;
            ScoreApp.instance.ViewRef.SetTimerText(TimeString);
            ScoreApp.instance.ViewRef.SetForeImageFillAmount(timerFillAmount);
            checkForLowTime(remainingTime);
            while (ScoreApp.instance.ModelRef.GetIsPaused())
            {
                yield return null;
            }
            yield return oneSec;
        }
        ScoreApp.instance.ModelRef.SetIsPaused(true);
        ScoreApp.instance.TimeReachedZero();
    }
    #endregion
}
