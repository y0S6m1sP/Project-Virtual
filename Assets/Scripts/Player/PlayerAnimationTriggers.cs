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

    private void AttackTrigger()
    {
        Hitbox hitbox = Player.GetComponentInChildren<Hitbox>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(hitbox.transform.position, hitbox.attackCheckSize, 0);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {   
                if (hit.TryGetComponent<EnemyStats>(out var _target))
                {
                    Player.Stats.DoDamage(_target);
                }
            }
        }
    }

    private void OpenParryWindow() => Player.OpenParryWindow();

    private void CloseParryWindow() => Player.CloseParryWindow();
}
