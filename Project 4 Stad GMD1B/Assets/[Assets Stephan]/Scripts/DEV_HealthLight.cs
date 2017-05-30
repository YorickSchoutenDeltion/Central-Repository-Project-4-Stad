using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEV_HealthLight : MonoBehaviour {

    public DEV_ResourceManager resource;
    public GameObject gameManager;

    public Light armorLight;
    public Light ltOptions;

    public List<Color> lightColors = new List<Color>();
    public List<int> intensityList = new List<int>();
    public int i;

    public enum State {
        HighHealth,
        MediumHealth,
        LowHealth,
        Dead
    }

    public State state;

    private void Awake()    {

        resource.health = 100;
        resource = gameManager.GetComponent<DEV_ResourceManager>();
        ltOptions = armorLight.GetComponent<Light>();
   
    }

	void Update () {

        //if (Input.GetButtonDown("Jump"))
        //{
            HealthCheck();
        //}

    }

    void HealthCheck()
    {
        if (resource.health <= 100 && resource.health > 75)
        {
            state = State.HighHealth;
        }

        if (resource.health <= 75 && resource.health > 50)
        {
            state = State.MediumHealth;
        }

        if (resource.health <= 50 && resource.health > 25)
        {
            state = State.LowHealth;
        }
        if (resource.health <= 25 && resource.health > 0)
        {
            state = State.Dead;
        }


        

        if (state == State.HighHealth)
        {
            i = 0;
            ltOptions.color = lightColors[i];
            ltOptions.intensity = intensityList[i];
        }

        if (state == State.MediumHealth)
        {
            i = 1;
            ltOptions.color = lightColors[i];
            ltOptions.intensity = intensityList[i];
        }
        if (state == State.LowHealth)
        {
            i = 2;
            ltOptions.color = lightColors[i];
            ltOptions.intensity = intensityList[i];
        }
        if (state == State.Dead)
        {
            i = 3;
            ltOptions.color = lightColors[i];
            ltOptions.intensity = intensityList[i];
        }


    }

}
