using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_functions : MonoBehaviour
{
    private GameObject pause_menu, death_screen, victory_screen;
    void Start()
    {
        if(GameObject.Find("Pause_menu") != null)
            pause_menu = GameObject.Find("Pause_menu").transform.GetChild(0).gameObject;

        if(GameObject.Find("Death Screen") != null)
            death_screen = GameObject.Find("Death Screen").transform.GetChild(0).gameObject;
        if (GameObject.Find("Victory_screen") != null)
            victory_screen = GameObject.Find("Victory_screen").transform.GetChild(0).gameObject;
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
        SceneManager.LoadScene("Main_level");
    }
    public void load_main_menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main_Menu");
    }
    public void load_tutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");

    }
    public void quit_game()
    {
        Application.Quit();
    }

    public void won_game()
    {
        victory_screen.SetActive(true);
        Time.timeScale = 0;
    }
    public void start_endless_mode()
    {
        Time.timeScale = 1;
        victory_screen.SetActive(false);
    }
    public GameObject controls_screen;
    public void show_controls()
    {
        controls_screen.SetActive(!controls_screen.activeSelf);
    }
}
