﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	private int round = 0;
	private string dataFileName = "data.json";
	private float score;
	// public float Score { get; set; }
	private RoundData[] allRoundData;
	private Score scoreInstance;
	private int[] highScores;
	

	public void finishRound(){
		round++;
		scoreInstance.requestTopFiveScores();
	}

	public void updateHighScoreList(int[] hsList) {
		highScores = hsList;
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

		LoadGameData();

		SceneManager.LoadScene("MenuScreen");

		scoreInstance = GetComponent<Score>();
	}

	public RoundData getCurrentRoundData(int round){
		return allRoundData[round];
	}

	private void LoadGameData(){
		string filePath = Path.Combine(Application.streamingAssetsPath, dataFileName);

		if(File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);

			GameData data = JsonUtility.FromJson<GameData>(jsonData);

			allRoundData = data.allRoundData;
		}
		else {
			Debug.LogError("Seems like you are missing some data :/ :?");
		}
	}

	public int[] getHighScores() {
		return highScores;
	}
}
