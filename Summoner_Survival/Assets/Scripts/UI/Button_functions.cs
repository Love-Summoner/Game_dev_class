using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_functions : MonoBehaviour
{
    private GameObject pause_menu, death_screen;
    void Start()
    {
        pause_menu = GameObject.Find("Pause_menu").transform.GetChild(0).gameObject;
        death_screen = GameObject.Find("Death Screen").transform.GetChild(0).gameObject;
        Time.timeScale = 1;
    }

    public void pause_game()
    {
        if(pause_menu.activeSelf == false)
        {
            pause_menu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pause_menu.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void death()
    {
        death_screen.SetActive(true);
        Time.timeScale = 0;
    }
    public void Load_game()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
}
