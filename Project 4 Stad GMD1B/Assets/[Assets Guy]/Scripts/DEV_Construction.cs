using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_Construction : MonoBehaviour {

    public DEV_FinalCutscene final;
    public Vector3 lastPos = new Vector3(-27.981f, 0.29f, -32.883f);

    public Animator anim;
    public AudioSource humanType;
    public AudioSource humanWalking;
    public AudioSource playerAudio;
    public DEV_CharacterController controller;
    public DEV_GUI guiOptions;
    public DEV_CameraEffects camOne;
    public DEV_CameraEffects camTwo;
    public GameObject humanTypeObject;

    public bool stompCheck = false;

	void Start () {
        anim = this.GetComponent<Animator>();
        final = GameObject.FindGameObjectWithTag("CutSceneFinal").GetComponent<DEV_FinalCutscene>();
        guiOptions = GameObject.FindGameObjectWithTag("Loader").GetComponent<DEV_GUI>();
        humanType = GameObject.FindGameObjectWithTag("Enemy").GetComponent<AudioSource>();
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<DEV_CharacterController>();
        humanWalking = GameObject.FindGameObjectWithTag("HumanWalking").GetComponent<AudioSource>();
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        camOne = GameObject.FindGameObjectWithTag("CamOne").GetComponent<DEV_CameraEffects>();
        camTwo = GameObject.FindGameObjectWithTag("CamTwo").GetComponent<DEV_CameraEffects>();
        humanTypeObject = GameObject.FindGameObjectWithTag("Enemy");
    }

    public void Update() {
        anim.SetBool("canstomp", final.canStomp);
    }	

	void OnCollisionEnter(Collision c) {
        if(c.transform.tag == "Player") {
            if(stompCheck == false) {
                StartCoroutine("RecoverAnimation");
                stompCheck = true;
            }

            final.reachedFinalDestination = true;
            transform.position = lastPos;
        }
    }

    IEnumerator RecoverAnimation() {
        yield return new WaitForSeconds(0.5f);
        final.canStomp = true;
        StartCoroutine("Ending");
    }

    IEnumerator Ending() {
        yield return new WaitForSeconds(1.5f);
        controller.finalCutscene = false;
        controller.theEnd = true;
        humanType.Play();
        humanWalking.volume = 0;
        Destroy(gameObject);
        guiOptions.imageAlpha = 1f;
        camOne.isInDanger = false;
        camTwo.isInDanger = false;
        playerAudio.volume = 0;
        StartCoroutine("DeleteHuman");
    }

    IEnumerator DeleteHuman() {
        yield return new WaitForSeconds(1.5f);
        Destroy(humanType);
        StartCoroutine("EndGame");
    }

    IEnumerator EndGame() {
        yield return new WaitForSeconds(10);
        Application.Quit();
    }
}
