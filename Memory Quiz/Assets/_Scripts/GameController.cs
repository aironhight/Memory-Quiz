using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField] public GameObject GameOverPanel;
	[SerializeField] public GameObject ImagePanel;
	[SerializeField] public Text timeText;
	[SerializeField] public GameObject questionPanel;
	[SerializeField] public Text questionText;
	[SerializeField] public Text scoreText;
	[SerializeField] public Image imgPos;
	[SerializeField] public SimpleObjectPool buttonPool;
	[SerializeField] public Transform answerButtonParent;
	[SerializeField] public Text totalScore;
	private DataController dataController;
	private RoundData currRound;
	private float startTime;
	private QuestionData[] questions;
	private int score;
	private bool isInRound;
	private float timeElapsed;
	private int questionIndx;
	private Image img;
	private List<GameObject> buttons;
	private float total;

	// Use this for initialization
	void Start () {
		dataController = FindObjectOfType<DataController>();
		currRound = dataController.getCurrentRoundData();
		questions = currRound.questions;
		buttons = new List<GameObject>();
		imgPos = GetComponent<Image>();

		score = 0;
		startTime = Time.time;
		timeElapsed = 0;
		questionIndx = 0;
		total = 0;

		SetupQuestion();
		isInRound = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isInRound)
		{
			timeElapsed = Time.time - startTime;
			timeText.text = timeElapsed.ToString("0.0");
		}
	}

	private void ClearAnswers(){
		while(buttons.Count > 0)
		{
			buttonPool.ReturnObject(buttons[0]);

			buttons.RemoveAt(0);
		}
	}


	// private void SetupImage(){
	// 	QuestionData qData = questions[questionIndx];
	// 	imgPos.GetComponent<Renderer>().overrideSprite = img;
	// }

	private void SetupQuestion(){
		ClearAnswers();

		QuestionData qData = questions[questionIndx];
		questionText.text = qData.questionText;

		for (int i = 0; i < qData.answers.Length; i++)
		{
			GameObject answerButtonGO = buttonPool.GetObject();
			buttons.Add(answerButtonGO);
			answerButtonGO.transform.SetParent(answerButtonParent);
			AnswerButton answerButton = answerButtonGO.GetComponent<AnswerButton>();
			answerButton.Setup(qData.answers[i]);
		}
	}

	public void Answer(bool isCorrect){
		if(isCorrect)
		{
			score += currRound.pointsAddedForCorrectAnswer;
			scoreText.text = "Score: " + score;
		}

		if (questions.Length > questionIndx + 1)
		{
			questionIndx++;

			SetupQuestion();
		}
		else
		{
			EndRound();
		}
	}

	public void EndRound(){
		isInRound = false;
		total = score - (timeElapsed / 10);
		questionPanel.SetActive(false);
		GameOverPanel.SetActive(true);
		totalScore.text = "Total Score: " + total.ToString("0.0");
	}

	public void ReturnToMenu(){
		SceneManager.LoadScene("MenuScreen");
	}
}
