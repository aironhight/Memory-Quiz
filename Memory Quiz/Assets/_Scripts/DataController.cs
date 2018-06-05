using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO;

public class DataController : MonoBehaviour {

	private int round = 0;
	// private string dataFileName = "data1.json";
	private float score;
	// public float Score { get; set; }
	private RoundData[] allRoundData;
	public Score scoreInstance;
	private int[] highScores;
	private int allRounds;
	private int num;
	

	public void finishRound(){
		round++;
		scoreInstance.requestTopFiveScores();
	}

	public void updateHighScoreList(int[] hsList) {
		highScores = hsList;
	}

	public int getCurrentRound(){
		if (round >= allRoundData.Length)
		{
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

	void Start () {
		DontDestroyOnLoad(gameObject);

		resetRounds();

		LoadGameData();

		SceneManager.LoadScene("MenuScreen");

		scoreInstance = GetComponent<Score>();
	}

	public RoundData getCurrentRoundData(int round){
		Debug.Log("curr data: " + allRoundData.Length);
		return allRoundData[round];
	}

	private void LoadGameData(){
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
		int num = (int) Random.Range(1,4);
		string path = Application.streamingAssetsPath + "/data" + num + ".json";
        WWW www = new WWW(path);
        while(!www.isDone) {}
        string json = www.text;
        GameData data = JsonUtility.FromJson<GameData> (json); 
        allRoundData = data.allRoundData;
	}

	public int[] getHighScores() {
		return highScores;
	}

	public void UploadScoreInstance(){
		scoreInstance.postScore((int)score);
	}

	public void resetRounds(){
		round = 0;
		score = 0;
	}
}
