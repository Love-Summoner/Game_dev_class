using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawn_enemy : MonoBehaviour
{
    [SerializeField] private float spawn_rate = 5, enemy_speed = 2, spawn_distance = 10.82f;
    [SerializeField] private GameObject enemy_prefab;
    public TMP_Text clock_text;
    public Button_functions button_functions;

    private float difficulty = 1, seconds = 0, difficulty_timer = 0;
    private float enemy_count = 5;
    void Start()
    {
        if(is_main_level)
            StartCoroutine(play_message("The great circle is temporarily sealed"));
    }
    private bool on_cooldown = false, has_survived = true;
    private float time = 0;
    // Update is called once per frame ((int)(time % 60)).ToString()
    void Update()
    {
        time += Time.deltaTime;
        clock_text.text = Mathf.Floor(time/60).ToString() + ":" + ((time%60 < 10) ? "0" + ((int)(time % 60)).ToString() : ((int)(time % 60)).ToString());
        seconds += Time.deltaTime;
        difficulty_timer += Time.deltaTime;

        if (on_cooldown)
            return;
        
        StartCoroutine(spawn());
        increase_difficulty();

        spawn_enemy();

        if(GameObject.FindGameObjectsWithTag("Enemy").Length < enemy_count)
        {
            for (int i = 0; i < enemy_count; i++)
                spawn_enemy();
        }
        if(time > 900 && has_survived)
        {
            has_survived = false;
            button_functions.won_game();
        }
        open_great_circle();
    }
    private float next_dif_increase_at = 30;
    private void increase_difficulty()
    {
            if(difficulty_timer > next_dif_increase_at)
            {
                //next_dif_increase_at += 5;
                difficulty+=.25f;
                difficulty_timer = 0;
                enemy_count++;
                if(time %240  < 20)
                {
                    spawn_rate *=.7f;
                }
            if (time % 960 < 20)
            {
                spawn_rate *= .2f;
            }
        }
            
    }
    IEnumerator spawn()
    {
        on_cooldown = true;
        yield return new WaitForSeconds(spawn_rate/difficulty);
        on_cooldown = false;
    }
    public float exp_multiplier = 1;
    private void spawn_enemy()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector3 vector = new Vector3();

        vector = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);

        GameObject temp = Instantiate(enemy_prefab, angular_posion(angle, spawn_distance) + transform.position, transform.rotation);
        
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().health += difficulty;
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().exp_mod = difficulty * exp_multiplier;
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().probability_mod = exp_multiplier - 1;
    }
    private Vector3 angular_posion(float cur_angle, float radius)
    {
        float x_pos = radius * Mathf.Cos(cur_angle);
        float y_pos = radius * Mathf.Sin(cur_angle);

        return new Vector2(x_pos, y_pos);
    }

    public GameObject selections, fill_spots;
    public TMP_Text text;
    public bool is_main_level = false;
    private void open_great_circle()
    {
        if(is_main_level && time > 180)
        {
            selections.SetActive(true);
            fill_spots.SetActive(true);

            StartCoroutine(play_message("The great circle is now open"));
            is_main_level=false;
        }
    }
    IEnumerator play_message(string message)
    {
        text.text = message;
        yield return new WaitForSeconds(10);
        text.text = "";
    }
}
