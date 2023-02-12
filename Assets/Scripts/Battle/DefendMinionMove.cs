using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefendMinionMove : MonoBehaviour
{
    private int direction;
    private float baseSpeed = 50;
    private float speed;

    [SerializeField] private float killingSpeed = 1;
    [SerializeField] private GameObject killEffect;
    [SerializeField] private Transform effectPos;
    [SerializeField] private MasterManager master;
    public float Timer { get; set; }
    private int minionsDead;
    private float difficulty;


    private void Start()
    {
        GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
        foreach (GameObject masterObject in masterObjects)
        {
            master = masterObject.GetComponent<MasterManager>();
            difficulty = 1 + master.deadPolice.Count /10;
        }
        baseSpeed *= difficulty;
        killingSpeed /= difficulty;
    }

    void OnEnable()
    {
        transform.localPosition = new(15, Random.Range(-70, 70), 0);
        direction = Random.Range(0, 1) * 2 - 1;
        speed = baseSpeed;
        Timer = 0;
        minionsDead = 0;
    }

    void Update()
    {
        if (Timer / killingSpeed - minionsDead >= 1)
        {
            KillMinion();
        }

        float newPosY = transform.localPosition.y + direction * speed * Time.deltaTime;
        if (newPosY > 70 || newPosY < -70)
        {
            ChangeDirection();
        }
        else
        {
            transform.localPosition = new(15, newPosY, 0);
            if (Random.Range(0, 1000) < 3) { ChangeDirection(); }
        }
    }

    private void ChangeDirection()
    {
        direction *= -1;
        speed = baseSpeed * Random.Range(7, 15) / 10;
    }

    private void KillMinion()
    {
        master.minions--;
        if (master.minions <= 0) { SceneManager.LoadScene("Lost Screen"); }
        minionsDead++;
        Instantiate(killEffect, effectPos);
    }
}
