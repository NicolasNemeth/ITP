using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform player;
    [SerializeField] private Transform goal;

    private int currentLevelIndex = 0;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        InitializeLevel(levels[currentLevelIndex]);
    }

    private void InitializeLevel(Level level)
    {
        ChangeSkybox(level.skybox);
        player.position = level.playerPos;
        goal.position = level.goalPos;
        mainCamera.transform.position = new Vector3(player.position.x, player.position.y, player.position.z - 20f);
    }

    private void ChangeSkybox(Material material)
    {
        RenderSettings.skybox = material;
        DynamicGI.UpdateEnvironment();
    }
}
