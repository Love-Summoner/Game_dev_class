using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_functions : MonoBehaviour
{
    public string next_level;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void play_game()
    {
        next_level = "Game_scene";
        SceneManager.LoadScene("Load_screen");
        StartCoroutine(load(3));
    }
    public void main_menu()
    {
        next_level = "Main_Menu";
        SceneManager.LoadScene("Load_screen");
        StartCoroutine(load(1));
    }
    public void quit()
    {
        Application.Quit();
    }
    public void how_to_play()
    {
        GameObject.Find("Tutorial").transform.GetChild(0).gameObject.SetActive(true);
        canvas.SetActive(false);
    }
    public void back_to_menu()
    {
        canvas.SetActive(true);
        GameObject.Find("Tutorial").transform.GetChild(0).gameObject.SetActive(false);
    }
    IEnumerator load(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(next_level);
        Destroy(gameObject);
    }
}
