using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneUiHandler : MonoBehaviour
{
    #region Variables
    public GameObject winPopUp;
    public GameObject losePopUp;
    public GameObject pausePopUp;
    #endregion
    #region MonoBehaviour callbacks
    private void OnEnable()
    {
        GameStateManager.onWin += LevelWon;
        GameStateManager.onLose += LevelLost;
        ScoreApp.OnPuase += Pause;
    }
    private void OnDisable()
    {
        GameStateManager.onWin -= LevelWon;
        GameStateManager.onLose -= LevelLost;
        ScoreApp.OnPuase -= Pause;
    }
    #endregion
    #region Functions
    public void LevelWon()
    {
        winPopUp.SetActive(true);
    }
    public void LevelLost()
    {
        losePopUp.SetActive(true);
    }
    public void Pause(bool paused)
    {
        pausePopUp.SetActive(paused);
    }
    public void Continue()
    {
        pausePopUp.SetActive(false);
    }
  
    #endregion
}
