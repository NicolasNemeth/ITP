using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameObject[] levelIcons;
    [SerializeField] private Animator levelCompletedTextAnimator;
    [SerializeField] private GameObject gameCompletedToggle;

    [Space]

    [SerializeField] private Button levelInfoButton;
    [SerializeField] private GameObject levelInfoToggle;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;

    [Space]

    [SerializeField] private Player player;
    [SerializeField] private Goal goal;

    [Space]

    [SerializeField] private Button playerZoomButton;
    [SerializeField] private Button goalZoomButton;

    private int maxLevelIndex = 8;
    private int currentLevelIndex = -1;
    private Camera mainCamera;

    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;

        playerZoomButton.onClick.AddListener(() => ZoomInOnPosition(player.transform.position, true));
        goalZoomButton.onClick.AddListener(() => ZoomInOnPosition(goal.transform.position, true));
        levelInfoButton.onClick.AddListener(ToggleLevelInfo);

        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex > maxLevelIndex || currentLevelIndex > levels.Count - 1)
        {
            ShowEndScreen();
            return;
        }

        if (currentLevelIndex != 0)
            levelCompletedTextAnimator.SetTrigger("Fade");

        InitializeLevel(levels[currentLevelIndex]);
    }

    private void InitializeLevel(Level level)
    {
        player.Teleport(level.playerPos);
        goal.Teleport(level.goalPos);
        ZoomInOnPosition(level.playerPos, false);
        InputManager.Instance.UpdatePlayerVectorText(level.playerPos);

        ChangeSkybox(level.skybox);
        levelIcons[currentLevelIndex].SetActive(true);
    }

    private void ChangeSkybox(Material material)
    {
        RenderSettings.skybox = material;
        DynamicGI.UpdateEnvironment();
    }

    private void ZoomInOnPosition(Vector3 pos, bool buttonInput)
    {
        mainCamera.transform.position = new Vector3(pos.x, pos.y, pos.z + 20f);
        mainCamera.transform.LookAt(pos);

        if (buttonInput)
            ClearEventSystemSelectedButton();
    } 

    private void ToggleLevelInfo()
    {
        levelText.text = "Level " + (currentLevelIndex + 1);
        levelDescriptionText.text = levels[currentLevelIndex].description;
        levelInfoToggle.SetActive(!levelInfoToggle.activeSelf);
        ClearEventSystemSelectedButton();
    }

    private void ShowEndScreen() => gameCompletedToggle.SetActive(true);

    private void ClearEventSystemSelectedButton() => EventSystem.current.SetSelectedGameObject(null);
}
