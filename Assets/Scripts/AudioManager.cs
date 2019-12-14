using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audio;
    private GameObject player;
    private GameObject[] goasts;
    float timer = 0f;
    private void Awake()
    {
        audio=gameObject.GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        goasts = GameObject.FindGameObjectsWithTag("Goast");
        PlayWithInterval(IntervalTime(FindCloseDistance(player, goasts)));
    }
    private float FindCloseDistance(GameObject target,GameObject[] game)
    {
        float distance = 100f;
        for(int i = 0; i < game.Length; i++)
        {
            if ((target.transform.position - game[i].transform.position).magnitude < distance)
            {
                distance = (target.transform.position - game[i].transform.position).magnitude;
            }
        }
        return distance;
    }
    private float IntervalTime(float distance)
    {
        if (distance > 10)
        {
            return 5f;
        }
        else if (distance < 4)
            return 0.5f;
        else
            return 1f;
    }
    private void PlayWithInterval(float interval)
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            audio.Play();
            timer = 0f;
        }
    }

}
