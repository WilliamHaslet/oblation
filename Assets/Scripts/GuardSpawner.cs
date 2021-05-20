using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSpawner : MonoBehaviour {
    
    public Vector3[] spawnLocation;

    public IEnumerator SpawnGuard(GameObject guard)
    {
        yield return new WaitForSeconds(45);
        guard.transform.position = spawnLocation[guard.GetComponent<GuardController>().index];
        guard.SetActive(true);
    }

    public void SpawnThis(GameObject guard)
    {
        StartCoroutine(SpawnGuard(guard));
    }
}