using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TextPrompter : MonoBehaviour {
    public TextMeshProUGUI uiText;
    private string[] prompts;
    [SerializeField] private string sceneName;
	[SerializeField] private string alternateSceneName;
    private int index = 0;
    private int sIndex = 0;
    private bool isPrinting = false;
    private bool exit = false;
    private string currentString = "";
    private bool skipLevelLoading = false;
	private bool loadAlternateLevel = false;
	private bool lost = false;

    public void startTextPrompting(string[] prompt) {
        this.index = 0;
        this.sIndex = 0;
        this.isPrinting = true;
        this.exit = false;
        this.currentString = "";
        this.prompts = prompt;
        this.skipLevelLoading = false;
    }
    
    public void startTextPrompting(string[] prompt, bool skip) {
        this.index = 0;
        this.sIndex = 0;
        this.isPrinting = true;
        this.exit = false;
        this.currentString = "";
        this.prompts = prompt;
        this.skipLevelLoading = skip;
    }

	public void startTextPrompting(string[] prompt, bool skip, bool loadAlternate) {
        this.index = 0;
        this.sIndex = 0;
        this.isPrinting = true;
        this.exit = false;
        this.currentString = "";
        this.prompts = prompt;
        this.skipLevelLoading = skip;
		this.loadAlternateLevel = loadAlternate;
    }

	public void startTextPrompting(string[] prompt, bool skip, bool loadAlternate, bool lostInfo) {
        this.index = 0;
        this.sIndex = 0;
        this.isPrinting = true;
        this.exit = false;
        this.currentString = "";
        this.prompts = prompt;
        this.skipLevelLoading = skip;
		this.loadAlternateLevel = loadAlternate;
		this.lost = lostInfo;
    }

    void Update() {
        if (this.isPrinting) {
            this.currentString += this.prompts[index][sIndex];
            this.uiText.SetText(this.currentString);
            if (((this.sIndex+1) >= this.prompts[index].Length)) {
                this.isPrinting = false;
                this.index++;
                this.sIndex = 0;
                if (index >= this.prompts.Length) {
                    this.exit = true;
                }
            } else
            {
                this.sIndex++;
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                uiText.SetText("");
                this.currentString = "";
                this.isPrinting = true;
                if (this.exit && !skipLevelLoading && !loadAlternateLevel && !lost) {
                    SceneManager.LoadScene(this.sceneName);
                } else if (this.exit && skipLevelLoading) {
                    GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
                    foreach (GameObject playerObject in playerObjects) {
                        playerObject.GetComponent<PlayerMovement>().setPaused(false);
                    }
                    GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
                    foreach (GameObject masterObject in masterObjects) {
                        masterObject.GetComponent<MasterManager>().hidePolice();
                    }
                    this.exit = false;
                    this.gameObject.SetActive(false);
                }
				if (this.exit && loadAlternateLevel) {
					SceneManager.LoadScene(this.alternateSceneName);
				}
				if (this.exit && lost) {
					SceneManager.LoadScene("Lost Screen");
				}
            }
        }
    }
}
