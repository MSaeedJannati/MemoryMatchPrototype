using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePopHandler : MonoBehaviour
{
    #region Variables
    [SerializeField] TMPro.TMP_Text scoreText;

    WaitForSeconds delay = new WaitForSeconds(.9f);

    #endregion
    #region monobehaviourCallabacks
    private void OnEnable()
    {
        StartCoroutine(disableAfterDelay());
    }
    public void SetText(int Value)
    {
        scoreText.text =$"+{Value}";
    }
    #endregion
    #region Coroutines
    IEnumerator disableAfterDelay()
    {
        yield return delay;
        gameObject.SetActive(false);
    }
    #endregion
}
