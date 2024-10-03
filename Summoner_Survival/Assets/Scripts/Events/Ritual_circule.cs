using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual_circule : MonoBehaviour
{
    [SerializeField]private Summon_materials material_request = Summon_materials.BONE;
    [SerializeField] private GameObject crow_prefab, wolf_prefab;
    public int material_requirement = 1;
    private Inventory inventory;

    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            player_in_range = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            player_in_range = false;
        }
    }

    private bool player_in_range = false;
    void Update()
    {
        if (!player_in_range)
            return;
        if(Input.GetKeyDown(KeyCode.E)) 
        {
            if(inventory.get_item_count((int)material_request) >= material_requirement)
            {
                switch(material_request)
                {
                    case Summon_materials.BONE:
                        spawn_wolf();
                        break;
                }
                inventory.change_item_count((int)material_request, -material_requirement);
            }
        }
    }
    private void spawn_wolf()
    {
        GameObject temp = Instantiate(wolf_prefab, inventory.gameObject.transform.position, Quaternion.identity);
        temp.GetComponent<WolfAI>().wolf_count = material_requirement;
        material_requirement++;
    }
}
