using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_PlayerSounds : MonoBehaviour {

    public string playerTag;
    public string manager;

    public DEV_SoundManager sound;

    public List<AudioSource> audio = new List<AudioSource>();
    public List<AudioClip> playerAudioFiles = new List<AudioClip>();
    public List<GameObject> charParts = new List<GameObject>();

    public Animation handAnim;

    void Awake() {
        sound = GameObject.Find(manager).GetComponent<DEV_SoundManager>();
        audio[0] = charParts[0].GetComponent<AudioSource>();
        audio[1] = charParts[1].GetComponent<AudioSource>();
        audio[2] = charParts[2].GetComponent<AudioSource>();
    }

    public void Swinging() {
            handAnim.Play();
        if(handAnim.isActiveAndEnabled) {
            audio[2].Play();
        }
    }

    public void WeaponObjectHit() {
        audio[1].Play();
    }

    public void FootStep() {
        audio[2].Play();
    }
}
