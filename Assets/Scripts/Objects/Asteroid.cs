using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Object
{
    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound("Asteroid");
            Player.Instance.StopMoving();
            Player.Instance.TakeDamage();
            LevelManager.Instance.ReloadLevel();
        }
    }
}
