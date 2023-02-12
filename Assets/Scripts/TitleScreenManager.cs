using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour {

    public void quitGame() {
        Application.Quit();
    }

    public void playGame() {
        SceneManager.LoadScene("First World");  
    }

    public void exitToMenu() {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        foreach (GameObject masterObject in masterObjects) {
            masterObject.GetComponent<MasterManager>().resetValues();
        }
        SceneManager.LoadScene("Title Screen");
    }
}
