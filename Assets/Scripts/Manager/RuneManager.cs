using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public static RuneManager Instance;

    public List<ItemDataRune> startingRunes = new();
    public List<ItemDataRune> runes = new();
    [SerializeField] private Transform runeParent;
    private UI_RuneSlot[] runeSlots;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        runeSlots = runeParent.GetComponentsInChildren<UI_RuneSlot>();

        foreach (ItemDataRune rune in startingRunes)
        {
            AddRelic(rune);
        }
    }

    private void UpdateRelicSlots()
    {
        foreach (UI_RuneSlot slot in runeSlots)
        {
            slot.CleanUpSlot();
        }

        for (int i = 0; i < runes.Count; i++)
        {
            runeSlots[i].AddRune(runes[i]);
        }
    }

    public void AddRelic(ItemDataRune _relic)
    {
        runes.Add(_relic);
        _relic.AddModifiers();
        UpdateRelicSlots();
    }

    public void RemoveRelic(ItemDataRune _relic)
    {
        runes.Remove(_relic);
        _relic.RemoveModifiers();
        UpdateRelicSlots();
    }

    public List<ItemEffect> GetItemEffects(EffectType type)
    {
        List<ItemEffect> effects = new();

        foreach (ItemDataRune relic in runes)
        {
            foreach (ItemEffect effect in relic.itemEffects)
            {
                if (effect.effectType == type)
                {
                    effects.Add(effect);
                }
            }
        }

        return effects;
    }
}