using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Flee : MonoBehaviour
{
    [SerializeField] private Slider sacrificialSlider;
    [SerializeField] private BattleController controller;
    [SerializeField] private TextMeshProUGUI minionsNumUI;
    private MasterManager master;
    private int minions;
    private int difficulty;

    private void Awake()
    {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        foreach (GameObject masterObject in masterObjects)
        {
            master = masterObject.GetComponent<MasterManager>();
            minions = master.minions;
            difficulty = 1 + master.deadPolice.Count / 5;
        }
    }

    private void Update()
    {
        minionsNumUI.text = ((int)(minions * sacrificialSlider.value)).ToString();
    }

    public void Sacrifice()
    {
        minions = (int)(minions * sacrificialSlider.value);
        master.minions -= minions;
        if (master.minions <= 0) { SceneManager.LoadScene("Lost Screen"); }

        if (minions < (3 * difficulty) + Random.Range(-3, 4))
        {
            controller.Initialise();
        }
        else
        {
            controller.EndFightButNoDie();
        }
    }
}
