using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public UI_RuneSelect runeSelect;
    public UI_RuneToolTip runeTooltip;
    public UI_PathMap pathMap;

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

    private void Start()
    {
        runeTooltip.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            pathMap.RenderMap();
        }
    }

}