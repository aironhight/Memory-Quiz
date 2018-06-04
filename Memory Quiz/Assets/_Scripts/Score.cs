using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public int currentScore;
	[SerializeField]public DBManager db;
	bool _topScores = true; // for testing


	// Use this for initialization
	void Start () {
		currentScore = 0;
		db = GetComponent<DBManager>();
		requestTopFiveScores(); // testing purposes.
	}

	private void Update() {
		//for testing purposes
		if(_topScores) {
			requestTopFiveScores();
		}
		
	}

	//Starts an async task, posting the score to the database.
	public void postScore() {
		db.postScore(currentScore);
	}

	//Requests the top 5 scores from the database. The method 'inflateScoreBoard' gets called if the request is successful.
	public void requestTopFiveScores() {
		db.getTopFiveScores();
	}

	//this method gets called automatically by the DBManager, sending a sorted array with the top 5 scores (decreasing value - index 0 is the top score)
	public void inflateScoreBoard(int[] topScores) {
		//begining of testing lines
		_topScores = false;
		
		foreach(int i in topScores) {
			Debug.Log("Kurami #" + (i+1) + " e " + i + " santimetra.");
		}
		//end of testing lines
	}

	//Adds score to the current score.
	public void addScore (int score) {
		currentScore += score;
	}
}
