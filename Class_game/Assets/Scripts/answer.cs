using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class answers : MonoBehaviour
{
    private TMP_Text text;
    private quiz quiz_manager;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
        quiz_manager = GameObject.Find("Game").GetComponent<quiz>();
    }

    public void when_clicked()
    {
        quiz_manager.recieve_answer(text.text);
    }
}
