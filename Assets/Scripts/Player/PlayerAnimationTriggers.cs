using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player Player => GetComponentInParent<Player>();

    private void AnimFinishTrigger()
    {
        Player.AnimFinishTrigger();
    }

    private void AnimAllowCancel()
    {
        Player.AnimAllowCancel();
    }
}
