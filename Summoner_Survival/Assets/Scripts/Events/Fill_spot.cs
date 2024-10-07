using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_spot : MonoBehaviour
{
    public STAT_TYPE type = STAT_TYPE.SPEED;
    private Main_circle circle;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        circle = transform.parent.parent.GetComponent<Main_circle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interractable && Input.GetKeyDown(KeyCode.E) && (inventory.get_item_count((int)type) > 0))
        {
            circle.Fill_item();
            inventory.change_item_count((int)type, -1);
        }
    }
    private bool interractable = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            interractable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            interractable = false;
        }
    }
}
