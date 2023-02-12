using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimation : MonoBehaviour
{
    
    public float offset = 0.1f;
    public float intervalRate = 0.2f;
    public bool invert = false;

    private Vector3 starting_position;

    private void Start() {
        this.starting_position = transform.position;
    }

    void Update() {
        Vector3 pos = this.transform.position;
        if (invert) {
            pos.y = Mathf.PingPong(Time.time * intervalRate + 0.5f,  offset) + starting_position.y;
        }
        else {
            pos.y = Mathf.PingPong(Time.time * intervalRate,  offset) + starting_position.y;
        }
        this.transform.position = pos;
    }
}
