using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PoliceManager : MonoBehaviour {
	[SerializeField] private GameObject textBubble;
	[SerializeField] private string[] prompts;

	public void initializeTextPrompt() {
		textBubble.SetActive(true);
		textBubble.GetComponent<TextPrompter>().startTextPrompting(this.prompts);
	}
}
