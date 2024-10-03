using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject toggle;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    private void Toggle()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        toggle.SetActive(!toggle.activeSelf);
    }
}
