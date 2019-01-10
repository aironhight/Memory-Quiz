using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
  
// © 2017 TheFlyingKeyboard and released under MIT License
// theflyingkeyboard.net
public class ColorChange : MonoBehaviour { 
    [SerializeField] private Gradient colorOverTime; 
    [SerializeField] private float timeMultiplier = 0.5f; 
  
    private Image image; 
  
    [SerializeField] private bool changeColor = false; 
    [SerializeField] private bool goBack = false; 
  
    private float currentTimeStep; 
  
    private void Start() 
    { 
		image = GetComponent<Image>();
  
        if (changeColor) 
        { 
            StartChangingColor(image, colorOverTime, timeMultiplier); 
        } 
    } 
  
    private IEnumerator ChangeTextColor(Image newImage, Gradient newGradient, float timeSpeed) 
    { 
        while (true) 
        { 
            if (goBack) 
            { 
                currentTimeStep = Mathf.PingPong(Time.time * timeSpeed, 1); 
            } 
            else 
            { 
                currentTimeStep = Mathf.Repeat(Time.time * timeSpeed, 1); 
            } 
  
            newImage.color = newGradient.Evaluate(currentTimeStep); 
  
            yield return null; 
        } 
    } 
  
    public void StartChangingColor(Image newImage = null, Gradient newGradient = null, float timeSpeed = -1.0f) 
    { 
        if (newImage != null && newGradient != null && timeSpeed > 0.0f) 
        { 
            StartCoroutine(ChangeTextColor(newImage, newGradient, timeSpeed)); 
        } 
        else if(newImage != null && newGradient != null) 
        { 
            StartCoroutine(ChangeTextColor(newImage, newGradient, timeMultiplier)); 
        } 
        else if(newGradient != null && timeSpeed > 0.0f) 
        { 
            StartCoroutine(ChangeTextColor(image, newGradient, timeSpeed)); 
        } 
        else if (newImage != null && timeSpeed > 0.0f) 
        { 
            StartCoroutine(ChangeTextColor(newImage, colorOverTime, timeSpeed)); 
        }else if(newImage != null) 
        { 
            StartCoroutine(ChangeTextColor(newImage, colorOverTime, timeMultiplier)); 
        }else if(newGradient != null) 
        { 
            StartCoroutine(ChangeTextColor(image, newGradient, timeMultiplier)); 
        }else if(timeSpeed > 0.0f) 
        { 
            StartCoroutine(ChangeTextColor(image, colorOverTime, timeSpeed)); 
        } 
    } 
  
    public void StopChangingColor() 
    { 
        StopCoroutine(ChangeTextColor(image, colorOverTime, timeMultiplier)); 
    }
}