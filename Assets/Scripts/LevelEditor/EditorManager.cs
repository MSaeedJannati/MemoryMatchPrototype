using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagerCalsses;
using Newtonsoft.Json;

public class EditorManager : MonoBehaviour
{
    #region Varaibeles
    public static EditorManager instance;
    public delegate void myIntDelegate(int value);
    public static event myIntDelegate cardActivisionChange;

    #region Grid creating variables
    [SerializeField] GridCriterians Criterians;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GridInfo gridInfo;
    [SerializeField] Transform cardsParentTransform;
    [SerializeField] Vector2 TotalGap;
    bool hasXPadding = false;
    bool hasYPadding = false;
    #endregion
    #region LevelData

    [SerializeField] int cardsCount;
    [SerializeField] int levelTime;
    [SerializeField] int[] scoreRanges;
    [SerializeField] int[,] GridCards;
    #endregion
    public EditorUI editorUiRef;
    public LevelSelection levelSelectingRef;

    [SerializeField] string inputData;

    public List<string> levels;

    public int levelIndex;
    string path = "Assets\\Levels\\level";
    #endregion
    #region MonoBehaviour callbacks
    private void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    #region Functions


    #region gridCreation
    public void OnGenerate(int[] girdDimensions, int[] scores, int time)
    {
        clearGrid();
        cardsCount = 0;
        levelTime = time;
        scoreRanges = scores;
        gridInfo.rowCount = girdDimensions[0];
        gridInfo.coloumnCount = girdDimensions[1];
        if (gridInfo.rowCount > 0 || gridInfo.coloumnCount > 0)
        {
            CreateGrid();
        }
    }
    void CreateGrid(int[,] Grid = null)
    {
        Vector3 scale = CalcScale();
        Vector3 pos = Vector3.zero;
        bool hasGrid = false;
        if (Grid != null)
        {
            GridCards = Grid;
            hasGrid = true;
        }
        else
        {
            GridCards = new int[gridInfo.rowCount, gridInfo.coloumnCount];
        }
        for (int i = 0; i < gridInfo.rowCount; i++)
        {
            for (int j = 0; j < gridInfo.coloumnCount; j++)
            {
                if (ObjectPool.Instantiate(cardPrefab, cardsParentTransform).TryGetComponent(out GridCard card))
                {
                    pos = CalcPos(i, j, ref scale);
                    card.SetCardAppearence(ref pos, ref scale);
                    if (hasGrid)
                    {
                        card.activate(Grid[i, j] == 1);
                    }
                    else
                    {
                        card.activate(true);
                        GridCards[i, j] = 1;
                    }
                    card.SetRowAnCol(i, j);
                }
            }
        }
    }
   public  void clearGrid()
    {
        for (int i = cardsParentTransform.childCount - 1; i >= 0; i--)
        {
            cardsParentTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
    Vector3 CalcScale()
    {
        Vector3 scale = Vector3.one;
        float deltaY = Criterians.topLeft.position.y - Criterians.bottomRight.position.y;
        float deltaX = Criterians.bottomRight.position.x - Criterians.topLeft.position.x;
        float xSections;
        float ySections;

        if (gridInfo.coloumnCount > 1)
        {
            xSections = gridInfo.coloumnCount + TotalGap.x;
            hasXPadding = true;
        }
        else
        {
            xSections = gridInfo.coloumnCount;
            hasXPadding = false;
        }
        if (gridInfo.rowCount > 1)
        {
            ySections = gridInfo.rowCount + TotalGap.y;
            hasYPadding = true;
        }
        else
        {
            ySections = gridInfo.rowCount;
            hasYPadding = false;
        }
        scale.x = deltaX / (xSections);
        scale.y = deltaY / (ySections);
        return scale;
    }
    Vector3 CalcPos(int row, int coloumn, ref Vector3 scale)
    {
        Vector3 pos = Vector3.zero;
        float xPadding = hasXPadding ?
            coloumn * TotalGap.x * scale.x / (gridInfo.coloumnCount - 1) :
            0.0f;
        float yPadding = hasYPadding ?
            row * TotalGap.y * scale.y / (gridInfo.rowCount - 1) :
            0.0f;
        pos.x = scale.x * coloumn + xPadding + scale.x / 2;
        pos.y = -(scale.y * row + yPadding + scale.y / 2);
        pos += Criterians.topLeft.position;
        return pos;

    }
    public void CardActivisionChanged(int delta)
    {
        cardsCount += delta;
        cardActivisionChange?.Invoke(cardsCount);
    }
    public void ChangeGridUnitValue(int row, int col, int value)
    {
        GridCards[row, col] = value;
    }
    #endregion
    public void Save()
    {
        Hashtable levelData = new Hashtable();
        levelData.Add("level_time", levelTime);
        levelData.Add("grid_Info", gridInfo);
        levelData.Add("score_ranges", scoreRanges);
        levelData.Add("cards_count", cardsCount);
        levelData.Add("grid_status", GridCards);
        string serilizedLevel = JsonConvert.SerializeObject(levelData);
        SaveLevel(serilizedLevel, (levelIndex).ToString());
        levelSelectingRef.gameObject.SetActive(true);
    }
    public void LoadLevel(int index)
    {
        levelIndex = index+1;
        EditorManager.instance.editorUiRef.SetLevelTxt(levelIndex);
        LoadData(levels[index].ToString());
        setInputValueAfterLoad();
        clearGrid();
        CreateGrid(GridCards);
        levelSelectingRef.gameObject.SetActive(false);
    }
    public void LoadData(string jsonString)
    {
        Hashtable levelData = new Hashtable();
        levelData = JsonConvert.DeserializeObject(jsonString, typeof(Hashtable)) as Hashtable;
        if (levelData.ContainsKey("level_time"))
        {
            int.TryParse(levelData["level_time"].ToString(), out levelTime);
        }
        if (levelData.ContainsKey("grid_Info"))
        {
            gridInfo = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["grid_Info"]),
                typeof(GridInfo)
                ) as GridInfo;
        }
        if (levelData.ContainsKey("score_ranges"))
        {
            scoreRanges = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["score_ranges"]),
                typeof(int[])
                ) as int[];
        }
        if (levelData.ContainsKey("grid_status"))
        {
            GridCards = JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(levelData["grid_status"]),
                typeof(int[,])
                ) as int[,];
        }
    }
    [ContextMenu("Load Test")]
    void TestLoad()
    {
        levelIndex = 1;
        LoadData(inputData);
        setInputValueAfterLoad();
        clearGrid();
        CreateGrid(GridCards);
    }
    public void setInputValueAfterLoad()
    {
        int[] gridInfos = new int[2];
        gridInfos[0] = gridInfo.rowCount;
        gridInfos[1] = gridInfo.coloumnCount;
        editorUiRef.SetGridInfo(gridInfos);
        editorUiRef.SetTime(levelTime);
        editorUiRef.SetScoreRanges(scoreRanges);
    }
    public void resetUi()
    {
        int[] gridInfos = new int[2];
        gridInfos[0] = 0;
        gridInfos[1] = 0;
        editorUiRef.SetGridInfo(gridInfos);
        editorUiRef.SetTime(0);
        editorUiRef.SetScoreRanges(new int[] { 0, 0, 0 });
        editorUiRef.SetCardsText(0);
    }
    [ContextMenu("LoadingLevels")]
    public void loadLevelsFromMemory()
    {

        string fullAddress = string.Empty;
        levels = new List<string>();
        int index = 1;
        while (true)
        {
            fullAddress = $"{path}{index}.json";
            if (FileHandler.FileExsits(fullAddress))
            {
                levels.Add(FileHandler.LoadFile(fullAddress));
                index++;
            }
            else
            {
                break;
            }
        }
    }
    public void SaveLevel(string json, string index)
    {
        var fullAddress = $"{path}{index}.json";
        FileHandler.SaveFile(json, fullAddress);
    }

    public void BackToSelection()
    {
        cardsCount = 0;
        levelSelectingRef.gameObject.SetActive(true);
    }
    public void playTestLevel()
    {
        Cache.LoadLevelJson(levelIndex-1,levels[levelIndex-1],true);
        SceneManagementLogic.instance.ChangeScene(CustomEnums.Scenes.GAME);
    }
    #endregion
}
