using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animation_controller : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void play_running_anim()
    {
        anim.SetBool("is_running", true);
        
    }
    public void set_idle()
    {
        anim.SetBool("is_running", false);
        anim.SetBool("is_attacking", false);
    }
    public void play_attack_animation()
    {
        anim.Play("Attack");
    }
}
