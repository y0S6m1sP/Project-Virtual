using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearAnimationTriggers : MonoBehaviour
{
    private SamuraiSpear Enemy => GetComponentInParent<SamuraiSpear>();

    private void AnimationFinishTrigger()
    {
        Enemy.AnimationFinishTrigger();
    }
}
