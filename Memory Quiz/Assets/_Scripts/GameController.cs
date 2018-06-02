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
	private float imageDisplayTime;
	private DataController dataController;
	private RoundData currRound;
	private float startTime;
	private QuestionData[] questions;
	private int image;
	private int score;
	private bool isInRound;
	private float timeElapsed;
	private int questionIndx;
	private Image img;
	private List<GameObject> buttons;
	private float total;
	private bool showImage;
	private int round;

	void Awake(){
		
	}

	void Start () {
		round = 0;

		dataController = FindObjectOfType<DataController>();
		currRound = dataController.getCurrentRoundData(round);
		image = currRound.image;
		questions = currRound.questions;
		buttons = new List<GameObject>();

		if(questionImage != null)
		{
			questionImage.sprite = Resources.Load<Sprite>("Images/" + image);
		}

		imageDisplayTime = 10.0f;
		score = 0;
		startTime = 0;
		timeElapsed = 0;
		questionIndx = 0;
		total = 0;

		showImage = true;
		isInRound = false;

		ShowImage();
	}
	
	
	private void ShowImage(){
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

	private void ClearAnswers(){
		while(buttons.Count > 0)
		{
			buttonPool.ReturnObject(buttons[0]);

			buttons.RemoveAt(0);
		}
	}

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
		roundOverPanel.SetActive(true);
		totalScore.text = "Total Score: " + total.ToString("0.0");
	}

	public void ReturnToMenu(){
		SceneManager.LoadScene("MenuScreen");
	}

	public void UploadScore()
	{
		// icko da zapovqda
	}

	public void NextRound(){
		// to be done
	}
}
