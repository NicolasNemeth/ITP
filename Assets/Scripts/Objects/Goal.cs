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
            AudioManager.Instance.PlaySound("Goal");
            Player.Instance.StopMoving();
            LevelManager.Instance.LoadNextLevel();
        }
    }
}
