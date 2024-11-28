using UnityEngine;

class GlitchSamuraiAnimationTirgger : EnemyAnimationTriggers
{

    private Vector3 defaultPosition;

    private void StartTeleportToPlayer()
    {
        defaultPosition = new Vector3(Enemy.Anim.transform.position.x, Enemy.Anim.transform.position.y, Enemy.Anim.transform.position.z);

        Enemy.Anim.transform.position = PlayerManager.instance.player.transform.position + new Vector3(3 * Enemy.FacingDir, 0, 0);
    }

    private void EndTeleportToPlayer()
    {
        Enemy.Anim.transform.position = defaultPosition;
    }
}