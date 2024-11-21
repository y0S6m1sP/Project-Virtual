using UnityEngine;

[CreateAssetMenu(fileName = "Addition Fire Damage", menuName = "Data/Relic Effect/Addition Fire Damage")]
public class FireEffect : ItemEffect
{
    public override void ExecuteEffect(EntityStats target)
    {
        target.TakeDamage(5);
    }
}