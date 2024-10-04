using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public Material skybox;
    public Vector3 playerPos;
    public Vector3 goalPos;
    public string description;
}
