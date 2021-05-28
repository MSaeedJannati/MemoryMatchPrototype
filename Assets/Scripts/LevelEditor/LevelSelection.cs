using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    #region Variabels
    [SerializeField] GameObject editorCardPrefab;
    [SerializeField] Transform levelCardsParent;
    #endregion
    #region monobehaviourCallbacks
    private void Start()
    {
        ClearGrid();
        GenerateCards();
    }
    #endregion

    #region Functions
    void ClearGrid()
    {
        for (int i = levelCardsParent.childCount - 1; i >= 0; i--)
        {
            levelCardsParent.GetChild(i).gameObject.SetActive(false);
        }
    }
    void GenerateCards()
    {
        EditorManager.instance.loadLevelsFromMemory();
        for (int i = 0; i < EditorManager.instance.levels.Count; i++)
        {
            if (ObjectPool.Instantiate(editorCardPrefab, levelCardsParent).
                TryGetComponent<EditorLevelCard>(out var card))
            {
                card.SetIndex(i);
            }
        }
    }
    public void AddClicked()
    {
        EditorManager.instance.levelIndex = levelCardsParent.childCount + 1;
        EditorManager.instance.editorUiRef.SetLevelTxt(levelCardsParent.childCount + 1);
        EditorManager.instance.clearGrid();
        EditorManager.instance.resetUi();
        gameObject.SetActive(false);
    }

    #endregion
}
