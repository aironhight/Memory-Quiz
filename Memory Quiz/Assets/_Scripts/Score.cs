using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public int currentScore;


	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void postScore() {

	}

	public Score[] getTopScores() {
		return null;
	}

	public void addScore (int score) {
		currentScore += score;
	}
}
