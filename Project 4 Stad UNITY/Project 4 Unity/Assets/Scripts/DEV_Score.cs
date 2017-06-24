using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DEV_Score : MonoBehaviour {


    public int score;
    public Text scoreText;
	
	void Update () {

        scoreText.text = score.ToString();

        }
	}

