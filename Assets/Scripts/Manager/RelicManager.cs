using System.Collections.Generic;
using UnityEngine;

public class RelicManager : MonoBehaviour
{
    public static RelicManager Instance;

    public List<ItemDataRelic> startingRelics = new();
    public List<ItemDataRelic> relics = new();
    [SerializeField] private Transform relicParent;
    private UI_RelicSlot[] relicSlots;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        relicSlots = relicParent.GetComponentsInChildren<UI_RelicSlot>();

        foreach (ItemDataRelic relic in startingRelics)
        {
            AddRelic(relic);
        }
    }

    private void UpdateRelicSlots()
    {
        foreach (UI_RelicSlot slot in relicSlots)
        {
            slot.CleanUpSlot();
        }

        for (int i = 0; i < relics.Count; i++)
        {
            relicSlots[i].AddRelic(relics[i]);
        }
    }

    public void AddRelic(ItemDataRelic _relic)
    {
        relics.Add(_relic);
        _relic.AddModifiers();
        UpdateRelicSlots();
    }

    public void RemoveRelic(ItemDataRelic _relic)
    {
        relics.Remove(_relic);
        _relic.RemoveModifiers();
        UpdateRelicSlots();
    }
}