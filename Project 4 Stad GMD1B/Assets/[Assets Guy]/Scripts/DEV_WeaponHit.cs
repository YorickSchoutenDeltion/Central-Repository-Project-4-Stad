using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_WeaponHit : MonoBehaviour {

    public DEV_SoundManager soundManager;
    public GameObject player;

    public AudioSource audio;

    public void Awake() {
        soundManager = player.GetComponent<DEV_SoundManager>();
    }

    public void OnCollisionEnter(Collision c) {
        if (c.gameObject.transform.tag != "Player") {
            audio.Play();
        }
    }
}
