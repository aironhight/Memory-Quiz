using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	[SerializeField] public GameObject gameOverPanel;
	[SerializeField] public GameObject roundOverPanel;
	[SerializeField] public Text timeText;
	[SerializeField] public GameObject questionPanel;
	[SerializeField] public Text questionText;
	[SerializeField] public Text scoreText;
	[SerializeField] public GameObject buttonPrefab;
	[SerializeField] public Transform answerButtonParent;
	[SerializeField] public Text totalScore;
<<<<<<< HEAD
	[SerializeField] public Image questionImage;
=======
	[SerializeField] private Image questionImage;
	[SerializeField] public Text gameOverScoreText;
	[SerializeField] public GameObject highScorePanel;
	// Old version 
	// [SerializeField] public Text[] topScoreTextFields = new Text[5];
	private float imageDisplayTime;
>>>>>>> 7828181ea859ed53a0cfeadb6b59cf846a6b02d8
	private DataController dataController;
	private RoundData currRound;
	private float startTime;
	private Questions[] questions;
	private int image;
	private float score;
	private bool isInRound;
	private float timeElapsed;
	private int questionIndx;
	private Image img;
	private List<GameObject> buttons;
	private float totalRoundScore;
	private bool showImage;
	private int round;
	private Score scoreInstance;

	void Awake(){
		
	}

<<<<<<< HEAD
	// Use this for initialization

	void Awake(){
		questionImage = GetComponent<Image>();
	}
=======
>>>>>>> 7828181ea859ed53a0cfeadb6b59cf846a6b02d8
	void Start () {
		dataController = FindObjectOfType<DataController>();
		round = dataController.getCurrentRound();

		if(round == -1)
		{
			EndGame();
			return;
		}

		currRound = dataController.getCurrentRoundData(round);
		image = currRound.image;
		questions = currRound.questions;
		buttons = new List<GameObject>();
<<<<<<< HEAD
		imgPos = GetComponent<Image>();
		
		if(questionImage != null){
			questionImage.sprite = Resources.Load<Sprite>("images/1");
		}
=======
		scoreInstance = dataController.scoreInstance;
>>>>>>> 7828181ea859ed53a0cfeadb6b59cf846a6b02d8

	

		if(questionImage != null)
		{
			questionImage.sprite = Resources.Load<Sprite>("Images/" + image);
		}

		imageDisplayTime = 9.9f;
		score = 0;
		startTime = 0;
		timeElapsed = 0;
		questionIndx = 0;
		totalRoundScore = 0;

		showImage = true;
		isInRound = false;

		ShowImage();

		highScorePanel.SetActive(false);
		gameOverPanel.SetActive(false);
	}
	
	
	private void ShowImage() {
		imageDisplayTime -= Time.deltaTime;
		timeText.text = "Time left: " + imageDisplayTime.ToString("0.0");
		if(imageDisplayTime < 0)
		{
			showImage = false;
			questionImage.enabled = false;
			SetupQuestion();
			isInRound = true;
			startTime = Time.time;
		}
		
	}

	void Update () {
		if(isInRound)
		{
			timeElapsed = Time.time - startTime;
			timeText.text = "Time passed:" + timeElapsed.ToString("0.0");
			scoreText.text = "Score: " + score;
		}

		if(showImage)
		{
			ShowImage();
			scoreText.text = "Score: " + dataController.getTotalScore();
		}
	}

	public void DeleteGameObject(GameObject toDelete) 
	{
			Debug.Log(toDelete.name + " - Destroying.");
			Destroy(toDelete);
	}

	private void ClearAnswers() {
		while(buttons.Count > 0)
		{
			// No longer using OP
			// buttonPool.ReturnObject(buttons[0]);

			DeleteGameObject(buttons[0]);
			buttons.RemoveAt(0);
		}


	}

	private void SetupQuestion() {
		ClearAnswers();

		Questions qData = questions[questionIndx];
		questionText.text = qData.questionText;

		for (int i = 0; i < qData.answers.Length; i++)
		{
			GameObject newAnswerButtonGO = (GameObject)GameObject.Instantiate(buttonPrefab);
			buttons.Add(newAnswerButtonGO);
			newAnswerButtonGO.transform.SetParent(answerButtonParent);
			AnswerButton answerButton = newAnswerButtonGO.GetComponent<AnswerButton>();
			answerButton.Setup(qData.answers[i]);
		}
	}

	public void Answer(bool isCorrect) {
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

	private float timeScore() {
		int timePassed = (int)timeElapsed;

		if(timePassed < 5)
			return 100.0f;
		else if(timePassed < 8)
			return 60.0f;
		else if(timePassed < 15)
			return 30.0f;
		else if(timePassed < 30)
			return 10.0f;
		return 0f;
	}
	void EndRound() {
		float tScore = timeScore();
		isInRound = false;
		totalRoundScore = score + tScore;

		dataController.addScore(totalRoundScore);
		questionPanel.SetActive(false);
		roundOverPanel.SetActive(true);

		totalScore.text = "Total Score: " + dataController.getTotalScore();
	}

	public void ReturnToMenu() {
		SceneManager.LoadScene("MenuScreen");
		dataController.ResetRounds();
	}

	private void UploadScore() {
		dataController.UploadScoreInstance();
	}

	public void NextRound() {
		dataController.finishRound();
		SceneManager.LoadScene("Game");
	}

	// public void InflateScoreBoard() {
	// 	int[] topScores = dataController.getHighScores();
	// 	for(int i=0; i<topScores.Length; i++) {
	// 		topScoreTextFields[i].text = (i+1) + ". " + topScores[i];
	// 	}
	// }

	public void EndGame(){
	
		questionPanel.SetActive(false);
		roundOverPanel.SetActive(false);

		gameOverPanel.SetActive(true);
		gameOverScoreText.text = "Total Score: " + dataController.getTotalScore().ToString();
		UploadScore();
		dataController.ResetRounds();
	}

	public void ShowHighScores(){
		// questionPanel.SetActive(false);
		// roundOverPanel.SetActive(false);
		// gameOverPanel.SetActive(false);

		// highScorePanel.SetActive(true);
		// InflateScoreBoard();
		SceneManager.LoadScene("Highscores");
	}

}
