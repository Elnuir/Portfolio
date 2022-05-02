using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class MoveHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler// required interface when using the OnPointerEnter method.
{
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite changeToSprite;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = changeToSprite;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = baseSprite;
    }
}