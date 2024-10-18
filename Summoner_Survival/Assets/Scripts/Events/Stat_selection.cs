using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STAT_TYPE
{
    SPEED = 0,
    POWER, 
    GROWTH
}
public class Stat_selection : MonoBehaviour
{
    public STAT_TYPE type = STAT_TYPE.SPEED;

    private Main_circle ritual_circle;
    private bool interractable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            interractable=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            interractable = false;
        }
    }
    void Start()
    {
        ritual_circle = transform.parent.parent.GetComponent<Main_circle>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).gameObject.SetActive(interractable);

        if(interractable && Input.GetKeyDown(KeyCode.E))
        {
            ritual_circle.start_ritual(type);
        }
    }
}
