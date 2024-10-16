using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ritual_circule : MonoBehaviour
{
    [SerializeField]private Summon_materials material_request = Summon_materials.BONE;
    [SerializeField] private GameObject crow_prefab, wolf_prefab, dragon_prefab;
    public int material_requirement = 1;
    public int summon_count = 1;
    private int mat_count = 0;
    private Inventory inventory;
    private Transform crow_holder;
    private void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
        crow_holder = inventory.gameObject.transform.GetChild(0);
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
            if(inventory.get_item_count((int)material_request) > 0 && mat_requirement_met())
            {
                switch(material_request)
                {
                    case Summon_materials.BONE:
                        spawn_wolf();
                        break;
                    case Summon_materials.FEATHER:
                        spawn_crow();
                        break;
                    case Summon_materials.SCALE:
                        spawn_dragon();
                        break;
                }
                
            }
        }
    }
    private void spawn_wolf()
    {
        GameObject temp = Instantiate(wolf_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<WolfAI>().wolf_count = summon_count;
        increase_mat_count();
    }
    private void spawn_crow()
    {
        GameObject temp = Instantiate(crow_prefab, transform.position, Quaternion.identity);
        temp.GetComponent<Crow>().crow_count = summon_count;
        temp.transform.parent = crow_holder;

        float rad_increment = (Mathf.PI * 2)/crow_holder.childCount;
        float angle = 0;

        foreach(Transform crow in crow_holder)
        {
            crow.localPosition = angular_posion(angle, .4f);
            crow.localScale = new Vector2(.4f, .4f);
            angle += rad_increment;
        }
        increase_mat_count();
    }
    private void spawn_dragon()
    {
        GameObject temp = Instantiate(dragon_prefab, transform.position, Quaternion.identity);
        increase_mat_count();
    }
    private Vector2 angular_posion(float cur_angle, float radius)
    {
        float x_pos = radius * Mathf.Cos(cur_angle);
        float y_pos = radius * Mathf.Sin(cur_angle);



        return new Vector2(x_pos, y_pos);
    }

    private void increase_mat_count()
    {
        if(material_requirement < 3)
            material_requirement++;
        summon_count++;
    }
    private bool mat_requirement_met()
    {
        mat_count++;
        inventory.change_item_count((int)material_request, -1);

        if(mat_count == material_requirement)
        {
            mat_count = 0;
            return true;
        }
        return false;
    }
}
