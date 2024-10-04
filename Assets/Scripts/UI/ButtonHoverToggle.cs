using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverToggle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject toggle; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        toggle.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toggle.SetActive(false);
    }
}
