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

    private void AttackTrigger()
    {
        Hitbox hitbox = Enemy.GetComponentInChildren<Hitbox>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.attackCheckSize, 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                Debug.Log("Hit: " + hit.name);
            }
        }
    }
}
