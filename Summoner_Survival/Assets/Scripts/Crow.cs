using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private GameObject bullet;


    private bool on_cooldown = false;
    private float fire_rate = 2;
    void Update()
    {
        if(on_cooldown)
        {
            return;
        }
        Instantiate(bullet, transform.position, transform.rotation);
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        on_cooldown=true;
        yield return new WaitForSeconds(fire_rate);
        on_cooldown=false;
    }
}
