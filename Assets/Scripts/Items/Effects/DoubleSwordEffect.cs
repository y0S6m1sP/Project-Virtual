using UnityEngine;

[CreateAssetMenu(fileName = "Double sword", menuName = "Data/Item effect/Double sword")]
public class DoubleSwordEffect : ItemEffect
{
    public override void ExecuteEffect(EntityStats target)
    {
        Debug.Log("Double sword effect executed!");
    }
}