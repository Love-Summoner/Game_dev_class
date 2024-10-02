using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_enemy : MonoBehaviour
{
    [SerializeField] private float spawn_rate = 5, enemy_speed = 2;
    [SerializeField] private GameObject enemy_prefab;

    private float difficulty = 1, seconds = 0, difficulty_timer = 0;
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
        Transform point = transform.GetChild(Random.Range(0, transform.childCount));
        GameObject temp = Instantiate(enemy_prefab, point.position, point.rotation);
        temp.transform.GetChild(0).gameObject.GetComponent<Enemy_AI>().health*=difficulty;
    }
    private void increase_difficulty()
    {
        if(difficulty == 1 || difficulty == 2)
        {
            if(difficulty_timer > 30)
            {
                difficulty+=.25f;
                difficulty_timer = 0;
            }
        }
    }
    IEnumerator spawn()
    {
        on_cooldown = true;
        yield return new WaitForSeconds(spawn_rate/difficulty);
        on_cooldown = false;
    }
}
