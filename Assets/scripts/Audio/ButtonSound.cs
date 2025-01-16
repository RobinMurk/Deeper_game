using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    // Called when the mouse pointer enters the button
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!gameObject.activeInHierarchy) return;
        FindObjectOfType<AudioManager>().Play("ButtonHover");
    }

    // Called when the button is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().Play("ButtonClick");
    }
}
