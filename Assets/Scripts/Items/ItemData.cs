using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;

public enum ItemType
{
    Relic
}

public class ItemData : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public string itemId;

    [Header("Item effects")]
    public float itemCooldown;
    public ItemEffect[] itemEffects;

    protected StringBuilder sb = new();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemId = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }

    public virtual void Effect(EntityStats target)
    {
        foreach (ItemEffect effect in itemEffects)
        {
            effect.ExecuteEffect(target);
        }
    }

    public void AddEffect(ItemEffect newEffect)
    {
        var effectsList = new List<ItemEffect>(itemEffects)
        {
            newEffect
        };

        itemEffects = effectsList.ToArray();
    }
}
