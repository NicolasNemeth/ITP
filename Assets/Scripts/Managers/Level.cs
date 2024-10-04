using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public Material skybox;
    public Vector3 playerPos;
    public Vector3 goalPos;
    [TextArea(3, 10)] public string description;
    public bool vectorAddition;
    public Vector3[] asteroidPositions;
}
