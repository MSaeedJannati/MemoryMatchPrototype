using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainUICalsses;
using CustomEnums;
public class MainUiHandler : MonoBehaviour
{
    #region Variables
    //[0]==>gray,[1]==>gold
    [SerializeField] Sprite[] starSprites;

    //[0]==>off,[1]==>currentLevel,[2]==>passed
    [SerializeField] Color[] lampColours;
    [SerializeField] Object[] levels;


    public static MainUiHandler instance;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform scrollContent;
    [SerializeField] List<int> stars;
    #endregion
    #region properties
    public Sprite[] StarSprites => starSprites;
    public Color[] LampColours => lampColours;
    #endregion
    #region Monobehaviour callbacks
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
    }
    private void Start()
    {
        loadStars();
        CreateLevels();
    }
    #endregion
    #region Functions
    public void LoadLevel(int index)
    {
        Cache.LoadLevelJson(index-1, levels[index - 1].ToString(), false);
        SceneManagementLogic.instance.ChangeScene(Scenes.GAME);
    }
    public void loadStars()
    {
        stars = Cache.getLevelStars();
        if (stars.Count < levels.Length)
        {
            for (int i = stars.Count; i < levels.Length; i++)
            {
                stars.Add(0);
            }
            Cache.setStars(stars);
        }
    }
    
    public void CreateLevels()
    {
        int lastLevel = Cache.getLastLevel();
        for (int i = 0; i < levels.Length; i++)
        {
            if (ObjectPool.Instantiate(cardPrefab, scrollContent).
                TryGetComponent<LevelCard>(out var card))
            {
                card.setIndex(i + 1);
                if (i < lastLevel)
                {
                    card.setPassed(stars[i]);
                }
                else if (i == lastLevel)
                {
                    card.setCurrentLevel();
                }
                else
                {
                    card.disableCard();
                }
            }
        }

    }
    #endregion
}
