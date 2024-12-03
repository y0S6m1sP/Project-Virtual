using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_MapNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;

    private MapNode node;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Node Type: " + node.Type);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
    }

    public void SetNode(MapNode _node)
    {
        node = _node;

        if (node.Type == NodeType.Empty)
        {
            icon.color = new Color(0, 0, 0, 0); // Set color to transparent
        }
    }

    
}