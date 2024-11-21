using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SwordManager : MonoBehaviour
{
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Transform swordParent;
    [SerializeField] private UI_SwordSlot swordSlots1;
    [SerializeField] private UI_SwordSlot swordSlots2;
    [SerializeField] private UI_SwordSlot swordSlots3;
    [SerializeField] private UI_SwordSlot swordSlots4;
    [SerializeField] private UI_SwordSlot swordSlots5;

    public static SwordManager Instance;

    public List<ItemDataSword> startingSwords = new();
    public Dictionary<UI_SwordSlot, ItemDataSword> swordSlotDict = new();
    public Dictionary<UI_SwordSlot, DragDrop> dragDropDict = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        InitSwordSlot();

        foreach (ItemDataSword sword in startingSwords)
        {
            AddSword(sword);
        }
    }

    private void InitSwordSlot()
    {
        swordSlotDict.Add(swordSlots1, null);
        swordSlotDict.Add(swordSlots2, null);
        swordSlotDict.Add(swordSlots3, null);
        swordSlotDict.Add(swordSlots4, null);
        swordSlotDict.Add(swordSlots5, null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (var slot in swordSlotDict)
            {
                string swordName = slot.Value != null ? slot.Value.name : "Empty";
                Debug.Log($"Slot: {slot.Key.name}, Sword: {swordName}");
            }

            AddSwordEffect(ScriptableObject.CreateInstance<DoubleSwordEffect>());
        }
    }

    public void AddSword(ItemDataSword _sword)
    {
        UI_SwordSlot currentSlot = null;

        foreach (var slot in swordSlotDict)
        {
            if (slot.Value == null)
            {
                swordSlotDict[slot.Key] = _sword;
                currentSlot = slot.Key;
                break;
            }
        }

        if (currentSlot == null)
        {
            Debug.Log("No empty slot available");
            return;
        }

        GameObject sword = Instantiate(swordPrefab, currentSlot.transform.position, Quaternion.identity, swordParent);
        sword.GetComponent<Image>().sprite = _sword.itemIcon;

        if (sword.TryGetComponent<DragDrop>(out var dragDrop))
        {
            dragDropDict.Add(currentSlot, dragDrop);
            dragDrop.CurrentSlot = currentSlot;
        }
    }

    public void BlocksAllRaycasts(bool isBlock)
    {
        foreach (CanvasGroup group in swordParent.GetComponentsInChildren<CanvasGroup>())
        {
            group.blocksRaycasts = isBlock;
        }
    }

    public void GenerateSword(Transform player, EntityStats enemy)
    {
        foreach (var slot in swordSlotDict)
        {
            if (slot.Value != null)
            {
                var isDouble = false;
                foreach (ItemEffect effect in slot.Value.itemEffects)
                {
                    if (effect is DoubleSwordEffect)
                    {
                        isDouble = true;
                    }
                }

                if (isDouble)
                {
                    GameObject doubleSword = Instantiate(slot.Value.swordPrefab, player.position, Quaternion.identity);
                    doubleSword.GetComponent<SwordController>().Setup(enemy);
                }

                GameObject sword = Instantiate(slot.Value.swordPrefab, player.position, Quaternion.identity);
                sword.GetComponent<SwordController>().Setup(enemy);
            }
        }
    }

    public List<ItemEffect> GetItemEffects(EffectType type)
    {
        List<ItemEffect> effects = new();

        foreach (ItemDataSword sword in swordSlotDict.Values)
        {
            foreach (ItemEffect effect in sword.itemEffects)
            {
                if (effect.effectType == type)
                {
                    effects.Add(effect);
                }
            }
        }

        return effects;
    }

    public void AddSwordEffect(ItemEffect effect)
    {
        swordSlotDict[swordSlots1].AddEffect(effect);
    }
}