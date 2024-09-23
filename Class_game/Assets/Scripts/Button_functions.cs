using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_functions : MonoBehaviour
{
    public string next_level;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void play_game()
    {
        next_level = "Game_scene";
        SceneManager.LoadScene("Load_screen");
        StartCoroutine(load());
    }
    public void main_menu()
    {
        SceneManager.LoadScene("Main_Menu");
    }
    public void quit()
    {
        Application.Quit();
    }
    IEnumerator load()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(next_level);
        Destroy(gameObject);
    }
}
