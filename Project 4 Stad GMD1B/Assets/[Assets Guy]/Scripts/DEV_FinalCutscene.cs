using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_FinalCutscene : MonoBehaviour {

    public bool cutSceneActivated = false;
    public bool hasSpawned;
    public bool canStomp = false;
    public bool reachedFinalDestination = false;

    public GameObject constructionTypeSpawner;
    public GameObject constructionType;
    private GameObject cType;
    public float spawnSpeedMultiplier;
    public DEV_ConstructionTypeSceneOne firstScene;
    public GameObject human;
    public GameObject player;
    public DEV_HumanTypeAI humanSettings;
    public DEV_CharacterController controller;

    public Vector3 transformGoal;

    public Animator constrType;

    public void Awake() {
        humanSettings = human.GetComponent<DEV_HumanTypeAI>();
        controller = player.GetComponent<DEV_CharacterController>();
    }

    void Update() {
        constrType.SetBool("canstomp", canStomp);
        if(controller.finalCutscene == true) {
            cutSceneActivated = true;
        }

        if (cutSceneActivated == true && hasSpawned == false) {
            Debug.Log("Attempting to instantiate object");
            cType = (GameObject)Instantiate(constructionType, new Vector3(constructionTypeSpawner.transform.position.x, 0, constructionTypeSpawner.transform.position.z), constructionType.transform.rotation);
            hasSpawned = true;
        }

        if (cType != null) {
            if (reachedFinalDestination == false) {
                Vector3 transformGoal = Vector3.MoveTowards(cType.transform.position, player.transform.position, Time.deltaTime * spawnSpeedMultiplier);
                cType.transform.position = transformGoal;
            }

            if (cType.transform.position == transformGoal) {
                canStomp = true;
                reachedFinalDestination = true;
                humanSettings.canStartWalking = false;
            }
        }
    }
}
