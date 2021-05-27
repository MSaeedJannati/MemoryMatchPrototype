using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    #region Variables
    public delegate void myVoidEvent();

    [SerializeField]int remainingCards;
    int levelTotalInitCard;

    public static event myVoidEvent onWin;
    public static event myVoidEvent onLose;
    #endregion
    #region Monobehaviour Callbacks
    private void OnEnable()
    {
        GameManager.OnLevelStart += SetLevelTarget;
        GameManager.Match += decreseCards;
        ScoreApp.timeReachedZero += Lose;
    }
    private void OnDisable()
    {
        GameManager.OnLevelStart -= SetLevelTarget;
        GameManager.Match -= decreseCards;
        ScoreApp.timeReachedZero -= Lose;
    }

    #endregion
    #region Functions
    public void SetLevelTarget(int cardsCount)
    {
        levelTotalInitCard = cardsCount;
        remainingCards = cardsCount;
    }
    public void decreseCards()
    {
        remainingCards -= 2;
        if (remainingCards < 1)
        {
            Win();
        }
    }
    public void Win()
    {
        onWin?.Invoke();
    }
    public void Lose()
    {
        onLose?.Invoke();
    }
    #endregion
}
