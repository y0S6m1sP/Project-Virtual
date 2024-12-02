using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject swordPanel;

    public UI_SwordSelect swordSelect;
    public UI_SwordToolTip swordTooltip;
    public UI_RuneToolTip runeTooltip;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start() {
        swordTooltip.gameObject.SetActive(false);
        runeTooltip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            swordPanel.SetActive(!swordPanel.activeSelf);
        }
    }

}