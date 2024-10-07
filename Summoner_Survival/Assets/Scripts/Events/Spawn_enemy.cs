using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_enemy : MonoBehaviour
{
    [SerializeField] private float spawn_rate = 5, enemy_speed = 2, spawn_distance = 10.82f;
    [SerializeField] private GameObject enemy_prefab;

    private float difficulty = 1, seconds = 0, difficulty_timer = 0;
    private float enemy_count = 5;
    void Start()
    {
        
    }
    private bool on_cooldown = false;
    // Update is called once per frame
    void Update()
    {
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
    }
    private float next_dif_increase_at = 30;
    private void increase_difficulty()
    {
            if(difficulty_timer > next_dif_increase_at)
            {
                next_dif_increase_at += 5;
                difficulty+=.25f;
                difficulty_timer = 0;
                enemy_count++;
            }
    }
    IEnumerator spawn()
    {
        on_cooldown = true;
        yield return new WaitForSeconds(spawn_rate/difficulty);
        on_cooldown = false;
    }
    private void spawn_enemy()
    {
        float angle = Random.Range(0f, 2 * Mathf.PI);
        Vector3 vector = new Vector3();

        vector = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle), 0);

        GameObject temp = Instantiate(enemy_prefab, angular_posion(angle, spawn_distance) + transform.position, transform.rotation);
        
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().health += difficulty;
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().exp_mod = difficulty;
    }
    private Vector3 angular_posion(float cur_angle, float radius)
    {
        float x_pos = radius * Mathf.Cos(cur_angle) + radius;
        float y_pos = radius * Mathf.Sin(cur_angle);

        if(cur_angle <= Mathf.PI/2 || cur_angle >= (Mathf.PI*3)/2) 
        {
            x_pos += radius;
        }

        return new Vector2(x_pos, y_pos);
    }
}
