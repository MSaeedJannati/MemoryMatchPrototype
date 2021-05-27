using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreApp : MonoBehaviour
{
    #region Variables
    public static ScoreApp instance;
    [SerializeField] ScoreController controllerRef;
    [SerializeField] ScoreModel modelRef;
    [SerializeField] ScoreVIew viewRef;


    public delegate void MyVoidDelegate();
    public delegate void MyBoolDelegate(bool pause);
    public static event MyVoidDelegate timeReachedZero;
    public static event MyBoolDelegate OnPuase;
    #endregion
    #region Getter Properties
    public ScoreController ControllerRef => controllerRef;
    public ScoreModel ModelRef => modelRef;
    public ScoreVIew ViewRef => viewRef;
    #endregion
    #region MonobehaviourCallBacks
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        GameManager.OnLevelStart += GameStarted;
    }
    private void OnDisable()
    {
        GameManager.OnLevelStart -= GameStarted;
    }
    #endregion
    #region Functions
    public void TimeReachedZero()
    {
        timeReachedZero?.Invoke();
    }
    void GameStarted(int cardsCount)
    {
        initModel(cardsCount);
        ControllerRef.init();

    }
    void initModel(int cardsCount)
    {
        modelRef.SetinitCardCount(cardsCount);
        modelRef.SetIsPaused(false);
        modelRef.SetLevelIndex(GameManager.instance.LevelIndex);
        modelRef.SetLevelTime(GameManager.instance.StartTime);
        modelRef.SetRemainingCards(cardsCount);
        modelRef.SetRemainingTime(GameManager.instance.StartTime);
        modelRef.SetScore(0);
        modelRef.ResetCombo();
        modelRef.SetBaseScore(GameManager.instance.BaseScore);
    }
   public  void Pause(bool pause)
    {
        OnPuase?.Invoke(pause);
    }
    #endregion
}
