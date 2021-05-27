using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform cardBackTransform;
    [SerializeField] Transform cardForeTransform;
    [SerializeField] Transform mTransform;
    [SerializeField] float foreSpriteBaseScale;
    #endregion
    #region Functions
    public void SetCardAppearence(ref Vector3 pos,ref Vector3 scale)
    {
        mTransform.position = pos;
        cardBackTransform.localScale = scale;
        float smallerScale = scale.x >= scale.y ? scale.y : scale.x;
        cardForeTransform.localScale = smallerScale * foreSpriteBaseScale * Vector3.one;
    }
    #endregion
}
