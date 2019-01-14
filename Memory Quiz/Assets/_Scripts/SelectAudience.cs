using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectAudience : MonoBehaviour
{

    private DataController dataController;
    // Start is called before the first frame update
    void Start()
    {
        dataController = FindObjectOfType<DataController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadGameData(string audience)
    {
		string path = Application.streamingAssetsPath + "/" + audience + ".json";
        WWW www = new WWW(path);
        while(!www.isDone) {}
        string json = www.text;

		// Transform json to useable data
        GameData data = JsonUtility.FromJson<GameData> (json); 

		// Set the current round data to the one loaded from json
        dataController.LoadGameData(data.allRoundData);
    }

    public void Classic()
    {
        LoadGameData("classic");
        SceneManager.LoadScene("Game");
    }
    public void Children()
    {
        LoadGameData("children");
        SceneManager.LoadScene("Game");
    }
    public void Adult()
    {
        LoadGameData("adult");
        SceneManager.LoadScene("Game");
    }
    public void Elderly()
    {
        LoadGameData("elderly");
        SceneManager.LoadScene("Game");
    }
}
