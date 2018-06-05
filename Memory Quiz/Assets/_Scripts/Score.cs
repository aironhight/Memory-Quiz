using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {
	private DBManager db;
	private GameController gameController;


	// Use this for initialization
	void Start () {
		db = GetComponent<DBManager>();
		gameController = GetComponent<GameController>();
		requestTopFiveScores(); // testing purposes.
	}

	private void Update() {
		
	}

	//Starts an async task, posting the score to the database.
	public void postScore(int currentScore) {
		db.postScore(currentScore);
	}

	//Requests the top 5 scores from the database. The method 'inflateScoreBoard' gets called if the request is successful.
	public void requestTopFiveScores() {
		db.getTopFiveScores();
	}

	//this method gets called automatically by the DBManager, sending a sorted array with the top 5 scores (decreasing value - index 0 is the top score)
	public void inflateScoreBoard(int[] topScores) {
		gameController.InflateScoreBoard(topScores);
	}
}
