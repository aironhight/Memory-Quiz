using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	[SerializeField] public GameObject roundOverPanel;
	[SerializeField] public Text timeText;
	[SerializeField] public GameObject questionPanel;
	[SerializeField] public Text questionText;
	[SerializeField] public Text scoreText;
	[SerializeField] public SimpleObjectPool buttonPool;
	[SerializeField] public Transform answerButtonParent;
	[SerializeField] public Text totalScore;
	[SerializeField] private Image questionImage;
	[SerializeField] public Text hint;
	[SerializeField] public Text[] topScoreTextFields = new Text[5];
	private float imageDisplayTime;
	private DataController dataController;
	private RoundData currRound;
	private float startTime;
	private QuestionData[] questions;
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

	void Start () {
		dataController = FindObjectOfType<DataController>();
		round = dataController.getCurrentRound();
		currRound = dataController.getCurrentRoundData(round);
		image = currRound.image;
		questions = currRound.questions;
		buttons = new List<GameObject>();
		scoreInstance = GetComponent<Score>();

		if(round == -1)
		{
			Debug.Log("no next round");
		}

		if(questionImage != null)
		{
			questionImage.sprite = Resources.Load<Sprite>("Images/" + image);
		}

		imageDisplayTime = 10.0f;
		score = 0;
		startTime = 0;
		timeElapsed = 0;
		questionIndx = 0;
		totalRoundScore = 0;

		showImage = true;
		isInRound = false;

		ShowImage();
	}
	
	
	private void ShowImage() {
		imageDisplayTime -= Time.deltaTime;
		timeText.text = "Time left: " + imageDisplayTime.ToString("0.0");
		if(imageDisplayTime < 0)
		{
			hint.enabled = false;
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
		}

		if(showImage)
		{
			ShowImage();
		}
	}

	private void ClearAnswers() {
		while(buttons.Count > 0)
		{
			buttonPool.ReturnObject(buttons[0]);

			buttons.RemoveAt(0);
		}
	}

	private void SetupQuestion() {
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

		Debug.Log(timePassed.ToString());

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
	public void EndRound() {
		float tScore = timeScore();
		isInRound = false;
		totalRoundScore = score + tScore;

		Debug.Log(totalRoundScore.ToString("0.0"));

		dataController.addScore(totalRoundScore);
		questionPanel.SetActive(false);
		roundOverPanel.SetActive(true);

		Debug.Log(dataController.getTotalScore());

		totalScore.text = "Total Score: " + dataController.getTotalScore();
	}

	public void ReturnToMenu() {
		SceneManager.LoadScene("MenuScreen");
	}

	public void UploadScore() {
		scoreInstance.postScore((int)score);
	}

	public void NextRound() {
		dataController.finishRound();
		SceneManager.LoadScene("Game");
	}

	public void RequestScoreBoardUpdate() {
		scoreInstance.requestTopFiveScores();
	}

	public void InflateScoreBoard(int[] topScores) {
		for(int i=0; i<topScores.Length; i++) {
			topScoreTextFields[i].text = i + ". " + topScores[i];
		}
	}

}
