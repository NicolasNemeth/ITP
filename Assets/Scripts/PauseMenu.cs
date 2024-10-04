using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject toggle;
    [SerializeField] private Button toggleButton;
    [SerializeField] private Sprite pauseSprite;
    [SerializeField] private Sprite returnSprite;

    private void Awake()
    {
        toggleButton.onClick.AddListener(Toggle);
    }

    private void Update()
    {
        if (StartScreen.Instance.HasGameStarted && Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    private void Toggle()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        toggle.SetActive(!toggle.activeSelf);
        UpdateButtonImage();
    }

    private void UpdateButtonImage()
    {
        toggleButton.image.sprite = Time.timeScale == 0 ? returnSprite : pauseSprite;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
