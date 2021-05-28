using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManagerUi : MonoBehaviour
{
    #region Variables
    [SerializeField] Image[] Images;
    [SerializeField] TMPro.TMP_Text[] uiTexts;
    #endregion
    #region Functions
    public void Fade(bool fadeIn)
    {
        StartCoroutine(FadeCoroutine(fadeIn));
    }
    #endregion
    #region coroutines
    IEnumerator FadeCoroutine(bool fadeIn)
    {

        float initAlpha = fadeIn ? 0.0f : 1.0f;
        float destAlpha = fadeIn ? 1.0f : 0.0f;
        Color colour = Color.white;
        float t = 0.0f;
        float period = .5f;
        float currentAlpha = 0.0f;
        while (t < period)
        {
            t += Time.deltaTime;
            currentAlpha = initAlpha + (destAlpha - initAlpha) * (t / period);
            for (int i = 0; i < Images.Length; i++)
            {
                colour = Images[i].color;
                colour.a = currentAlpha;
                Images[i].color = colour;
            }
            for (int i = 0; i < uiTexts.Length; i++)
            {
                colour = uiTexts[i].color;
                colour.a = currentAlpha;
                uiTexts[i].color = colour;
            }
            yield return null;
        }
    }
    #endregion
}
