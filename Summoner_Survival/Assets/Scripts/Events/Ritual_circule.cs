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
                    case Summon_materials.FEATHER:
                        spawn_crow();
                        break;
                }
                inventory.change_item_count((int)material_request, -material_requirement);
            }
        }
    }
    private void spawn_wolf()
    {
        GameObject temp = Instantiate(wolf_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<WolfAI>().wolf_count = material_requirement;
        material_requirement++;
    }
    private void spawn_crow()
    {
        GameObject temp = Instantiate(crow_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<Crow>().crow_count = material_requirement;
        temp.transform.parent = inventory.gameObject.transform;
        switch (material_requirement)
        {
            case 1:
                temp.transform.localPosition = new Vector3(0f, .75f, 0);
                break;
            case 2:
                temp.transform.localPosition = new Vector3(-.25f, .55f, 0);
                break;
            case 3:
                temp.transform.localPosition = new Vector3(.5f, .136f, 0);
                break;
            case 4:
                temp.transform.localPosition = new Vector3(-.25f, -.3f, 0);
                break;
        }
        
        material_requirement++;
    }
}
