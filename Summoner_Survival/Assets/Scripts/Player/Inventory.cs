using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Summon_materials { FEATHER=0, SCALE, BONE}

public struct slot
{
    public Summon_materials material;
    public int count;
}
public class Inventory : MonoBehaviour
{
    public slot[] inventory = new slot[(int)Summon_materials.BONE + 1];
    public float cur_level = 0, cur_experience = 0, exp_til_level_up = 5;
    public Slider slider;
    public TMP_Text level_counter;

    private visualize_inventory vis;

    private void Start()
    {
        vis = GameObject.Find("inventory").GetComponent<visualize_inventory>() ;
        slider = GameObject.Find("experience_bar").GetComponent<Slider>();
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
    private int level = 1;
    private float exp_increment = 3;
    public void level_up(float val)
    {
        cur_experience+=val;
        
        if(cur_experience >= exp_til_level_up)
        {
            level++;
            level_counter.text = "Level " + level.ToString();
            change_item_count(get_random_item(), 1);
            cur_experience = cur_experience - exp_til_level_up;
            exp_til_level_up += exp_increment;
            cur_level++;
            if(cur_level % 10 == 0)
            {
                exp_increment *= 2;
            }
        }
        
        slider.value = cur_experience/exp_til_level_up;
    }
    public void change_item_count(int slot_number, int amount)
    {
        if (inventory[slot_number].count + amount >= 0)
        {
            inventory[slot_number].count += amount;
            vis.change_slot_icon(slot_number);
        }
    }
    public int get_item_count(int slot_number)
    {
        return inventory[slot_number].count;
    }
    private int[] in_a_rows = { 0,0,0};
    private int get_random_item()
    {
        int ran = UnityEngine.Random.Range(0, (int)Summon_materials.BONE + 1);
        while (in_a_rows[ran] > 0)
        {
            ran = UnityEngine.Random.Range(0, (int)Summon_materials.BONE + 1);
            if(in_a_rows[ran] > 0)
            {
                in_a_rows[ran]--;
            }
        }

        in_a_rows[ran]++;
        return ran;
    }
}
