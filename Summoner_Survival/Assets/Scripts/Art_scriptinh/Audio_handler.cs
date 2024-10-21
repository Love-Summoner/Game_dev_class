using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio_handler : MonoBehaviour
{
    public float volume = .5f;
    public AudioSource experience_sound;
    public Slider volume_slider;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Main_Menu")
        {
            volume_slider = GameObject.Find("volume_slider").GetComponent<Slider>();
            volume_slider.value = volume;
        }

        DontDestroyOnLoad(gameObject);
        Update_volume();
    }
    public float exp_pitch_mod = 0;
    public void play_exp_sound()
    {
        experience_sound.pitch = Random.Range(.95f, 1.05f) + exp_pitch_mod;
        experience_sound.Play();

        StartCoroutine(increase_exp_pitch());
    }
    public void Update_volume()
    {
        experience_sound.volume = volume;
    }

    IEnumerator increase_exp_pitch()
    {
        if(exp_pitch_mod < .3f)
            exp_pitch_mod += .01f;
        yield return new WaitForSeconds(1);
        exp_pitch_mod -= .01f;

        if (exp_pitch_mod < 0)
            exp_pitch_mod = 0;
    }
    private void Update()
    {
        if(volume_slider != null)
        {
            volume = volume_slider.value;
            Update_volume();
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name != "Main_Menu")
            return;
        volume_slider = GameObject.Find("volume_slider").GetComponent<Slider>();
        if (volume_slider != null)
            volume_slider.value = volume;
    }
    }
