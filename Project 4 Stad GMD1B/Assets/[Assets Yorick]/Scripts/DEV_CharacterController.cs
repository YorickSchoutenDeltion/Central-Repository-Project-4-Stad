﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_CharacterController : MonoBehaviour {

    private float hor;
    private float ver;
    private float horCam;
    private float verCam;
    private float attackTimerDefault;
    public float moveSpeed;
    public float turnSpeed;
    public float attackTimer;

    private Vector3 playerMove;
    private Vector3 playerRotate;
    private Vector3 axisRotate;
    private Vector3 towardsPlane;

    public GameObject playerCam;
    public GameObject playerObj;
    public GameObject camAxis;
    public GameObject rayPlane;
    public GameObject weapon;

    private RaycastHit camRay;
    private RaycastHit talkRay;

    public DEV_PlayerSounds playerSounds;
    public DEV_CameraEffects dangerZone;

    public AudioSource audio;

    public Animator animator;

    public enum WeaponState {Melee, Ranged}
    public WeaponState myState;

    void Start()
    {
        attackTimerDefault = attackTimer;
        audio = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider col) {
        if(col.transform.tag == "Enemy") {
            dangerZone.isInDanger = true;
            dangerZone.blurScreen = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.transform.tag == "Enemy") {
            dangerZone.isInDanger = false;
            dangerZone.blurScreen = false;
        }
    }

    public void Update() {
        if(dangerZone.isInDanger == true) {
            audio.volume = Mathf.Lerp(audio.volume, 1, 2 * Time.deltaTime);
        } else if(dangerZone.isInDanger == false) {
            audio.volume = Mathf.Lerp(audio.volume, 0, 2 * Time.deltaTime);
        }
    }


    void FixedUpdate()
    {

        MoveVars();
        PlayerMove();
        PlayerRotate();
        CamClip();
        FightMelee();
    }

    // This sets the various movement variables to their appropriate values
    void MoveVars()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        horCam = Input.GetAxis("Mouse X");
        verCam = Input.GetAxis("Mouse Y");
        playerMove.x = hor;
        playerMove.z = ver;
        playerRotate.y = horCam;
        axisRotate.x = -verCam;
    }

    // This causes the player object to move when input is given
    void PlayerMove()
    {
        transform.Translate(playerMove * moveSpeed * Time.deltaTime);
    }

    // This controls the rotation of the player and camera
    void PlayerRotate()
    {
        playerObj.transform.Rotate(playerRotate * turnSpeed * Time.deltaTime);
        camAxis.transform.Rotate(axisRotate * turnSpeed * Time.deltaTime);
    }

    // This ensures the camera doesn't clip into any colliders
    void CamClip()
    {
        towardsPlane = (rayPlane.transform.position - camAxis.transform.position);
        Debug.DrawRay(camAxis.transform.position, towardsPlane * 10000, Color.green);
        if(Physics.Raycast(camAxis.transform.position, towardsPlane, out camRay, Mathf.Infinity))
        {
            playerCam.transform.position = camRay.point;
        }
    }

    // This handles the melee combat
    void FightMelee()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            attackTimer = attackTimerDefault;
            animator.SetFloat("IsSwing", 1);
            weapon.GetComponent<DEV_WeaponDamage>().isSwinging = true;
            playerSounds.Swinging();
        }
        else
        {
            attackTimer -= Time.deltaTime;
            animator.SetFloat("IsSwing", 0);
            if(attackTimer < 0)
            {
                weapon.GetComponent<DEV_WeaponDamage>().isSwinging = false;
            }
        }
    }
}
