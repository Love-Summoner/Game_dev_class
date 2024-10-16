using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill_spot : MonoBehaviour
{
    public STAT_TYPE type = STAT_TYPE.SPEED;
    public Sprite[] item_sprites = new Sprite[3], silhouettes = new Sprite[3];
    private Main_circle circle;
    private Inventory inventory;
    private bool is_filled = false;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        circle = transform.parent.parent.GetComponent<Main_circle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (interractable && Input.GetKeyDown(KeyCode.E) && (inventory.get_item_count((int)type) > 0) && !is_filled)
        {
            circle.Fill_item();
            inventory.change_item_count((int)type, -1);
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = item_sprites[(int)type];
            is_filled = true;
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
    public void reset_circle()
    {
        is_filled = true;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
    }
    public void set_circle()
    {
        int num = UnityEngine.Random.Range(0, 3);
        type = (STAT_TYPE)num;
        is_filled = false;
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = silhouettes[(int)type];
    }
}
