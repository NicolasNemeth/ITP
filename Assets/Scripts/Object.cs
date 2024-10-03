using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Object : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coordsText;

    private float textDisappearCooldown = 1f;
    private float lastMouseEnterTime;

    private void Start()
    {
        UpdateCoordsText(transform.position);
    }

    private void OnMouseEnter()
    {
        lastMouseEnterTime = Time.time;
        EnableCoordsText();
    }

    private void OnMouseExit()
    {
        Invoke("DisableCoordsText", textDisappearCooldown);
    }

    protected void UpdateCoordsText(Vector3 pos) => coordsText.text = pos.x + "\n" + pos.y + "\n" + pos.z;

    private void EnableCoordsText() => coordsText.enabled = true;

    private void DisableCoordsText()
    {
        if (Time.time - lastMouseEnterTime < textDisappearCooldown)
            return;

        coordsText.enabled = false;
    }
}
