using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
    public GameObject pause;
    public PlayerMovement player;

    private bool isPause = false;
    void Start() {
        pause.SetActive(false);
        this.isPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            this.alternatePause();
        }
    }

    public void alternatePause() {
        if (!isPause) {
            pause.SetActive(true);
            this.isPause = true;
            player.setPaused(true);
        } else {
            pause.SetActive(false);
            this.isPause = false;
            player.setPaused(false);
        }
    }

    public void exitGame() {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        foreach (GameObject masterObject in masterObjects) {
            masterObject.GetComponent<MasterManager>().resetValues();
        }
        SceneManager.LoadScene("Title Screen");
    }
}
