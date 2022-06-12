using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    public Text pointsText;

    void Update()
    {
        pointsText.text = "Total Points: " + ScorePoints.totalPoints;
    }
}
