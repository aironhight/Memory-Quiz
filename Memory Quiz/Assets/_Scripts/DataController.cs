using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour {

	public RoundData[] allRoundData;
	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);

		SceneManager.LoadScene("MenuScreen");
	}

	public RoundData getCurrentRoundData(int round){
		return allRoundData[round];
	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
