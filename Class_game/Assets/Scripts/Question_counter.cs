using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Question_counter : MonoBehaviour
{
    [SerializeField] private quiz question_number;
    void Update()
    {
        GetComponent<TMP_Text>().text = (question_number.question_number-1).ToString();
    }
}
