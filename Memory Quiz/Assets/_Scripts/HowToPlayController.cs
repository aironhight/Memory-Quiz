using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour {

	public void StartGame(){
		SceneManager.LoadScene("SelectAudience");
	}
	public void BackToMenu(){
		SceneManager.LoadScene("MenuScreen");
	}
}
