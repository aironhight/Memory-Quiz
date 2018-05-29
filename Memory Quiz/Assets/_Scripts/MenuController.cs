using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public void StartGame(){
		SceneManager.LoadScene("Game");
	}

	public void ShowHighScores(){
		// to be done
	}
	public void ExitGame(){
		Application.Quit();
	}
}
