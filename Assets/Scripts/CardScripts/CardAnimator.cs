using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardAnimator : MonoBehaviour
{
    #region Variables
    [SerializeField] Animator mAnimator;
    static int flipHash;
    static int flipBackwardHash;
    #endregion
    #region MonoBehaviour callbacks
    private void Start()
    {
        if (flipHash == 0)
        {
            flipHash = Animator.StringToHash("Flip");
            flipBackwardHash = Animator.StringToHash("FlipBackward");
        }
    }
    #endregion
    #region Functions
    public void Flip()
    {
        mAnimator.SetTrigger(flipHash);
    }
    public void FlipBackWard()
    {
        mAnimator.SetTrigger(flipBackwardHash);
    }
    #endregion
}
