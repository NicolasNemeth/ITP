using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    [SerializeField] private GameObject toggle;
    [SerializeField] private Button startButton;
    [SerializeField] private Button[] buttonsToEnable;
    [SerializeField] private TMP_InputField[] inputFieldsToEnable;

    [Space]

    [SerializeField] Button[] restartButtons;

    public bool HasGameStarted { get; private set; } = false;

    public static StartScreen Instance;

    private void Awake()
    {
        Instance = this;
        UpdateUI();

        startButton.onClick.AddListener(StartGame);

        foreach (Button button in restartButtons)
        {
            button.onClick.AddListener(RestartGame);
        }
    }

    private void RestartGame()
    {
        AudioManager.Instance.PlaySound("Button");

        toggle.SetActive(true);
        HasGameStarted = false;

        Player.Instance.Heal();
        LevelManager.Instance.HideUI();
        LevelManager.Instance.Begin();

        UpdateUI();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void StartGame()
    {
        AudioManager.Instance.PlaySound("Button");
        toggle.SetActive(false);
        HasGameStarted = true;
        UpdateUI();
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateUI()
    {
        foreach (Button btn in buttonsToEnable)
        {
            btn.interactable = HasGameStarted;
        }

        foreach (TMP_InputField inputField in inputFieldsToEnable)
        {
            inputField.interactable = HasGameStarted;
        }
    }
}
