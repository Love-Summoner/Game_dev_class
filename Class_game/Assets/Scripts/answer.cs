using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class answers : MonoBehaviour
{
    private TMP_Text text;
    [SerializeField] private quiz quiz_manager;

    void Start()
    {
        text = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void when_clicked()
    {
        quiz_manager.recieve_answer(text.text);
    }
}
