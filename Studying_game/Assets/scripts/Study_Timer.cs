using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study_Timer : MonoBehaviour
{
    [SerializeField] private float wait_time = 20;
    private AudioSource source;
    private float cur_time = 0;
    void Start()
    {
        cur_time = wait_time;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        cur_time -= Time.deltaTime;

        if(cur_time <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            cur_time = wait_time;
            source.Play();
        }
    }
}
