using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_ConstructionTypeFeet : MonoBehaviour {

    public AudioSource audio;
    public AudioClip stomping;

    public void Awake() {
        audio = this.GetComponent<AudioSource>();

    }


    public void OnTriggerEnter(Collider other) {
        if(other.transform.tag == "Asphalt") {
            audio.clip = stomping;
            audio.Play();
        }
    }

    public void OnTriggerExit(Collider other) {
        audio.Stop();
    }
}
