using System.Collections;
using UnityEngine;
using UnityStandardAssets.CinematicEffects;

public class DEV_CameraEffects : MonoBehaviour {

    /// Made by: Stephan & Guy
    /// Reference to used assets: Legacy Cinematic Image Effects
    /// Used scripts: Motion Blur, Lens Abberrations.

    public int randomBlurCheck;
    public int blurHitCount;

    public bool isInDanger;
    public bool blurScreen;

    public LensAberrations cameraEffects;
    public MotionBlur cameraMovementBlur;

    public GameObject cam;

    //References

    private void Awake()    {
        cameraEffects = cam.GetComponent<LensAberrations>();
        cameraMovementBlur = cam.GetComponent<MotionBlur>();
    }

    public void Start() {
        cameraEffects.vignette.center.x = 0.5f;
        cameraEffects.vignette.center.x = 0.5f;
        cameraEffects.vignette.smoothness = 0.63f;
    }

    //checks per frame if the int is the equivalent. If that's the case, it activates a blur effect.
    void Update () {
        randomBlurCheck = Random.Range(0, 50000);
        if(randomBlurCheck == 1)    {
            blurScreen = true;
            blurHitCount += 1;
            StartCoroutine("BlurActivation");
        }

        if(blurScreen == true)  {
        cameraEffects.vignette.blur = Mathf.Lerp(cameraEffects.vignette.blur, 1, 2 * Time.deltaTime);
        }

        if (blurScreen == false)    {
            cameraEffects.vignette.blur = Mathf.Lerp(cameraEffects.vignette.blur, 0, 2 * Time.deltaTime);
        }

        if (isInDanger == true) {
            cameraEffects.vignette.intensity = Mathf.Lerp(cameraEffects.vignette.intensity, 1.73f, 4 * Time.deltaTime);
            cameraEffects.vignette.desaturate = Mathf.Lerp(cameraEffects.vignette.desaturate, 1, 2 * Time.deltaTime);
            cameraMovementBlur.settings.frameBlending = Mathf.Lerp(cameraMovementBlur.settings.frameBlending, 1, 2 * Time.deltaTime);
        }

         if (isInDanger == false)   {
            cameraEffects.vignette.intensity = Mathf.Lerp(cameraEffects.vignette.intensity, 0f, 4 * Time.deltaTime);
            cameraEffects.vignette.desaturate = Mathf.Lerp(cameraEffects.vignette.desaturate, 0, 2 * Time.deltaTime);
            cameraMovementBlur.settings.frameBlending = Mathf.Lerp(cameraMovementBlur.settings.frameBlending, 0, 2 * Time.deltaTime);
        }
	}

    IEnumerator BlurActivation()    {
        yield return new WaitForSeconds(3);
        blurScreen = false;
    }
}
