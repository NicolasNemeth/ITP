using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private GameObject[] levelIcons;
    [SerializeField] private Animator levelCompletedTextAnimator;
    [SerializeField] private GameObject gameCompletedToggle;

    [Space]

    [SerializeField] private Player player;
    [SerializeField] private Goal goal;

    private int maxLevelIndex = 8;
    private int currentLevelIndex = 0;
    private Camera mainCamera;

    public static LevelManager Instance;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
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
        mainCamera.transform.position = new Vector3(level.playerPos.x, level.playerPos.y, level.playerPos.z + 20f);
        InputManager.Instance.UpdatePlayerVectorText(level.playerPos);

        ChangeSkybox(level.skybox);
        levelIcons[currentLevelIndex].SetActive(true);
        currentLevelIndex++;
    }

    private void ChangeSkybox(Material material)
    {
        RenderSettings.skybox = material;
        DynamicGI.UpdateEnvironment();
    }

    private void ShowEndScreen() => gameCompletedToggle.SetActive(true);
}
