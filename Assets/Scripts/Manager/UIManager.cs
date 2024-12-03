using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public UI_RuneSelect runeSelect;
    public UI_RuneToolTip runeTooltip;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        runeTooltip.gameObject.SetActive(false);
    }

}