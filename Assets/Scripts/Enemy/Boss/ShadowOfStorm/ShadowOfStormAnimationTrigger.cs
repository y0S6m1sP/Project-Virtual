using System.Collections;
using UnityEngine;

class ShadowOfStormAnimationTrigger : EnemyAnimationTriggers
{
    private ShadowOfStorm shadowOfStorm => GetComponentInParent<ShadowOfStorm>();

    private void ChargeBeamTrigger()
    {
        StartCoroutine(ChargeBeamPreStart());
    }

    private IEnumerator ChargeBeamPreStart()
    {
        var playerTransform = PlayerManager.instance.player.transform;
        Vector3 initialPosition = playerTransform.position;
        yield return new WaitForSeconds(.1f);
        var beam = Instantiate(shadowOfStorm.ChargeBeamPrefab, initialPosition, Quaternion.identity);
        beam.GetComponent<ChargeBeamController>().Setup(shadowOfStorm.Stats);
    }
}