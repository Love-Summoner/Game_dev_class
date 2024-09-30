using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_enemy : MonoBehaviour
{
    [SerializeField] private float spawn_rate = 5, enemy_speed = 2;
    [SerializeField] private GameObject enemy_prefab;
    void Start()
    {
        
    }
    private bool on_cooldown = false;
    // Update is called once per frame
    void Update()
    {
        if (on_cooldown)
            return;

        StartCoroutine(spawn());

        Transform point = transform.GetChild(Random.Range(0, transform.childCount));
        Instantiate(enemy_prefab, point.position, point.rotation);
    }
    IEnumerator spawn()
    {
        on_cooldown = true;
        yield return new WaitForSeconds(spawn_rate);
        on_cooldown = false;
    }
}
