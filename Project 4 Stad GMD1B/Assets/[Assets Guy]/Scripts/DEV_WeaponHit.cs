using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_WeaponHit : MonoBehaviour {

    public DEV_SoundManager soundManager;
    public GameObject player;
    public DEV_PlayerSounds playerSounds;

    public AudioSource audio;



    public void Awake() {
        soundManager = player.GetComponent<DEV_SoundManager>();
        playerSounds = player.GetComponent<DEV_PlayerSounds>();
    }

    public void hasHit() {
        audio.Play();
    }
}
