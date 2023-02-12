using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceSpawner : MonoBehaviour {
    public GameObject policePrefab;
    public float spawnTime = 0.3f;

    void Start() {
        StartCoroutine(spawnWave());
    }
    
    private void spawnPolice() {
        GameObject a = Instantiate(policePrefab) as GameObject;
        a.transform.position = this.gameObject.transform.position;
        a.transform.rotation = new Quaternion(Random.Range(0.0f, 200.0f), Random.Range(0.0f, 200.0f), Random.Range(0.0f, 200.0f), Random.Range(0.0f, 200.0f));
    }
    
    IEnumerator spawnWave() {
        while (true) {
            yield return new WaitForSeconds(spawnTime);
            spawnPolice();
        }
    }
    
}
