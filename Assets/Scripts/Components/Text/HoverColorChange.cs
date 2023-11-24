using UnityEngine;
using UnityEngine.EventSystems; // Required for the event trigger
using TMPro; // Required for TextMeshPro elements

public class HoverColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // Assign this in the inspector
    public Color normalColor = Color.white; // Default color
    public Color hoverColor = Color.red; // Color on hover

    // When the cursor enters the button area
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = hoverColor;
    }

    // When the cursor exits the button area
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = normalColor;
    }
}