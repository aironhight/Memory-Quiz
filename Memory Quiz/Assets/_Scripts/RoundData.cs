using UnityEngine;
using System.Collections;

[System.Serializable]
public class RoundData
{
    // store a reference to the correct image inside the Resources/Images
    public int image;
    // name for the round
    public string name;
    public int pointsAddedForCorrectAnswer;
    public Questions[] questions;

}