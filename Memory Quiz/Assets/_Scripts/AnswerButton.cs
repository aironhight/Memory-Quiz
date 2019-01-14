using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour {

	[SerializeField]
	public Text answerText;
	private Answers answerData;
	private GameController gameController;
	void Start () {
		gameController = FindObjectOfType<GameController>();
	}

	public void Setup(Answers data){
		answerData = data;
		answerText.text = answerData.answerText;
	}
	
	// Update is called once per frame
	public void HandleClick(){
		gameController.Answer(answerData.isCorrect);
	}
}
