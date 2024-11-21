using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStats : MonoBehaviour
{
    public string swordName;

    public Stat damage;

    public virtual void DoDamage(EntityStats _entityStats)
    {
        var relicEffects = RelicManager.Instance.GetItemEffects(EffectType.Offensive);

        foreach (ItemEffect effects in relicEffects)
        {
            effects.ExecuteEffect(_entityStats);
        }

        _entityStats.TakeDamage(damage.GetValue());
    }

}
