using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPolice : MonoBehaviour
{
	public float killTime = 7f;

	void Start() {
		StartCoroutine(spawnWave());
	}
    
	private void killSelf() {
		Destroy(this.gameObject);
	}
    
	IEnumerator spawnWave() {
		while (true) {
			yield return new WaitForSeconds(killTime);
			killSelf();
		}
	}
}
