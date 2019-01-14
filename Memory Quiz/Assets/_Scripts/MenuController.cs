using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	private DataController dataController;
	void Start () {
		dataController = FindObjectOfType<DataController>();
	}

	public void StartGame(){
		SceneManager.LoadScene("SelectAudience");
	}

	public void ShowHighScores(){
		SceneManager.LoadScene("Highscores");
	}

	public void HowToPlay(){
		SceneManager.LoadScene("HowToPlay");
	}
	
	public void ExitGame(){
		Application.Quit();
	}
}
