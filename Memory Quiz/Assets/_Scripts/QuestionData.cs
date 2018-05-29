using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData 
{
    [SerializeField] public Image questionImage;
    public string questionText;
    public AnswerData[] answers;
}