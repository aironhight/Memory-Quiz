using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public int currentScore;
	private DBManager db;


	// Use this for initialization
	void Start () {
		currentScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void postScore() {

	}

	public void requestTopFiveScores() {
		db.getTopFiveScores();
	}

	public void inflateScoreBoard(int[] topScores) {
		//this method gets called by the DBManager, sending an array with the top 5 scores.
		
	}

	public void addScore (int score) {
		currentScore += score;
	}
}
