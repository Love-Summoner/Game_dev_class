using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_movement : MonoBehaviour
{
    [SerializeField] private float max_speed, start_speed, acceleration;
    [SerializeField]private bool immortal = false;
    public float health = 5;

    private Button_functions button_functions;
    private GameObject health_bar, bar_tracker;
    private Camera cam;
    private Slider slider;

    private Rigidbody2D rb;
    private float max_health;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        button_functions = GameObject.Find("Button_functions").GetComponent<Button_functions>() ;
        health_bar = GameObject.Find("health_bar");
        bar_tracker = transform.GetChild(2).gameObject;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        slider = health_bar.GetComponent<Slider>();
        max_health = health;
    }

    public float horizontal, vertical;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button_functions.pause_game();
        }

        health_bar.transform.position = cam.WorldToScreenPoint(bar_tracker.transform.position);
        slider.value =  health/max_health;
    }

    [SerializeField]private float cur_speed = 0;
    private float direction = 0;
    private void FixedUpdate()
    {
        direction = ((Mathf.Abs(vertical) + (Mathf.Abs(horizontal))) > 1) ? Mathf.Sqrt(2f)/2 : 1;

        if(cur_speed >= max_speed)
        {
            rb.velocity = new Vector2 (horizontal*max_speed*direction, vertical*max_speed*direction);
        }
        else if(horizontal > 0 || vertical > 0)
        {
            cur_speed += acceleration;
            rb.velocity = new Vector2(horizontal * cur_speed * direction, vertical * cur_speed * direction);
        }
        else
        {
            cur_speed /= 2;
        }
        if(cur_speed < acceleration) 
        {
            cur_speed = 0;
        }
    }
    private void die()
    {
        button_functions.death();
    }
    public void hurt()
    {
        if (immortal)
            return;
        health -= Time.deltaTime;
        if(health <=  0 )
        {
            die();
        }
    }
    public void heal()
    {
        if (health + max_health / 3 < max_health)
        {
            health += max_health / 3;
        }
        else
            health = max_health;
    }
}
