using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.IO;

public class DataController : MonoBehaviour {

	private int round = 0;
	private string dataFileName = "data.json";
	private float score;
	// public float Score { get; set; }
	private RoundData[] allRoundData;
	

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

		LoadGameData();

		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData getCurrentRoundData(int round){
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

		string path = Application.streamingAssetsPath + "/data.json";
        WWW www = new WWW(path);
        while(!www.isDone) {}
        string json = www.text;
        GameData data = JsonUtility.FromJson<GameData> (json); 
        allRoundData = data.allRoundData;
	}
}
