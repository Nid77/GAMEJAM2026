using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PopupButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PopupSpawn.Instance.OnAnswerSelected(transform.GetSiblingIndex()+transform.parent.GetSiblingIndex()*2);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {   
        //Debug.Log("Hovering over button " + transform.GetSiblingIndex());
        PopupSpawn.Instance.OnAnswerHovered(transform.GetSiblingIndex()+transform.parent.GetSiblingIndex()*2);
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
//Debug.Log("Stopped hovering over button " + transform.GetSiblingIndex());
        PopupSpawn.Instance.OnAnswerHovered(-1);
    }
}
