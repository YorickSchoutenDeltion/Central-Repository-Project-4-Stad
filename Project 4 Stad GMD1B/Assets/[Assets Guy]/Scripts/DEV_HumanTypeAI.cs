using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_HumanTypeAI : MonoBehaviour {

    public bool seesPlayer = false;
    public bool reachedTransform = false;
    public bool canStartWalking = false;
    public bool spottedHasPlayed = false;

    public Transform goal;
    public Transform player;
    public Transform playerSpawn;

    public DEV_CameraEffects cameraSettings;
    public DEV_CharacterController playerController;
    public DEV_ConstructionTypeSceneOne walkingOptions;
    public DEV_GUI guiOptions;

    public GameObject camera;
    public GameObject constructionTypeSpawnOne;
    public GameObject loader;

    public AudioSource audio;
    public AudioSource humanWalking;
    public AudioSource rain;
    public AudioSource spotted;

    public Animator anim;

    public GameObject blackScreen;

    public Vector3 goalRandomizedLocation;
    public int secondsToRandomizedLocationGoal;

    private Vector3 humanRotation;
    private Vector3 humanRotationToPlayer;

    public float xGoalLoc;
    public float zGoalLoc;
    public float respawnTimer;

    public void Awake() {
        cameraSettings = camera.GetComponent<DEV_CameraEffects>();
        walkingOptions = constructionTypeSpawnOne.GetComponent<DEV_ConstructionTypeSceneOne>();
        playerController = player.GetComponent<DEV_CharacterController>();

        guiOptions = loader.GetComponent<DEV_GUI>();
        anim = this.GetComponent<Animator>();
    }

    public void Start() {
        StartCoroutine("RandomizeGoal");
    }

    void Update () {

        anim.SetBool("reachedtransform", reachedTransform);
        anim.SetBool("seesplayer", seesPlayer);

        if (transform.position == goal.position) {
            reachedTransform = true;
        }

        if(reachedTransform == true) {
            humanWalking.volume = 0;
        } else if(canStartWalking == true) {
            humanWalking.volume = 1;
        }

        goalRandomizedLocation = new Vector3(xGoalLoc, 0, zGoalLoc / 2);
        if (seesPlayer == false) {
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.LookRotation(goalRandomizedLocation - transform.position)), Time.deltaTime * 5);
        }

        if (seesPlayer == true) {
            humanRotation = new Vector3(0, goal.position.y, 0);
            humanRotationToPlayer = new Vector3(player.position.x, player.position.y, player.position.z);
        }

        if (seesPlayer == false && canStartWalking == true) {
            transform.position = Vector3.MoveTowards(transform.position, goal.position, Time.deltaTime * 2);

        } else if (seesPlayer == true) {
            transform.rotation = Quaternion.Slerp(transform.rotation, (Quaternion.LookRotation(player.transform.position - transform.position)), Time.deltaTime * 5);
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 8);
        if(spottedHasPlayed == false) {
                spotted.Play();
                spottedHasPlayed = true;
            }
            }
        }

    IEnumerator RandomizeGoal() {
        yield return new WaitForSeconds(secondsToRandomizedLocationGoal);
        reachedTransform = false;
        xGoalLoc = Random.Range(-13.5f, 11);
        zGoalLoc = Random.Range(9.93f, 16f);
        goal.position = goalRandomizedLocation;
        StartCoroutine("RandomizeGoal");
    }

    private void OnCollisionEnter(Collision c) {
        if(c.transform.tag == "Player") {
            player.transform.position = new Vector3(playerSpawn.transform.position.x, 0, playerSpawn.transform.position.z);
            playerController.isInCrouchingZone = false;
            seesPlayer = false;
            spottedHasPlayed = false;
            reachedTransform = false;
            cameraSettings.isInDanger = false;
            walkingOptions.canWalk = false;
            player.GetComponent<AudioSource>().volume = 0;
            rain.volume = 0;
            guiOptions.imageAlpha = 1;
            playerController.forcedThirdPerson = true;
            audio.Play();
            StartCoroutine("CanMoveAgain");
        }
    }

    IEnumerator CanMoveAgain() {
        yield return new WaitForSeconds(respawnTimer);
        guiOptions.imageAlpha = 0;
        playerController.forcedThirdPerson = false;
        walkingOptions.canWalk = true;
        rain.volume = 1;
    }
}

