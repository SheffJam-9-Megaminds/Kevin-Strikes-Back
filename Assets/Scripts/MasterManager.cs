using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MasterManager : MonoBehaviour {
	public List<string> deadPolice = new List<string>();
	public List<string> recruitedMinions = new List<string>();
	public int minions = 10;
	private static MasterManager s_Instance = null;
 
	void Awake()
	{
		if (s_Instance == null)
		{
			s_Instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void resetValues() {
		deadPolice = new List<string>();
		recruitedMinions = new List<string>();
		minions = 10;
	}

	public void addPolice(GameObject police) {
		deadPolice.Add(police.name);
	}

	public void addMinion(GameObject minion) {
		recruitedMinions.Add(minion.name);
	}

	public void hidePolice() {
		GameObject[] foundPolice = GameObject.FindGameObjectsWithTag("Police");
		GameObject[] foundMinions = GameObject.FindGameObjectsWithTag("Minion");
		GameObject[] foundFire = GameObject.FindGameObjectsWithTag("Fire");
		foreach (string policeName in deadPolice) {
			foreach (GameObject police in foundPolice) {
				if (policeName == police.name) {
					police.SetActive(false);
				}
			}
			
		}
		foreach (string minionName in recruitedMinions) {
			foreach (GameObject minion in foundMinions) {
				if (minionName == minion.name) {
					minion.SetActive(false);
				}
			}
		}
		int disabledFires = foundFire.Length - deadPolice.Count;
		foreach (GameObject fire in foundFire) {
			if (disabledFires > 0) {
				fire.SetActive(false);
				disabledFires--;
			}
		}
	}
	
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name == "First World") {
			hidePolice();
		} else if (scene.name == "Title Screen") {
			resetValues();
		}
	}
}
