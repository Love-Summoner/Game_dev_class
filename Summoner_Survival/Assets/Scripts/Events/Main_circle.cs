using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_circle : MonoBehaviour
{
    [SerializeField] GameObject fill_spot;

    private GameObject selection, sacrifices;
    private STAT_TYPE current_ritual;
    private Summon_stats stats;
    private float surivival_time = 12;

    private void Start()
    {
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
        selection = transform.GetChild(0).gameObject;
        sacrifices = transform.GetChild(1).gameObject;
        spawn_fill_points();
    }

    public void start_ritual(STAT_TYPE type)
    {
        selection.SetActive(false);

        current_ritual = type;
        Ritual_being_done = true;
    }
    public bool Ritual_being_done;
    private int kill_requirement = 10, cur_kills = 0;
    public bool count_kills = false, was_hit = false;
    void Update()
    {
        if(!Ritual_being_done || !requirement_met)
        {
            return;
        }

        switch (current_ritual)
        {
            case STAT_TYPE.SPEED:
                speed_ritual();
                break;
            case STAT_TYPE.POWER:
                count_kills = true;
                power_ritual();
                break;
            case STAT_TYPE.GROWTH:
                break;
        }
    }
    private void power_ritual()
    {
        if(cur_kills >= kill_requirement)
        {
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
        if(was_hit)
        {
            end_ritual();
        }
        cur_time += Time.deltaTime;
        if (cur_time >= surivival_time)
        {
            up_difficulty();
            stats.increase_speed();
            end_ritual();
        }
    }
    private void end_ritual()
    {
        requirement_met = false;
        count_kills = true;
        Ritual_being_done = false;
        cur_kills = 0;
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
        }
    }
    private int item_count = 0, item_requirement = 1;
    private bool requirement_met = false;
    public void Fill_item()
    {
        item_count++;
        if(item_count >= item_requirement)
        {
            item_count = 0;
            if (item_requirement < 5)
                item_requirement++;
            requirement_met = true;
        }
    }
    private void spawn_fill_points()
    {
        GameObject temp = Instantiate(fill_spot, transform.position, transform.rotation);
        temp.transform.parent = sacrifices.transform;
        if(item_requirement == 1 || item_requirement == 5)
        {
            return;
        }

        float angle = (Mathf.PI * 2)/sacrifices.transform.childCount;
        float cur_angle = 0;
        foreach(Transform t in sacrifices.transform)
        {
            t.localPosition = angular_position(cur_angle, .435f);
            cur_angle += angle;
        }
    }
    private Vector2 angular_position(float cur_angle, float radius)
    {
        float x_pos = radius * Mathf.Cos(cur_angle);
        float y_pos = radius * Mathf.Sin(cur_angle);



        return new Vector2(x_pos, y_pos);
    }
}
