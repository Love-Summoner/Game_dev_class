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
        if(targets.Contains(useless))
            targets.RemoveAt(targets.IndexOf(useless));
    }
    public GameObject get_target(int i) { return targets.ToArray()[i]; }

    public bool target_exist(int i)
    {

        if (targets.Count == 0)
            return false;
        return targets.Count > i;
    }
    public GameObject wolf_target(int i)
    {
        if(targets.Count == 0)
        {
            return null;
        }
        while (i < targets.Count && targets.ToArray()[i] != null && targets.ToArray()[i].GetComponent<Enemy_AI>().targeted)
        {
            i++;
        }
        if (i >= targets.Count || targets.ToArray()[i] == null || targets.ToArray()[i].GetComponent<Enemy_AI>().targeted)
        {
            return null;
        }
        
        targets.ToArray()[i].GetComponent<Enemy_AI>().targeted = true;
        return targets.ToArray()[i];
    }   
    private void clear_nulls()
    {
        if (targets.Count == 0)
            return;
        for(int i  = 0; i < targets.Count; i++)
        {
            if (targets.ToArray()[i] == null)
            {
                targets.RemoveAt(i);
            }
        }
    }
    private void Update()
    {
        clear_nulls() ;
    }
}
