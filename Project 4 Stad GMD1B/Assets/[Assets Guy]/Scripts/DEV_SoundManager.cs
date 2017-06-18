using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_SoundManager : MonoBehaviour {

    public DEV_PlayerSounds playerSounds;

    public void Awake() {
        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponent<DEV_PlayerSounds>();
    }

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            playerSounds.Swinging();
        }
    }

    public void WeaponHit() {
        playerSounds.WeaponObjectHit();
    }
}
