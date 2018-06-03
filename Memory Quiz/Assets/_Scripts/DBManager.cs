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
		//getTopFiveScores();

		int[] scores = getTopFiveScores();

		bubbleSort(scores);

		// for(int i=0; i<scores.Length; i++) {
		// 			Debug.Log("The current score of top score #" + (i+1) + " is :" + scores[i]);
		// }
		
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	private void displayScore() {
		bubbleSort(topScores);
		for(int i=0; i<topScores.Length; i++) {
			Debug.Log("The current score of top score #" + (i+1) + " is :" + topScores[i]);
		}

		scoreInstance.inflateScoreBoard(topScores);

		//Here you call the method that shows the score / inflates the scoreboard.
	}

	//Returns the top 3 scores as an int array (index 0 is the highest score, index 1 the second highes, etc.)
	//If the query fails it returns null.
	public void getTopFiveScores() {
		// dbr.OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWith(task => {
		// 	if (task.IsFaulted) {
		// 		Debug.Log("FAILED TASK");
		// 	}
		// 	else if (task.IsCompleted) {
		// 		Dictionary<string, object> results = (Dictionary<string, object>) task.Result.Value;
		// 		int index = scores.Length-1;
		// 		Debug.Log("DO TUK DOBRE");
		// 		foreach(var res in results) {
		// 			Debug.Log(res.Value.ToString());
		// 			// scores[index] = int.Parse(res.Value.ToString());
		// 			// index--;
		// 		}
		// 		for(int i=0; i<scores.Length; i++) {
		//  			Debug.Log("The current score of top score #" + (i+1) + " is :" + scores[i]);
		//  		}
		// 	}
		// });

		dbr.OrderByChild("score").LimitToLast(5).GetValueAsync().ContinueWith(task => {
			if (task.IsFaulted) {
				Debug.Log("FAILED TASK");
			}
			else if (task.IsCompleted) {

				// DataSnapshot snapShot = task.Result;

				// foreach(var dataChild in snapShot.Children) {
				// 	Debug.Log(dataChild.GetValue(false));
				// }
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

				// Dictionary<string, object> results = (Dictionary<string, object>) task.Result.Value;
				// int index = scores.Length-1;
				// foreach(var res in results) {
				// 	scores[index] = int.Parse(res.Value.ToString());
				// 	index--;
				// }

				// scores[0] = int.Parse(results["one"].ToString());
				// scores[1] = int.Parse(results["two"].ToString());
				// scores[2] = int.Parse(results["three"].ToString());
				// scores[3] = int.Parse(results["four"].ToString());
				// scores[4] = int.Parse(results["five"].ToString());

				// for(int i=0; i<scores.Length; i++) {
				// 	Debug.Log("The current score of top score #" + (i+1) + " is :" + scores[i]);
				// }
				topScores = scores;
				displayScore();
			}
		});
	}

	public void postScore(int score) {
		Debug.Log("Pushing the score " + score + " to the database.");
 		DatabaseReference postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(score);

	}

	public void fillDBWithRandomStuff() {
		DatabaseReference postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(17);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(14);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(18);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(11);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(100);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(188);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(1);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(9);
		postRef = dbr.Push().Child("score");
		postRef.SetValueAsync(28);
	}

	private void bubbleSort(int[] arr) {
		bool swops;
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
