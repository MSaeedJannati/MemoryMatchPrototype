using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomEnums;
public class SceneManagementLogic : MonoBehaviour
{
    #region Variables
    [SerializeField] SceneManagerUi uiScrptRef;
    public static SceneManagementLogic instance;
    WaitForSeconds delay = new WaitForSeconds(.5f);
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
            if (instance != this)
                Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false);
    }
    #endregion
    #region Fucntions
    public void ChangeScene(Scenes scene)
    {

        gameObject.SetActive(true);
        StartCoroutine(ChangeSceneCoroutine(scene));
    }
    #endregion
    #region Coroutines
    IEnumerator ChangeSceneCoroutine(Scenes scene)
    {
        uiScrptRef.Fade(true);
        //scenes are loading too fast atm so later on we can get rid of this coroutine and move
        //it's contents to ChangeScene function
        yield return delay;
        yield return delay;
        SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single).completed += (assyncOpr) =>
        {
            uiScrptRef.Fade(false);
        };
        yield return delay;
        gameObject.SetActive(false);
    }
    #endregion

}
