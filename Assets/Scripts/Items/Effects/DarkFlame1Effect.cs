using UnityEngine;

[CreateAssetMenu(fileName = "DarkFlame1", menuName = "Data/Item Effect/DarkFlame1")]
public class DarkFlame1Effect : ItemEffect
{
    [SerializeField] private GameObject darkFlamePrefab;

    public override void ExecuteEffect(EntityStats target)
    {
        var darkFlame = Instantiate(darkFlamePrefab, target.transform.position, Quaternion.identity);
        target.TakePhysicalDamage(PlayerManager.instance.player.Stats);
        Destroy(darkFlame, 1f);
    }
}