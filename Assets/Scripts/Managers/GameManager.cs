using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManagerCalsses;

public class GameManager : MonoBehaviour
{
    #region Variables
    [SerializeField] GridCriterians Criterians;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GridInfo gridInfo;
    [SerializeField] Transform cardsParentTransform;
    [SerializeField] Vector2 TotalGap;
    [SerializeField] int levelInitTime;
    [SerializeField] int levlIndex;
    [SerializeField] Color[] colours;

   
    WaitForSeconds delay = new WaitForSeconds(.7f);


    public static GameManager instance;

    Card[] selectedCards;

    bool hasXPadding = false;
    bool hasYPadding = false;

    static bool canSelectCard;
    public delegate void myVoidEvent();
    public delegate void MyIntEvent(int cardsCount);
    public static event myVoidEvent Match;
    public static event myVoidEvent WrongMatch;
    public static event MyIntEvent OnLevelStart;
    #endregion
    #region properties
    public static bool CanSelectCard => canSelectCard;
    public int LevelIndex => levlIndex;
    public int StartTime => levelInitTime;
    #endregion
    #region MonobehaviourCallbacks
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
        selectedCards = new Card[2];
    }
    private void Start()
    {
        CreateGrid();

    }
    #endregion
    #region Functions
    [ContextMenu("create grid test")]
    void CreateGrid()
    {
        Vector3 scale = CalcScale();
        Vector3 pos = Vector3.zero;
        List<int> ids = creatIDList();
        colours = createColorList();
        for (int i = 0; i < gridInfo.rowCount; i++)
        {
            for (int j = 0; j < gridInfo.coloumnCount; j++)
            {
                if (ObjectPool.Instantiate(cardPrefab, cardsParentTransform).TryGetComponent(out Card card))
                {
                    pos = CalcPos(i, j, ref scale);
                    card.SetId(getRnadomIdFromList(ids));
                    card.SetCardAppearence(ref pos, ref scale);
                    card.SetForeAppearence(colours[card.ID]);
                }
            }
        }
        OnLevelStart?.Invoke(gridInfo.rowCount* gridInfo.coloumnCount);
        canSelectCard = true;
    }
    [ContextMenu("clear the grid")]
    void clearGrid()
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

    List<int> creatIDList()
    {

        int count = gridInfo.rowCount * gridInfo.coloumnCount / 2;
        List<int> ids = new List<int>(count * 2);
        for (int i = 0; i < count; i++)
        {
            ids.Add(i);
            ids.Add(i);
        }
        return ids;
    }
    Color[] createColorList()
    {
        var cols = new Color[gridInfo.rowCount * gridInfo.coloumnCount / 2];
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = new Color(getRandNum(), getRandNum(), getRandNum(), 1.0f);
        }
        return cols;
    }
    float getRandNum()
    {
        float output = (float)Random.Range(0, 255) / 255;
        return output;

    }
    int getRnadomIdFromList(List<int> ids)
    {
        int index = Random.Range(0, ids.Count);
        int outPut = ids[index];
        ids.RemoveAt(index);
        return outPut;

    }
    public void OnCardSelect(Card card)
    {
        if (selectedCards[0] == null)
        {
            selectedCards[0] = card;
        }
        else
        {
            if (card != selectedCards[0])
            {
                selectedCards[1] = card;
                checkForMatch();
            }
            else
            {
                selectedCards[0] = null;
            }

        }
    }
    void checkForMatch()
    {
        if (selectedCards[0].ID == selectedCards[1].ID)
        {
            OnMatch();
        }
        else
        {
            OnWrongMatch();
        }


    }
    void OnMatch()
    {
        StartCoroutine(OnMatchCoroutine());
    }
    void OnWrongMatch()
    {
        StartCoroutine(WrongMatchCoroutine());
    }
    #endregion

    #region coroutines
    IEnumerator WrongMatchCoroutine()
    {
        canSelectCard = false;
        yield return delay;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            selectedCards[i].Rotate();
            selectedCards[i] = null;
        }
        WrongMatch?.Invoke();
        canSelectCard = true;
    }
    IEnumerator OnMatchCoroutine()
    {
        canSelectCard = false;
        yield return delay;
        for (int i = 0; i < selectedCards.Length; i++)
        {
            selectedCards[i].gameObject.SetActive(false);
            selectedCards[i] = null;
        }
        Match?.Invoke();
        canSelectCard = true;
    }
    #endregion
}
