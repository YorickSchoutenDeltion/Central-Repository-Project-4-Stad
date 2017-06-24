using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_PlayerMovement : MonoBehaviour {

    public float playerSpeed = 50;
    public new AudioSource audio;
    public AudioClip audioWalking;

    void FixedUpdate() {

        audio.clip = audioWalking;

        if (Input.GetKey("up")) {
            transform.Translate(0, 0, playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey("down")) {
            transform.Translate(0, 0, -playerSpeed * Time.deltaTime);
        }

        if (Input.GetKey("left")) {
            transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey("right")) {
            transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKeyDown("up") || Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) {
            if(!audio.isPlaying) {
                audio.Play();
            } else {

            }
        }

        if (Input.GetKeyUp("right") || Input.GetKeyUp("down") || Input.GetKeyUp("left") || Input.GetKeyUp("up")) {
            audio.Stop();
        }
    }
}
