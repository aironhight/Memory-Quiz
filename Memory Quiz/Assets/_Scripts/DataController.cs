using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
//using System.IO;

public class DataController : MonoBehaviour {

	private int round = 0;
	private float score;
	// public float Score { get; set; }
	private RoundData[] allRoundData;
	public Score scoreInstance;
	private int[] highScores;
	private int allRounds;
	private int num;
	
	void Awake(){
		
	}

	void Start () {
		scoreInstance = GetComponent<Score>();
		// Make the game object persist in the background
		DontDestroyOnLoad(gameObject);
		
		// Just to be sure :D
		ResetRounds();

		// No longer loading data from here - keep as reminder
		// LoadOldGameData();

		StartCoroutine(Load());
	}

	IEnumerator Load()
    {
        // suspend execution for N seconds
        yield return new WaitForSeconds(3);
		SceneManager.LoadScene("MenuScreen");
    }

	public void finishRound(){
		round++;

		// requestiong topFive here because database result comes after 4-5 secs delay
		scoreInstance.requestTopFiveScores();
	}

	public void updateHighScoreList(int[] hsList) {
		highScores = hsList;
	}

	public int getCurrentRound(){
		if (round >= allRoundData.Length)
		{
			// when we reach the last round -1 is being send to the GameController so it know when to show the Game Over panel
			round = -1;
		}
		return round;
	}

	public void addScore(float newScore){
		score += newScore;
	}

	public float getTotalScore(){
		return score;
	}

	

	public RoundData getCurrentRoundData(int round){
		Debug.Log("curr data: " + allRoundData.Length);
		return allRoundData[round];
	}

	public void LoadGameData(RoundData[] data)
	{
		this.allRoundData = data;
	}


	// Method no longer being used - keep for reminder
	// private void LoadOldGameData(){
		/* // For windows use the following:
		string filePath = Path.Combine(Application.streamingAssetsPath, dataFileName);
		if(File.Exists(filePath))
		{
			string jsonData = File.ReadAllText(filePath);

			GameData data = JsonUtility.FromJson<GameData>(jsonData);

			allRoundData = data.allRoundData;
		}
		else {
			Debug.LogError("Seems like you are missing some data :/ :?");
		}*/
        

		// For Andorid use the following code!
		// Meant to load 1 random json file from 3 current
		// int num = (int) Random.Range(1,4);

		// string path = Application.streamingAssetsPath + "/data" + num + ".json";
        // WWW www = new WWW(path);
        // while(!www.isDone) {}
        // string json = www.text;

		// // Transform json to Game Data
        // GameData data = JsonUtility.FromJson<GameData> (json); 

		// // Set the current round data to the one loaded from json
        // allRoundData = data.allRoundData;

		//New solution - Load data on start
	// }

	public int[] getHighScores() {
		return highScores;
	}

	public void UploadScoreInstance(){
		// Used to upload the score to Firebase, called from GameController
		scoreInstance.postScore((int)score);
	}

	public void ResetRounds(){
		round = 0;
		score = 0;
	}
}
