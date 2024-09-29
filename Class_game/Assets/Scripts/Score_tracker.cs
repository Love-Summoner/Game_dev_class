using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Score_tracker : MonoBehaviour
{
    [SerializeField] private quiz quiz_score;

    void OnEnable()
    {
        GetComponent<TMP_Text>().text = quiz_score.score.ToString();
        float score = quiz_score.score;

        switch(score)
            {
                case <70:
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "You failed...";
                    break;
                case <=100:
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "You Passed!";
                    break;
                case <= 200:
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "Great Score!";
                    break;
                case < 300:
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "Incredible!";
                    break;
                case 300:
                    transform.GetChild(2).GetComponent<TMP_Text>().text = "Perfect Score!\nAmazing!";
                    break;
            }
    }
}
