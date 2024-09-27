using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player_movement : MonoBehaviour
{
    [SerializeField] private float max_speed, start_speed, acceleration;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public float horizontal, vertical;
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.Return)) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
}
