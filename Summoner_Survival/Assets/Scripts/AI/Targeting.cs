using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            targets.Add(collision.gameObject);
        }
    }
    public void Destoy_target(GameObject useless)
    {
        targets.Remove(useless);
    }
    public GameObject get_target(int i) { return targets.ToArray()[i]; }

    public bool target_exist(int i)
    {
        return targets.Count > i;
    }
}
