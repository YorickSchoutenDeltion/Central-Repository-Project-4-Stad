using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_CharacterController : MonoBehaviour {

    public float hor;
    public float ver;
    private float horCam;
    private float verCam;
    public float moveSpeed;
    public float turnSpeed;

    private Vector3 playerMove;
    private Vector3 playerRotate;
    private Vector3 axisRotate;
    private Vector3 towardsPlane;

    public GameObject playerCam;
    public GameObject playerObj;
    public GameObject camAxis;
    public GameObject rayPlane;
    public GameObject human;

    public Animator anim;

    private RaycastHit camRay;
    private RaycastHit talkRay;

    public DEV_PlayerSounds playerSounds;
    public DEV_CameraEffects dangerZone;
    public DEV_CameraEffects dangerZoneTwo;
    public DEV_ConstructionTypeSceneOne constructionCutsceneOne;
    public DEV_ConstructionTypeSceneThree sceneThree;
    public DEV_HumanTypeAI humanType;
    public DEV_GUI guiOptions;
    public DEV_BookCase bookcase;
    public DEV_GateOpening gate;

    public bool crouching;
    public bool isInCrouchingZone;
    public bool recovering;
    public bool gotKeyCard = false;
    public bool forcedFirstPerson = false;
    public bool forcedThirdPerson = false;
    public bool useLight = false;
    public bool forcedFlashLightFalse = false;
    public bool isNearBookCase;
    public bool pushedBookCase;
    public bool canActivateGate = false;
    public bool hasActivatedGate;
    public bool fallAnimTrigger = false;
    public bool finalCutscene = false;
    public bool theEnd = false;

    public Camera camOne;
    public Camera camTwo;

    public Light flashLight;

    public Text endText;

    public AudioSource bookCase;
    public AudioSource gateAudio;
    public AudioSource keyCardHolder;
    public AudioSource flashLightToggle;
    public AudioSource audio;
    public AudioSource humanTypeAudio;
    public AudioSource humanTypeWalkingAudio;

    public Animator animator;

    public Vector3 lastPosition;

    public GameObject finalPos;

    public enum WeaponState {Melee, Ranged}
    public WeaponState myState;

    void Awake()
    {
        audio = this.GetComponent<AudioSource>();
        constructionCutsceneOne = GameObject.Find("ConstructionTypeSpawnOne").GetComponent<DEV_ConstructionTypeSceneOne>();
        humanType = human.GetComponent<DEV_HumanTypeAI>();
    }

    public void OnTriggerStay(Collider col) {
        if (col.transform.tag == "Crouching") {
            isInCrouchingZone = true;
        }

        if (col.transform.tag == "CutSceneFinal" && theEnd == false) {
            finalCutscene = true;
        }

        if (col.transform.tag == "KeyCardHolder" && gotKeyCard == true) {
            canActivateGate = true;
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.transform.tag == "Enemy") {
            dangerZone.isInDanger = true;
            dangerZone.blurScreen = true;
            dangerZoneTwo.isInDanger = true;
            dangerZoneTwo.blurScreen = true;
        }

        if (col.transform.tag == "BookShelf") {
            isNearBookCase = true;
        }


        if (col.transform.tag == "CutSceneTriggerOne") {
            constructionCutsceneOne.cutSceneActivated = true;
        }
    }

    private void OnTriggerExit(Collider col) {
        if (col.transform.tag == "Enemy") {
            dangerZone.isInDanger = false;
            dangerZone.blurScreen = false;
            dangerZoneTwo.isInDanger = false;
            dangerZoneTwo.blurScreen = false;

        }

        if(col.transform.tag == "KeyCardHolder") {
            canActivateGate = false;
        }

        if(col.transform.tag == "BookShelf") {
            isNearBookCase = false;
        }

        if(col.transform.tag == "Crouching") {
            isInCrouchingZone = false;
        }
    }


    public void Update() {

        if(theEnd == true) {
            endText.enabled = true;
            forcedThirdPerson = true;
            transform.position = new Vector3(1000, 1000, 1000);
        }


        if(finalCutscene == true) {
            ver = 0;
        }

        if(finalCutscene == true && fallAnimTrigger == false) {
            lastPosition = finalPos.transform.position;
        } else if(finalCutscene == true && fallAnimTrigger == true) {
            lastPosition = new Vector3(finalPos.transform.position.x, -0.88f, finalPos.transform.position.z);
        }

        if(finalCutscene == true) {
            forcedFlashLightFalse = true;
            forcedFirstPerson = true;
            dangerZone.isInDanger = true;
            dangerZoneTwo.isInDanger = true;
            constructionCutsceneOne.canWalk = false;
            transform.position = lastPosition;

            
        }

        if (Input.GetButtonDown("Fire3") && isNearBookCase == true) {
            bookcase.moveToGoal = true;
            if (pushedBookCase == false) {
                bookCase.Play();
                pushedBookCase = true;
            }
        }

            if(Input.GetButtonDown("Fire3") && canActivateGate == true && hasActivatedGate == false) {
                gate.gateOpening = true;
                keyCardHolder.Play();
                gateAudio.Play();
                hasActivatedGate = true;
            }

        if (forcedFlashLightFalse == true) {
            useLight = false;
        }

        if(Input.GetButtonDown("Fire2") && forcedFlashLightFalse == false) {
            if(useLight == true) {
                useLight = false;
                flashLightToggle.Play();
            } else if(useLight == false && forcedFlashLightFalse == false) {
                useLight = true;
                flashLightToggle.Play();
            }
        }

        if(useLight == true) {
            flashLight.intensity = 2.5f;
        } else if(useLight == false) {
            flashLight.intensity = 0;
        }

        if(forcedFirstPerson == true) {
            camOne.enabled = false;
            camTwo.enabled = true;
        }

        if (forcedThirdPerson == true) {
            camTwo.enabled = false;
            camOne.enabled = true;
        }

        if (Input.GetButtonDown("Fire1") && forcedFirstPerson == false && forcedThirdPerson == false) {
            if (camOne.enabled == true) {
                camOne.enabled = false;
                camTwo.enabled = true;
            } else if(camTwo.enabled == true) {
                camTwo.enabled = false;
                camOne.enabled = true;
            }
        }

        if(recovering == true) {
            guiOptions.imageAlpha = Mathf.Lerp(guiOptions.imageAlpha, 0, Time.deltaTime / 2);
        }

        if (dangerZone.isInDanger == true) {
            audio.volume = Mathf.Lerp(audio.volume, 1, 2 * Time.deltaTime);
        } else if (dangerZone.isInDanger == false) {
            audio.volume = Mathf.Lerp(audio.volume, 0, 2 * Time.deltaTime);
        }

        if (crouching == false && isInCrouchingZone == true) {
            humanType.seesPlayer = true;
        }

        if(isInCrouchingZone == true && useLight == true) {
            humanType.seesPlayer = true;
        }

        anim.SetFloat("var", ver);
        anim.SetBool("Crouching", crouching);
        anim.SetBool("fallanimtrigger", fallAnimTrigger);

        if (Input.GetButtonDown("Jump")) {
            if (crouching == true && constructionCutsceneOne.canWalk == true) {
                crouching = false;
            } else if (crouching == false && constructionCutsceneOne.canWalk == true) {
                crouching = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (finalCutscene == false && theEnd == false) {
            if (constructionCutsceneOne.canWalk == true) {
                PlayerMove();
            }

            MoveVars();
            PlayerRotate();
            CamClip();
        }
    }

    // This sets the various movement variables to their appropriate values
    public void MoveVars()
    {
        if (constructionCutsceneOne.canWalk == true)
        {
            ver = Input.GetAxis("Vertical");
            if(crouching == false) {
                playerMove.z = ver;
            } else {
                playerMove.x = hor/2;
                playerMove.z = ver/2;
            }
        }
        horCam = Input.GetAxis("Mouse X");
        verCam = Input.GetAxis("Mouse Y");
        playerRotate.y = horCam;
        axisRotate.x = -verCam;
    }

    // This causes the player object to move when input is given
    void PlayerMove()
    {
        transform.Translate(playerMove * moveSpeed * Time.deltaTime);
    }

    // This controls the rotation of the player and camera
    void PlayerRotate()
    {
        playerObj.transform.Rotate(playerRotate * turnSpeed * Time.deltaTime);
        camAxis.transform.Rotate(axisRotate * turnSpeed * Time.deltaTime);
    }

    public void OnCollisionEnter(Collision c) {
        if(c.transform.tag == "ConstructionTypePseudo") {
            Destroy(c.gameObject);
            guiOptions.imageAlpha = 1;
            dangerZone.isInDanger = false;
            dangerZoneTwo.isInDanger = false;
            StartCoroutine("Recover");
            humanTypeAudio.Play();
            forcedFirstPerson = false;
            forcedThirdPerson = true;
            humanTypeWalkingAudio.volume = 0;
        }

        if(c.transform.tag == "Enemy") {
            camOne.enabled = true;
            camTwo.enabled = false;
        }

        if(c.transform.tag == "ConstructionFinal") {
            fallAnimTrigger = true;
            ver = 0;
        }

        if(c.transform.tag == "KeyCard") {
            forcedFirstPerson = true;
            sceneThree.cutSceneActivated = true;
            Destroy(c.gameObject);
            gotKeyCard = true;
            
        }
    }

    IEnumerator Recover() {
        yield return new WaitForSeconds(5);
        recovering = true;
        StartCoroutine("RecoverEnd");
    }

    IEnumerator RecoverEnd() {
        yield return new WaitForSeconds(10);
        recovering = false;
        constructionCutsceneOne.canWalk = true;
        forcedThirdPerson = false;
        forcedFlashLightFalse = false;
        humanTypeWalkingAudio.volume = 1;
        
    }

    // This ensures the camera doesn't clip into any colliders
    void CamClip()
    {
        towardsPlane = (rayPlane.transform.position - camAxis.transform.position);
        Debug.DrawRay(camAxis.transform.position, towardsPlane * 10000, Color.green);
        if(Physics.Raycast(camAxis.transform.position, towardsPlane, out camRay, Mathf.Infinity))
        {
            playerCam.transform.position = camRay.point;
        } else 
        {
            playerCam.transform.position = rayPlane.transform.position;
        }
    }
}
