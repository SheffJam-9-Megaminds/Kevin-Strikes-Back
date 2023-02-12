using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class welcomeHandler : MonoBehaviour {
    public string[] welcomes;
    public TextPrompter text;
    public GameObject canvasUI;

    void Start() {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        if (masterObjects[0].GetComponent<MasterManager>().deadPolice.Count == 0) {
            text.startTextPrompting(welcomes, true);
        }
    }
}
