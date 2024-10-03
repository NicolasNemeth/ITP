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
            Debug.Log("Level completed!");
        }
    }
}
