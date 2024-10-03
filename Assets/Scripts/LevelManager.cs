using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Level> levels;
    [SerializeField] private Transform player;
    [SerializeField] private Transform goal;

    private void Awake()
    {
        InitializeLevel(levels[0]);
    }

    private void InitializeLevel(Level level)
    {
        ChangeSkybox(level.skybox);
        player.position = level.playerPos;
        goal.position = level.goalPos;
    }

    private void ChangeSkybox(Material material)
    {
        RenderSettings.skybox = material;
        DynamicGI.UpdateEnvironment();
    }
}
