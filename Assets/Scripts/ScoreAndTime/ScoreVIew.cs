using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreVIew : MonoBehaviour
{
    #region Variables
    [SerializeField] Image timeForeGroundImage;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text GoalText;
    [SerializeField] TMP_Text ScoreText;
    [SerializeField] TMP_Text LevelText;
    #endregion
    #region Functions
    public void SetScoreText(string data)
    {
        ScoreText.text = data;
    }
    public void SetGoalText(string data)
    {
        GoalText.text = data;
    }
    public void SetTimerText(string data)
    {
        timerText.text = data;
    }
    public void SetLevelText(string data)
    {
        LevelText.text = data;
    }

    public void SetForeImageFillAmount(float amount)
    {
        timeForeGroundImage.fillAmount = amount;
    }
    public void SetTimerTextColour(Color colour)
    {
        timerText.color = colour;
    }
    public void ClickOnPause()
    {
        
    }
    #endregion
}
