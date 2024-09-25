using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class quiz : MonoBehaviour
{
    private TMP_Text question;
    private GameObject[] answers = new GameObject[4];
    private string correct_answer;
    private string[] answer_pool = { "Pig", "Dog", "Cat", "Crow", "Lizard", "Deer", "Capybara" };

    private string[,] question_pool = { {"Which of these animals are related to pigs?", "Which of these colors can a pig be?" }, 
        { "Which of these animals are closely related to dogs?", "Which of these dog breeds is the biggest?"},
        { "How many Lives to cats have?", "Is the cat in Schrodinger's box dead or alive?"},
        { "What is a flock of crows called?", "What age does a human become smarter than a crow?"},
        { "What is the biggest species of lizard?", "What family of lizards does the Komodo Dragon belong to?"},
        {"What is a female deer called?","What is the species of deer that have fangs?" },
        { "What are capybaras?", "How big are capybara herds?"} };

    private string[,] bonus_answers = { { "Daeodon", "Dodo bird", "Sloth", "Alligator", "Brown", "Red", "Blue", "Yellow" }, 
        { "Wolves", "GroundHog", "Cat", "Ant Eater", "Great Dane", "Concur Spaniel", "Shih Tzu", "Goldren Retriever" },
        { "9", "1", "3", "0", "Both", "Neither", "Dead", "Alive" },
        { "Murder", "Flock", "Group", "Family", "7", "3", "5", "8" },
        { "Komodo Dragon", "Chameleon", "Beaded Dragon", "Gila Monster", "Monitor Lizards", "Connoli", "Gekkonidae", "Dibamidae" }, 
        { "Doe", "Female Deet", "Fawn", "Buck", "Musk Deer", "Deer", "Reindeer", "Moose"},
        { "Rodents", "Reptiles", "Birds", "Fish", "10-20", "1-2", "5-10", "20-40" }};
    private string default_question = "What animal is this?";
    private List<string> already_asked = new List<string>();
    public float score = 0;

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
        Debug.Log(correct_answer);
    }
    public void recieve_answer(string message)
    {
        if (message == correct_answer)
        {
            score += 20;
            ask_sub_question(cur_question);
            return;
        }
        else
        {
            Sub_question_asked = new List<int>();
            starting_question();
        }
    }

    private List<int> Sub_question_asked = new List<int>();
    private void ask_sub_question(int this_question)
    {
        if(Sub_question_asked.Count == 2)
        {
            Sub_question_asked = new List<int>();
            starting_question();
            return;
        }
        int which_question = Random.Range(0, 2);
        while (Sub_question_asked.Contains(which_question))
        {
            which_question = Random.Range(0, 2);
        }
        Sub_question_asked.Add(which_question);
        int answer_pointer = which_question * 4;
        correct_answer = bonus_answers[this_question, answer_pointer];
        
        question.text = question_pool[this_question, which_question];

        allocate_sub_asnwers(answer_pointer, this_question);
    }

    private void allocate_sub_asnwers(int anwer_marker, int real_question)
    {
        List<int> sub_asnwer_buttons = new List<int>();
        int temp = 0;
        for (int i = 0; i < 4; i++)
        {
            temp = Random.Range(0, 4);
            while (sub_asnwer_buttons.Contains(temp))
            {
                temp = Random.Range(0, 4);
            }
            sub_asnwer_buttons.Add(temp);

            answers[temp].GetComponentInChildren<TMP_Text>().text = bonus_answers[real_question, anwer_marker+i];
        }
    }
    private void end_game()
    {
        Debug.Log(score);
        GameObject.Find("End_Screen").transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Game").SetActive(false);
    }
}
