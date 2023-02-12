using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Defend : MonoBehaviour
{
    [SerializeField] private GameObject nextPhaseUI;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform progressBar;
    [SerializeField] private float fastAsFuck;
    [SerializeField] private float acceleration;
    [SerializeField] private float barScale;
    [SerializeField] private float progressSpeeeed;
    [SerializeField] private float progressDepreciationRateComparedToCompetetiveMarketRates;
    [SerializeField] private DefendMinionMove moveyThing;
    private float progress;
    private bool murderTime;
    private Rigidbody2D controlBar;
    private Vector2 velocityAddermerater;


    private void Start()
    {
        transform.localScale = new(1, barScale, 1);
        controlBar = GetComponent<Rigidbody2D>();
        velocityAddermerater = new(0, acceleration);
    }

    private void OnEnable()
    {
        progress = 0;
        murderTime = true;
    }


    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space) && controlBar.velocity.y < fastAsFuck)
        {
            controlBar.velocity += velocityAddermerater;
        }
    }

    private void Update()
    {
        if (murderTime) { moveyThing.Timer += Time.deltaTime; }

        if (progress > 5)
        {
            progress -= progressDepreciationRateComparedToCompetetiveMarketRates * Time.deltaTime;
        }
        progressBar.localScale = new(1, progress / 100, 1);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        progress += progressSpeeeed * Time.deltaTime;
        if (progress >= 100) { GetComponentInParent<BattleController>().AttackPhase(); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        murderTime = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        murderTime = true;
    }
}
