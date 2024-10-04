using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.CompareTag("Player"))
        {
            Player.Instance.StopMoving();
            AudioManager.Instance.PlaySound("Goal");
            LevelManager.Instance.LoadNextLevel();
        }
    }
}
