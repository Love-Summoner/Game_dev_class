using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public int crow_count = 0;

    private bool on_cooldown = false;
    private float fire_rate = 2;
    void Update()
    {
        if(on_cooldown)
        {
            return;
        }
        GameObject temp = Instantiate(bullet, transform.position, transform.rotation);
        temp.GetComponent<Bullet>().target_number = crow_count;
        temp.GetComponent<Bullet>().Select_Target();
        StartCoroutine(shoot());
    }

    IEnumerator shoot()
    {
        on_cooldown=true;
        yield return new WaitForSeconds(fire_rate);
        on_cooldown=false;
    }
}
