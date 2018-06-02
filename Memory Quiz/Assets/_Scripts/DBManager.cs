using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class DBManager : MonoBehaviour {

	private DatabaseReference dbr;

	private void Awake() {
		dbr = FirebaseDatabase.DefaultInstance.GetReference("highscore");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
