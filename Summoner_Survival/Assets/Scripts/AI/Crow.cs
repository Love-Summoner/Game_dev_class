using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public int crow_count = 0;

    private bool on_cooldown = false;
    private Summon_stats stats;

    private void Start()
    {
        stats = GameObject.Find("Summon_stats").GetComponent<Summon_stats>();
    }
    void Update()
    {
        if(on_cooldown)
        {
            return;
        }
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        on_cooldown=true;
        yield return new WaitForSeconds(stats.fire_speed);
        on_cooldown=false;
    }
}
