using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private Animator levelCompletedTextAnimator;
    [SerializeField] private GameObject gameCompletedToggle;
    [SerializeField] private GameObject gameOverToggle;

    [Space]

    [SerializeField] private Button levelInfoButton;
    [SerializeField] private GameObject levelInfoToggle;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI levelDescriptionText;

    [Space]

    [SerializeField] private Player player;
    [SerializeField] private Goal goal;
    [SerializeField] private GameObject asteroidPrefab;

    [Space]

    [SerializeField] private Button playerZoomButton;
    [SerializeField] private Button goalZoomButton;

    private int maxLevelIndex = 8;
    private int currentLevelIndex;
    private Camera mainCamera;
    private List<GameObject> asteroids;

    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        asteroids = new List<GameObject>();

        playerZoomButton.onClick.AddListener(() => ZoomInOnPosition(player.transform.position, true));
        goalZoomButton.onClick.AddListener(() => ZoomInOnPosition(goal.transform.position, true));
        levelInfoButton.onClick.AddListener(ToggleLevelInfo);

        Begin();
    }

    public void Begin()
    {
        currentLevelIndex = -1;
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

        DestroyAsteroids();
        CreateAsteroids();

        UpdateLevelInfo();
        InitializeLevel(levels[currentLevelIndex]);
    }

    public void ReloadLevel() => InitializeLevel(levels[currentLevelIndex]);

    public void HideUI()
    {
        gameCompletedToggle.SetActive(false);
        gameOverToggle.SetActive(false);
        levelInfoToggle.SetActive(false);
    }

    public void ShowGameOverScreen() => gameOverToggle.SetActive(true);

    private void InitializeLevel(Level level)
    {
        player.Teleport(level.playerPos);
        goal.Teleport(level.goalPos);
        ZoomInOnPosition(level.playerPos, false);

        InputManager.Instance.UpdatePlayerVectorText(level.playerPos);
        InputManager.Instance.SetInputType(level.vectorAddition);

        ChangeSkybox(level.skybox);
    }

    private void CreateAsteroids()
    {
        foreach (Vector3 pos in levels[currentLevelIndex].asteroidPositions)
        {
            GameObject asteroid = GameObject.Instantiate(asteroidPrefab, pos, Quaternion.identity);
            asteroids.Add(asteroid);
        }
    }

    private void DestroyAsteroids()
    {
        for (int i = 0; i < asteroids.Count; i++)
        {
            Destroy(asteroids[i]);
        }
        asteroids.Clear();
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
        {
            AudioManager.Instance.PlaySound("Button");
            ClearEventSystemSelectedButton();
        }
    } 

    private void ToggleLevelInfo()
    {
        AudioManager.Instance.PlaySound("Button");
        levelInfoToggle.SetActive(!levelInfoToggle.activeSelf);
        ClearEventSystemSelectedButton();
    }

    private void UpdateLevelInfo()
    {
        levelText.text = "Level " + (currentLevelIndex + 1);
        levelDescriptionText.text = levels[currentLevelIndex].description;
    }

    private void ShowEndScreen() => gameCompletedToggle.SetActive(true);

    private void ClearEventSystemSelectedButton() => EventSystem.current.SetSelectedGameObject(null);
}
