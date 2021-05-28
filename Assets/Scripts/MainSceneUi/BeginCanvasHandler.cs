using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginCanvasHandler : MonoBehaviour
{
    #region Variables
    static bool AlreadyShown;
    #endregion
    #region monobehaviour callbacks
    private void OnEnable()
    {
        if (AlreadyShown)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion
    #region Functions
    public void ClickedOnPlay()
    {
        AlreadyShown = true;
        gameObject.SetActive(false);
    }
    #endregion
}
