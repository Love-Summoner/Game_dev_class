using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Main_circle : MonoBehaviour
{
    [SerializeField] GameObject fill_spot;
    public TMP_Text instructions_UI;
    public TMP_Text[] stat_numbers = new TMP_Text[3];

    private GameObject selection, sacrifices;
    public STAT_TYPE current_ritual;
    private Summon_stats stats;
    private float surivival_time = 12;

    private string[] ritual_instructions = new string[3];
    private Player_movement mov;
    private void Start()
    {
        mov = GameObject.Find("Player").GetComponent<Player_movement>();
        ritual_instructions[0] = "Don't Get Hit\n" + (surivival_time - cur_time) + "Seconds left";
        ritual_instructions[1] = "Kill enemies while in the circle:\t\t" + (kill_requirement - cur_kills).ToString("0.00") + " remaining";
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
        selection = transform.GetChild(0).gameObject;
        sacrifices = transform.GetChild(1).gameObject;
        spawn_fill_points();
    }

    public void start_ritual(STAT_TYPE type)
    {
        selection.SetActive(false);

        current_ritual = type;
        set_circles();
    }
    public bool Ritual_being_done;
    private int kill_requirement = 10, cur_kills = 0;
    public bool count_kills = false, was_hit = false;
    void Update()
    {
        if(!Ritual_being_done)
        {
            return;
        }

        switch (current_ritual)
        {
            case STAT_TYPE.SPEED:
                instructions_UI.text = "Don't Get Hit\n" + (surivival_time - cur_time).ToString("0.00") + "Seconds left";
                speed_ritual();
                break;
            case STAT_TYPE.POWER:
                instructions_UI.text = "Kill enemies while in the circle:\t\t" + (kill_requirement - cur_kills).ToString() + " remaing";
                count_kills = true;
                power_ritual();
                break;
            case STAT_TYPE.GROWTH:
                instructions_UI.text = "Collect experience:\n" + (required_exp-cur_exp).ToString("0");
                growth_ritual();
                break;
        }
    }
    private void power_ritual()
    {
        if(cur_kills >= kill_requirement)
        {
            stat_numbers[1].text = (System.Int32.Parse(stat_numbers[1].text) + 1).ToString();
            end_ritual();
            stats.increase_damage();
            up_difficulty();
        }
    }
    public void add_kill()
    {
        if (interractable)
        {
            cur_kills++;
        }
    }
    private float cur_time = 0;
    private void speed_ritual()
    {
        if(mov.health < starting_health)
        {
            end_ritual();
        }
        cur_time += Time.deltaTime;
        if (cur_time >= surivival_time)
        {
            stat_numbers[0].text = (System.Int32.Parse(stat_numbers[0].text) + 1).ToString();
            up_difficulty();
            stats.increase_speed();
            end_ritual();
        }
    }
    public float cur_exp = 0, required_exp = 20;
    private void growth_ritual()
    {
        if(cur_exp >= required_exp)
        {
            foreach(GameObject t in GameObject.FindGameObjectsWithTag("Experience"))
            {
                t.GetComponent<Item>().is_interacting = true;
            }
            GameObject.Find("Enemy_spawn_points").GetComponent<Spawn_enemy>().exp_multiplier += .5f;
            stat_numbers[2].text = (System.Int32.Parse(stat_numbers[2].text) + 1).ToString();
            up_difficulty();
            cur_exp = 0;
            end_ritual();
        }
    }
    private bool max_requirement = false;
    private float starting_health;
    private void end_ritual()
    {
        instructions_UI.text = "";
        count_kills = false;
        Ritual_being_done = false;
        cur_kills = 0;
        cur_time = 0;
        was_hit = false;
        selection.SetActive(true);
        spawn_fill_points();
    }

    private bool interractable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            interractable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            interractable = false;
        }
    }
    private void up_difficulty()
    {
        if(surivival_time < 60)
        {
            surivival_time += 12;
            kill_requirement += 5;
            required_exp += 20;
        }
    }
    private int item_count = 0, item_requirement = 1;

    public void Fill_item()
    {
        item_count++;
        if(item_count >= item_requirement)
        {
            item_count = 0;
            if (item_requirement < 5)
                item_requirement++;
            Ritual_being_done = true;
            starting_health = mov.health;
        }
    }
    private void spawn_fill_points()
    {
        
        if(item_requirement == 5 && !max_requirement)
        {
            max_requirement = true;
            GameObject temp = Instantiate(fill_spot, transform.position, transform.rotation);
            temp.transform.parent = sacrifices.transform;
        }
        else if(!max_requirement)
        {
            GameObject temp = Instantiate(fill_spot, transform.position, transform.rotation);
            temp.transform.parent = sacrifices.transform;
        }

        float angle = (Mathf.PI * 2)/sacrifices.transform.childCount;
        float cur_angle = 0;
        foreach(Transform t in sacrifices.transform)
        {
            t.localPosition = angular_position(cur_angle, .435f);
            cur_angle += angle;
            t.GetComponent<Fill_spot>().reset_circle();
        }
    }
    private Vector2 angular_position(float cur_angle, float radius)
    {
        float x_pos = radius * Mathf.Cos(cur_angle);
        float y_pos = radius * Mathf.Sin(cur_angle);



        return new Vector2(x_pos, y_pos);
    }
    private void set_circles()
    {
        foreach (Transform t in sacrifices.transform)
        {
            t.GetComponent<Fill_spot>().set_circle();
        }
    }
}
