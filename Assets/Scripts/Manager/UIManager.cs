using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField] private GameObject swordPanel;

    public UI_SwordToolTip swordTooltip;
    public UI_RelicToolTip relicTooltip;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start() {
        swordTooltip.gameObject.SetActive(false);
        relicTooltip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            swordPanel.SetActive(!swordPanel.activeSelf);
        }
    }

}