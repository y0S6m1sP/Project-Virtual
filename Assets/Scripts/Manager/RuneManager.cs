using System.Collections.Generic;
using UnityEngine;

public class RuneManager : MonoBehaviour
{
    public static RuneManager Instance;

    public List<ItemDataRune> startingRunes = new();
    public List<ItemDataRune> runes = new();
    [SerializeField] private Transform runeParent;
    private UI_RuneSlot[] runeSlots;

    public List<ItemDataRune> runesPool = new();

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
            AddRune(rune);
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

    public void AddRune(ItemDataRune _relic)
    {
        runes.Add(_relic);
        _relic.AddModifiers();
        UpdateRelicSlots();
    }

    public void RemoveRune(ItemDataRune _relic)
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

    public List<ItemDataRune> GetRandomRunes(int count)
    {
        List<ItemDataRune> randomRunes = new();
        HashSet<int> usedIndices = new();
        List<ItemDataRune> poolCopy = new(runesPool);

        while (randomRunes.Count < count && usedIndices.Count < poolCopy.Count)
        {
            int randomIndex = Random.Range(0, poolCopy.Count);
            if (!usedIndices.Contains(randomIndex))
            {
                randomRunes.Add(poolCopy[randomIndex]);
                usedIndices.Add(randomIndex);
            }
        }

        return randomRunes;
    }
}