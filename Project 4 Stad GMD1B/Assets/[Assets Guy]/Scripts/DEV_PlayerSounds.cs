using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_PlayerSounds : MonoBehaviour {

    public string playerTag;
    public string manager;

    public DEV_SoundManager sound;

    public List<AudioSource> audio = new List<AudioSource>();
    public List<GameObject> charParts = new List<GameObject>();


    void Awake() {
        sound = GameObject.Find(manager).GetComponent<DEV_SoundManager>();
        audio[0] = charParts[0].GetComponent<AudioSource>();
    }

    public void Swinging() {
            audio[0].Play();
    }

    public void WeaponObjectHit() {
        audio[1].Play();
    }
}
