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
            if (hit.TryGetComponent<Player>(out var player))
            {
                if (player.isParryActive && Enemy.canBeParrried)
                {
                    StartCoroutine(player.ParrySuccess(Enemy.Stats));

                    player.Skill.Parry.UseSkill(); // this is an example, if not used, remove this line

                    return;
                }

                Enemy.Stats.DoDamage(player.Stats);
            }
        }
    }

    private void OpenParryWindow() => Enemy.OpenParryWindow();

    private void CloseParryWindow() => Enemy.CloseParryWindow();

}
