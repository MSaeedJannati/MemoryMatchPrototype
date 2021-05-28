using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelCard : MonoBehaviour
{
    #region Variables
    [SerializeField] Image lampImage;
    [SerializeField] GameObject starsParent;
    [SerializeField] Image[] starsImages;
    [SerializeField] TMP_Text indexTxt;
    [SerializeField] Button cardBtn;
    int index;
    #endregion
    #region Properties
    public int Index => index;
    #endregion
    #region Functions
    public void disableCard()
    {
        lampImage.color = MainUiHandler.instance.LampColours[0];
        starsParent.SetActive(false);
        cardBtn.interactable=false;
    }
    public void setCurrentLevel()
    {
        lampImage.color = MainUiHandler.instance.LampColours[1];
        starsParent.SetActive(false);
        cardBtn.interactable = true;
    }
    public void setPassed(int starCount=0)
    {
        lampImage.color = MainUiHandler.instance.LampColours[2];
        starsParent.SetActive(true);
        cardBtn.interactable = true;
        for (int i = 0; i < 3; i++)
        {
            if (i + 1 <= starCount)
            {
                starsImages[i].sprite = MainUiHandler.instance.StarSprites[1];
            }
            else
            {
                starsImages[i].sprite = MainUiHandler.instance.StarSprites[0];
            }
        }
    }
    public void setIndex(int num)
    {
        index = num;
        indexTxt.text = index.ToString();
    }
   public  void Onclick()
    {
        MainUiHandler.instance.LoadLevel(index);
    }
    #endregion
}
