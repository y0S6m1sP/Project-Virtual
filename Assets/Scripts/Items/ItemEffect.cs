using UnityEngine;

public enum EffectType
{
    Offensive,
    Defensive,
    Utility,
}

public class ItemEffect : ScriptableObject
{
    public EffectType effectType;

    [TextArea]
    public string effectDescription;

    public virtual void ExecuteEffect(EntityStats target)
    {
        Debug.Log("Effect executed!");
    }
}
