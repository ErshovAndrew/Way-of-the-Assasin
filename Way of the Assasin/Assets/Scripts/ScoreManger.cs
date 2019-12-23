using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManger : MonoBehaviour
{
    public static ScoreManger instance;
    public TextMeshProUGUI text;
    int score;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
public void ChangeScore(int coinValue)
    {
        score += coinValue;
        text.text = score.ToString();
    }
}
