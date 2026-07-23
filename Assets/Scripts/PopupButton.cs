using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PopupButton : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PopupSpawn.Instance.OnAnswerSelected(transform.GetSiblingIndex());
    }
}
