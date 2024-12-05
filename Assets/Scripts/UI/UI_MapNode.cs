using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MapNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image icon;

    private MapNode node;

    public void OnPointerDown(PointerEventData eventData)
    {
        GameLevelManager.Instance.SetCurrentNode(node);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }

    public void SetNode(MapNode _node)
    {
        node = _node;

        switch (node.Type)
        {
            case NodeType.Money:
                icon.color = Color.yellow;
                break;
            case NodeType.Stats:
                icon.color = Color.green;
                break;
            case NodeType.Rune:
                icon.color = Color.blue;
                break;
            case NodeType.Shop:
                icon.color = Color.magenta;
                break;
            case NodeType.Boss:
                icon.color = Color.red;
                break;
            case NodeType.Empty:
                icon.color = new Color(0, 0, 0, 0); // Set color to transparent
                break;
        }
    }

    public void ResetNode()
    {
        node = null;
        icon.color = Color.black;
    }


}