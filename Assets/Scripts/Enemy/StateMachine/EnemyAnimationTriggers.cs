using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTriggers : MonoBehaviour
{
    protected Enemy Enemy => GetComponentInParent<Enemy>();

    private Vector3 defaultPosition;

    [Header("Animation movement")]
    public Transform proxy;
    public Transform rootPrarent;
    public Transform rightBoundary;
    public Transform leftBoundary;

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

    private void UpdateParentPosition()
    {
        if (proxy != null && rootPrarent != null)
        {
            Vector3 offset = proxy.position - rootPrarent.position;
            offset.y = 0; // Ignore y position change
            // if (proxy.position.x > rightBoundary.position.x || proxy.position.x < leftBoundary.position.x)
            //     return;
            rootPrarent.position += offset;
        }
    }

    private void StartActualTeleportToPlayer()
    {
        rootPrarent.position = PlayerManager.instance.player.transform.position;
    }

    private void StartAnimTeleportToPlayer()
    {
        defaultPosition = new Vector3(Enemy.Anim.transform.position.x, Enemy.Anim.transform.position.y, Enemy.Anim.transform.position.z);

        Enemy.Anim.transform.position = PlayerManager.instance.player.transform.position;
    }

    private void StartTeleportToPlayerWithOffset()
    {
        defaultPosition = new Vector3(Enemy.Anim.transform.position.x, Enemy.Anim.transform.position.y, Enemy.Anim.transform.position.z);

        Enemy.Anim.transform.position = PlayerManager.instance.player.transform.position + new Vector3(3 * Enemy.FacingDir, 0, 0);
    }


    private void EndTeleportToPlayer()
    {
        Enemy.Anim.transform.position = defaultPosition;
    }

    private void BurstFireTrigger()
    {
        var playerTransform = PlayerManager.instance.player.transform;
        Vector3 initialPosition = playerTransform.position;
        var burstFire = Instantiate(Enemy.projectilePrefab, new(initialPosition.x, -2.37f, 0), Quaternion.identity);
        burstFire.GetComponent<ChargeBeamController>().Setup(Enemy.Stats);
    }

}
