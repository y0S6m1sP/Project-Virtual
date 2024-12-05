using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public UI_RuneSelect runeSelect;
    public UI_RuneToolTip runeTooltip;
    public UI_PathMap pathMap;

    private bool isMapRender = false;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance.gameObject);
        else
        {
            instance = this;
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pathMap.parent.gameObject.SetActive(!pathMap.parent.gameObject.activeSelf);
        }
    }

    public void ShowMap(bool isShow)
    {
        if (pathMap.parent != null)
        {
            pathMap.parent.gameObject.SetActive(isShow);
        }

        if (!isMapRender)
        {
            pathMap.RenderMap();
            isMapRender = true;
        }
    }

}