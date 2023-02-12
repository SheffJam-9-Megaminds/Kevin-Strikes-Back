using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueUI;
    [SerializeField] private GameObject defendUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject fleeUI;
    [SerializeField] private GameObject clickUI;
    [SerializeField] private TextMeshProUGUI minionsRemaining;
    private MasterManager master;

    private GameObject activeUI;

    private void Start()
    {
        Initialise();

        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        foreach (GameObject masterObject in masterObjects)
        {
            master = masterObject.GetComponent<MasterManager>();
        }
    }

    private void Update()
    {
        minionsRemaining.text = master.minions.ToString() + "x Minions";
    }

    public void Initialise()
    {
        activeUI = optionsUI;
        dialogueUI.SetActive(false);
        defendUI.SetActive(false); 
        optionsUI.SetActive(false);
        fleeUI.SetActive(false);
        activeUI.SetActive(true);
        clickUI.SetActive(false);
    }

    public void StartFight()
    {
        activeUI.SetActive(false);
        defendUI.SetActive(true);
        activeUI = defendUI;
    }

    public void AttackPhase()
    {
        activeUI.SetActive(false);
        clickUI.SetActive(true);
        activeUI = clickUI;
    }

    public void TryFlee()
    {
        activeUI.SetActive(false);
        fleeUI.SetActive(true);
        activeUI = fleeUI;
    }

    public void EndFightButNoDie()
    {
        SceneManager.LoadScene("First World");
    }
}
