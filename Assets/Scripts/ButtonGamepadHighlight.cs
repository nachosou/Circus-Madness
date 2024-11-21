using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonGamepadHighlight : MonoBehaviour, IPointerEnterHandler, IDeselectHandler, IPointerExitHandler
{
    public void OnDeselect(BaseEventData eventData)
    {
        this.GetComponent<Selectable>().OnPointerExit(null);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting) 
        { 
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
            if (!EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(null);
    }
}
