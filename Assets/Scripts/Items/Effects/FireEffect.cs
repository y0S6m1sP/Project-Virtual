using UnityEngine;

[CreateAssetMenu(fileName = "Fire effect", menuName = "Data/Item effect/Fire")]
public class FireEffect : ItemEffect
{
    public override void ExecuteEffect(EntityStats target)
    {
        Debug.Log("Fire effect executed!");
        target.TakeDamage(5);
    }
}