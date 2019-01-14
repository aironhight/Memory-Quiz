using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoresController : MonoBehaviour
{
    private DataController dataController;
    [SerializeField] public Text[] topScoreTextFields = new Text[5];

    void Start () {
		dataController = FindObjectOfType<DataController>();

        InflateScoreBoard();
	}
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScreen");
    }

    public void InflateScoreBoard() 
    {
		int[] topScores = dataController.getHighScores();
		for(int i=0; i<topScores.Length; i++) {
			topScoreTextFields[i].text = (i+1) + ". " + topScores[i];
		}
	}
}
