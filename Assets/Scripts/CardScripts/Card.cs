using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    #region Variables
    [SerializeField] Transform cardBackTransform;
    [SerializeField] Transform cardForeTransform;
    [SerializeField] SpriteRenderer foreRenderer;
    [SerializeField] Transform mTransform;
    [SerializeField] float foreSpriteBaseScale;
    [SerializeField] CardAnimator mCardAnimator;
    [SerializeField] bool isHidden = true;
    [SerializeField] int id;
    #endregion
    #region Properties
    public  int ID => id;
    #endregion
    #region Functions
    public void SetCardAppearence(ref Vector3 pos,ref Vector3 scale)
    {
        mTransform.position = pos;
        cardBackTransform.localScale = scale;
        float smallerScale = scale.x >= scale.y ? scale.y : scale.x;
        cardForeTransform.localScale = smallerScale * foreSpriteBaseScale * Vector3.one;
    }
    public void SetForeAppearence(Color colour)
    {
        foreRenderer.color = colour;
    }
    public void OnClick()
    {
        if (!GameManager.CanSelectCard)
            return;
        Rotate();
        GameManager.instance.OnCardSelect(this);
    }
   public void Rotate()
    {
        if (isHidden)
            FlipBackward();
        else
            Flip();
    }
  
    void Flip()
    {
        if (!isHidden)
        {
            isHidden = true;
            mCardAnimator.Flip();
        }
    }
    
    void FlipBackward()
    {
        if (isHidden)
        {
            isHidden = false;
            mCardAnimator.FlipBackWard();
        }
    }
    public void SetId(int desiredId)
    {
        id = desiredId;
    }
    #endregion
}
