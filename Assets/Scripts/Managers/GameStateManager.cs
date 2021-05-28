using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
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
    public void decreseCards(Vector3 pos)
    {
        remainingCards -= 2;
        if (remainingCards < 1)
        {
            Win();
        }
    }
    public void checkIfNewLevelUnlocked()
    {
        if (GameManager.instance.LevelIndex > Cache.getLastLevel())
        {
            Cache.setLastLevel(GameManager.instance.LevelIndex);
        }
    }
    public void checkForStars()
    {
        var score = ScoreApp.instance.ModelRef.GetScore();
        int starsGained = 0; ;
        print($"Score:{score}");
        print($"Ranges:{JsonConvert.SerializeObject(GameManager.instance.ScoreRanges)}");
        for (int i = 0; i < GameManager.instance.ScoreRanges. Length; i++)
        {
            if (score >= GameManager.instance.ScoreRanges[i])
            {
                starsGained++;
            }
        }
        var stars = Cache.getLevelStars();
        var index = GameManager.instance.LevelIndex - 1;
        if (starsGained > stars[index])
        {
            stars[index] = starsGained;
            Cache.setStars(stars);
        }
    }
    public void Win()
    {
        onWin?.Invoke();
        StartCoroutine(checkForStarsCoroutine());
        checkIfNewLevelUnlocked();
    }
    public void Lose()
    {
        onLose?.Invoke();
    }
    #endregion
    #region coroutines
    IEnumerator checkForStarsCoroutine()
    {
        yield return null;
        checkForStars();
    }
    #endregion
}
