using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Summon_materials { FEATHER=0, SCALE, BONE}

public struct slot
{
    public Summon_materials material;
    public int count;
}
public class Inventory : MonoBehaviour
{
    private slot[] inventory = new slot[(int)Summon_materials.BONE + 1];
    public float cur_level = 0, cur_experience = 0, exp_til_level_up = 10;

    private void Start()
    {
        allocate_inventory();
    }

    private void allocate_inventory()
    {
        int i = 0;
        foreach(Summon_materials mat in Enum.GetValues(typeof(Summon_materials)))
        {
            inventory[i].material = mat;
            inventory[i].count = 0;
            i++;
        }
    }

    public void level_up(int val)
    {
        cur_experience+=val;

        if(cur_experience >= exp_til_level_up)
        {
            change_item_count(UnityEngine.Random.Range(0, (int)Summon_materials.BONE + 1), 1);
            cur_experience = cur_experience - exp_til_level_up;
            exp_til_level_up += 10;
            cur_level++;
        }
    }
    public void change_item_count(int slot_number, int amount)
    {
        Debug.Log(inventory[slot_number].count);
        if (inventory[slot_number].count + amount >= 0)
            inventory[slot_number].count += amount;

        Debug.Log(inventory[slot_number].count);
        Debug.Log(inventory[slot_number].material);
    }
}
