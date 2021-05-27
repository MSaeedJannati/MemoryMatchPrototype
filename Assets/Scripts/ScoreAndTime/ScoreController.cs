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
        GameManager.Match += CardsCountChange;
    }
    private void OnDisable()
    {
        GameManager.Match -= CardsCountChange;
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
     void ScoreChanged(int deltaScore)
    {
        int score = ScoreApp.instance.ModelRef.GetScore()+ deltaScore;
        ScoreApp.instance.ModelRef.SetScore(score);
        ScoreApp.instance.ViewRef.SetScoreText(score.ToString());
    }
     void CardsCountChange()
    {
        print("Here");
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
        ScoreApp.instance.TimeReachedZero();
    }
    #endregion
}
