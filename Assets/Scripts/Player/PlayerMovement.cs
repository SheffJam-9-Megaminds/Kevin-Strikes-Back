using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private int moveSpeed;
    private Rigidbody rigidComp;
    private readonly float DiagonalMoveMult = Mathf.Sqrt(2) / 2;
    private readonly float rotation = -Mathf.PI/4;

    [SerializeField] private GameObject _characterModel;
    private Rigidbody modelRigid;
    private Animator _animator;

    private bool isPaused = false;
    private AudioSource footsteps;


    private void Start()
    {
        rigidComp = GetComponent<Rigidbody>();
        modelRigid = _characterModel.GetComponent<Rigidbody>();
        _animator = _characterModel.GetComponent<Animator>();
        isPaused = false;
        footsteps = GetComponent<AudioSource>();
        footsteps.enabled = false;
    }

    public void setPaused(bool pause) {
        this.isPaused = pause;
    }

    private void Update()
    {
        // Movement
        if (!isPaused) {
            float realX = Input.GetAxisRaw("Horizontal") * moveSpeed;
            float realZ = Input.GetAxisRaw("Vertical") * moveSpeed;
            if (realX != 0 && realZ != 0) {
                realX *= DiagonalMoveMult;
                realZ *= DiagonalMoveMult;
            }

            float moveX = realX * Mathf.Cos(rotation) - realZ * Mathf.Sin(rotation);
            float moveZ = realX * Mathf.Sin(rotation) + realZ * Mathf.Cos(rotation);
            Vector3 moveVal = new(moveX, rigidComp.velocity.y, moveZ);
            rigidComp.velocity = moveVal;

            //Rotation
            if (moveVal != Vector3.zero) {
                Vector3 rotationDirection = moveVal;
                rotationDirection.y = 0f;
                _characterModel.transform.rotation = Quaternion.LookRotation(rotationDirection);
            }

            //Animation
            if (moveX == 0f && moveZ == 0f) {
                _animator.SetBool("Is_Running", false);
                _animator.SetBool("Lock_State", true);
                footsteps.enabled = false;
            }
            else {
                _animator.SetBool("Is_Running", true);
                footsteps.enabled = true;
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Police")) {
            this.isPaused = true;
            Vector3 moveVal = new(0f, rigidComp.velocity.y, 0f);
            rigidComp.velocity = moveVal;
            GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
            foreach (GameObject masterObject in masterObjects) {
                masterObject.GetComponent<MasterManager>().addPolice(other.gameObject);
            }
            other.GetComponent<PoliceManager>().initializeTextPrompt();
        } else if (other.CompareTag("Minion")) {
            this.isPaused = true;
            Vector3 moveVal = new(0f, rigidComp.velocity.y, 0f);
            rigidComp.velocity = moveVal;
            GameObject[] masterObjects = GameObject.FindGameObjectsWithTag("Master");
            foreach (GameObject masterObject in masterObjects) {
                masterObject.GetComponent<MasterManager>().minions++;
                masterObject.GetComponent<MasterManager>().addMinion(other.gameObject);
            }
            other.GetComponent<MinionInteraction>().initializeTextPrompt();
        }
    }
}
