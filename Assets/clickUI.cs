using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class clickUI : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private string[] prompts;
    [SerializeField] private string[] completeWinPrompts;
    [SerializeField] private BattleController battleUI;
    public float clickTime = 3f;

    private IEnumerator timeStamp;

    public GameObject backGround;
    public GameObject prompt;
    public TextMeshProUGUI promptText;

    public int clicks = 0;
    private float countdown;
    
    void OnEnable() {
        countdown = clickTime;
        timeStamp = clickTimeCountdown();
        StartCoroutine(timeStamp);
    }

    void reduceTimer() {
        countdownText.SetText("" + countdown);
    }

    void displayEndScreen() {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        int requiredClicks = ((masterObjects[0].GetComponent<MasterManager>().deadPolice.Count-1) * 10) + 10;
        if (clicks >= requiredClicks) {
            backGround.SetActive(false);
            prompt.SetActive(true);
            prompt.GetComponent<TextPrompter>().startTextPrompting(this.prompts);
        } else {
            battleUI.Initialise();
        }
    }

    public void userClick() {
        if (countdown > 0) {
            clicks++;
        }
    }

    IEnumerator clickTimeCountdown() {
        while (true) {
            yield return new WaitForSeconds(1f);
            countdown--;
            if (countdown <= 0) {
                StopCoroutine(timeStamp);
                GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
                if (masterObjects[0].GetComponent<MasterManager>().deadPolice.Count < 9) {
                    countdownText.SetText("" + clickTime);
                    displayEndScreen();
                }
                else {
                    int requiredClicks = (masterObjects[0].GetComponent<MasterManager>().deadPolice.Count * 10) + 10;
                    if (clicks >= requiredClicks) {
                        countdownText.SetText("" + clickTime);
                        backGround.SetActive(false);
                        prompt.SetActive(true);
                        prompt.GetComponent<TextPrompter>()
                            .startTextPrompting(this.completeWinPrompts, false, true, false);
                    }
                    else {
                        countdownText.SetText("" + clickTime);
                        battleUI.Initialise();
                    }
                }
            }
            else {
                reduceTimer();
            }
        }
    }
}
