using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class displayMinionCount : MonoBehaviour {
    public TextMeshProUGUI text;
    private MasterManager master;
    void Start() {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        master = masterObjects[0].GetComponent<MasterManager>();
    }

    void Update() {
        text.SetText("Minions x " + master.minions);
    }
}
