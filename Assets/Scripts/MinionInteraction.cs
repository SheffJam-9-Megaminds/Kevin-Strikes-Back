using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionInteraction : MonoBehaviour
{
	[SerializeField] private GameObject textBubble;
	[SerializeField] private string[] prompts;

	public void initializeTextPrompt() {
		GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
		foreach (GameObject masterObject in masterObjects) {
			int randomMinionIncrease = Random.Range(10, 20);
			masterObject.GetComponent<MasterManager>().minions += randomMinionIncrease;
		}
		textBubble.SetActive(true);
		textBubble.GetComponent<TextPrompter>().startTextPrompting(this.prompts, true);
	}
}
