using System.Collections;
using UnityEngine;

public class ShockwaveController : MonoBehaviour
{
    [SerializeField] private float maxScaleSize = 5f;
    [SerializeField] private float duration = 1f;

    private Material material;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        material = renderer.material;

        StartCoroutine(AnimateScaleAndFade());
    }

    private IEnumerator AnimateScaleAndFade()
    {
        float elapsedTime = 0f;
        Vector3 startingScale = transform.localScale;
        Vector3 targetScale = new(maxScaleSize, maxScaleSize, maxScaleSize);

        Color startingColor = material.color;
        Color targetColor = new(startingColor.r, startingColor.g, startingColor.b, 0f);

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(startingScale, targetScale, elapsedTime / duration);

            material.color = Color.Lerp(startingColor, targetColor, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        material.color = targetColor;

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            EntityStats enemyStats = enemy.Stats;
            if (enemyStats != null && !enemyStats.IsDead)
            {
                // knockback currently not working because enemy attack state set the velocity to zero
                Player player = PlayerManager.instance.player;
                enemy.SetupKnockbackDir(player.transform);
                enemy.SetupKnockbackPower(player.Stats.knockbackPower);
                enemy.Knockback();

                enemyStats.TakePhysicalDamage(player.Stats);
            }
        }
    }
}