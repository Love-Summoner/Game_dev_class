using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{
    private TMP_Text question;
    private GameObject[] answers = new GameObject[4];
    private string correct_answer;
    private string[] answer_pool = { "Pig", "Dog", "Cat", "Crow", "Lizard", "Deer", "Capybara" };
    private string[,] question_pool = { {"Which of these animals are related to pigs?", "Which of these colors can a pig be?" }, { "", ""} };
    private string[] bonus_answers = { "Daeodon", "Dodo bird", "Sloth", "Alligator", "Brown", "Red", "Blue", "Yellow"};
    private string default_question = "What animal is this?";
    private List<string> already_asked = new List<string>();

    void Start()
    {
        question = GameObject.Find("Question_background").transform.GetChild(0).GetComponent<TMP_Text>();
        GameObject button_parent = GameObject.Find("Buttons");
        int i = 0;

        foreach (Transform t in button_parent.transform)
        {
            answers[i] = t.gameObject;
            i++;
        }
        starting_question();
    }
    private bool init = false;
    private int cur_question;
    private int question_number = 1;
    public void starting_question()
    {
        cur_question = Random.Range(0, answer_pool.Length);
        question.text = default_question;
        correct_answer = answer_pool[cur_question];

        while (init && already_asked.Contains(correct_answer))
        {
            cur_question = Random.Range(0, answer_pool.Length);
            correct_answer = answer_pool[cur_question];
        }

        init = true;

        already_asked.Add(correct_answer);
        allocate_answers();
    }

    public void allocate_answers()
    {
        if (question_number < 6)
            question_number++;
        else
            end_game();

        int correct = Random.Range(0, answers.Length);
        List<string> false_answers = new List<string>();

        for(int i = 0; i < 4; i++)
        {
            string temp_answer = answer_pool[Random.Range(0, answer_pool.Length)];
            while (temp_answer == correct_answer || false_answers.Contains(temp_answer))
            {
                temp_answer = answer_pool[Random.Range(0, answer_pool.Length)];
            }
            answers[i].transform.GetChild(0).GetComponent<TMP_Text>().text = temp_answer;
            false_answers.Add(temp_answer);
        }
        answers[correct].transform.GetChild(0).GetComponent<TMP_Text>().text = correct_answer;
        
    }
    public void recieve_answer(string message)
    {
        Debug.Log(correct_answer);
        if(message == correct_answer)
        {
            starting_question();
            return;
        }
        else
            starting_question();
    }
    private void ask_sub_question()
    {

    }
    private void end_game()
    {
        GameObject.Find("End_Screen").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Game").SetActive(false);
    }
}
