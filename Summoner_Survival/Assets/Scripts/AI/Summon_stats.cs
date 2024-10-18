using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon_stats : MonoBehaviour
{
    public float bullet_damage, fire_speed, melee_damage, melee_speed, cool_down = 5, shadow_time = 3, attack_time = 1, wolf_move_speed = 5;
    
    public void increase_damage()
    {
        float temp = bullet_damage;
        bullet_damage += .5f;
        melee_damage *= (bullet_damage / temp);
    }
    public void increase_speed()
    {
        fire_speed *= .8f;
        melee_speed *= .8f;
        cool_down *= .8f;
        shadow_time *= .8f;
        attack_time *= .8f;
        wolf_move_speed *= 1.2f;
    }
}
