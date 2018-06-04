using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	private int round = 0;
	private float score;
	// public float Score { get; set; }
	public RoundData[] allRoundData;
	

	public void finishRound(){
		round++;
	}

	public int getCurrentRound(){
		if (round > allRoundData.Length)
			round = -1;
		return round;
	}

	public void addScore(float newScore){
		score += newScore;
	}

	public float getTotalScore(){
		return score;
	}

	void Start () {
		DontDestroyOnLoad(gameObject);

		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData getCurrentRoundData(int round){
		return allRoundData[round];
	}

	void Update () {
		
	}
}
