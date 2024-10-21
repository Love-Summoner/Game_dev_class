using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Tutorial_manager : MonoBehaviour
{
    private string[] messages = { "Hello, welcome to summoner surivival", "For 15 minutes you will be attacked by an army of goblins", "To survive this you must summon allies with a ritual circle and a ritual item",
    "Pick up the ritual items that just spawned", "Good now walk into a ritual circle and press E", "Good! You've summoned some allies",
    "Wolves are summoned with bones and attack the first enemy they see", "Crows are summoned with feathers and shoot bullets in the direction you point", "And dragons are summoned with scales and breath fire on whoever they want","Before you start the actual game you need to know about the main ritual circle",
    "Follow the blank arrow until you find a large ritual circle", "This is the large ritual circle, you can use this to increase your stats.", "Select a stat to increase",
    "Now you must give all small red circles their requested item", "Congradulations you completed a stat boosting ritual and are ready to play the game.  Use escape to return to the main menu"};

    public TMP_Text text;
    public GameObject spawn_item, enemy_spawner;
    public Transform selection;
    public Renderer main_circle_renderer;
    public Summon_stats stats;


    private int ally_count;
    private float damage, speed, exp_rate;
    void Start()
    {
        damage = stats.bullet_damage;
        speed = stats.fire_speed;
        exp_rate = enemy_spawner.GetComponent<Spawn_enemy>().exp_multiplier;

        ally_count = GameObject.FindGameObjectsWithTag("Ally").Length;
        StartCoroutine(change_messages(cur_index));
    }
    private int cur_index = 0;
    private bool is_teaching = false;
    private int[] stop_points = { 3, 4, 10, 12, 13 };
    void Update()
    {
        if(cur_index == 14)
            text.text = messages[cur_index];
        if (!is_teaching && !stop_points.Contains(cur_index))
        {
            StartCoroutine(change_messages(cur_index));
        }
        else if (condition_met())
        {
            cur_index++;
            text.text = messages[cur_index];
        }
    }
    IEnumerator change_messages(int starting_index)
    {
        is_teaching = true;
        text.text = messages[cur_index];
        yield return new WaitForSeconds(5);
        if (cur_index < 13)
        {
            cur_index++;
            is_teaching = false;
            text.text = messages[cur_index];
        }
        if(cur_index == stop_points[0])
            spawn_item.SetActive(true);
    }
    private bool condition_met()
    {
        if (cur_index == stop_points[0] && spawn_item.transform.childCount == 0)
        {
            return true;
        }
        else if (cur_index == stop_points[1] && ally_count < GameObject.FindGameObjectsWithTag("Ally").Length)
        {
            enemy_spawner.SetActive(true);
            return true;
        }
        else if (cur_index == stop_points[2] && main_circle_renderer.isVisible)
        {
            return true;
        }
        else if (cur_index == stop_points[3] && !selection.gameObject.activeSelf)
        {
            return true;
        }
        else if (cur_index == stop_points[4] && stat_increased())
        {
            cur_index = 13;
            return true;
        }
        return false;
    }
    private bool stat_increased()
    {
        if(speed < stats.fire_speed)
        {
            return true;
        }
        else if(damage < stats.bullet_damage)
        {
            return true;
        }
        else if(exp_rate < enemy_spawner.GetComponent<Spawn_enemy>().exp_multiplier)
        {
            return true;
        }
        return false;
    }
}
