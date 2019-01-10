using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DBManager : MonoBehaviour {

	private DatabaseReference dbr;
	private int[] topScores;
	private Score scoreInstance;

	// Use this for initialization
	void Start () {
		 // Set up the Editor before calling into the realtime database.
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://memory-quiz.firebaseio.com/");

		//Get the scores reference location of the database.
		dbr = FirebaseDatabase.DefaultInstance.GetReference("scores");
		scoreInstance = GetComponent<Score>();
	}

	//Calls score board inflating methond from the Score class.
	private void displayScore() {
		bubbleSort(topScores);

		scoreInstance.inflateScoreBoard(topScores);
	}

	//Starts an Async task, requesting the top 5 scores from the database.
	//Calls displayScore() if the task is successful 
	public void getTopFiveScores() {
		dbr.OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWith(task => { //Gets the top 5 scores from the database
			if (task.IsFaulted) {
				Debug.Log("Top scores request failed.");
			}
			else if (task.IsCompleted) {
				int[] scores = new int[5];

				int index = scores.Length-1;
				Dictionary<string, object> results = (Dictionary<string, object>) task.Result.Value;
				foreach(var d in results) {
					Dictionary<string, object> resultss = (Dictionary<string, object>) d.Value; //Nested Dictionary? I wanna suicide!
					foreach(var rez in resultss) {
						scores[index] = int.Parse(rez.Value.ToString());
						index--;
					}
				}
				topScores = scores;
				displayScore(); //
			}
		});
	}

	public void postScore(int score) {
		Debug.Log("Pushing the score " + score + " to the database.");
 		DatabaseReference postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(score);
	}

	//Sorts an integer array in decreasing order
	private void bubbleSort(int[] arr) {
		if(arr == null && arr.Length == 1) {
			return;
		}
		bool swops; //boolean showing if we took any actions during the last iteration of the array.
		do{
			swops = false;
			for(int i=(arr.Length-1); i > 0; i--) {
				if(arr[i] > arr[i-1]) {
					int tmp = arr[i-1];
					arr[i-1] = arr[i];
					arr[i] = tmp;

					swops = true;
				}
			}
		} while (swops);
	}
}
