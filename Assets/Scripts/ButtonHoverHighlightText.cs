using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverHighlightText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color hightlightColor;

    private Color baseColor;

    private void Awake()
    {
        baseColor = text.color;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = hightlightColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = baseColor;
    }
}
