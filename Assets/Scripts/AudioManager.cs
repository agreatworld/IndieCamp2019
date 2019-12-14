using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource flip;
    private GameObject player;
    private GameObject[] goasts;
    float timer = 0f;
    public AudioClip[] steps;
    float timer2 = 0f;
    public bool walking;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        goasts = GameObject.FindGameObjectsWithTag("Goast");
        if (goasts.Length > 0)
            PlayWithInterval(IntervalTime(FindCloseDistance(player, goasts)));
        if (walking)
            PlayStepClip();
    }
    private float FindCloseDistance(GameObject target, GameObject[] game)
    {
        float distance = 100f;
        GameObject gameObject;
        for (int i = 0; i < game.Length; i++)
        {
            if ((target.transform.position - game[i].transform.position).magnitude < distance)
            {
                flip = game[i].GetComponent<AudioSource>();
                distance = (target.transform.position - game[i].transform.position).magnitude;
            }
        }
        return distance;
    }
    private float IntervalTime(float distance)
    {
        float i = distance / 6;

        if (distance <= 3f)
            return 0.5f;
        return i;
    }
    private void PlayWithInterval(float interval)
    {
        timer += Time.deltaTime;
        if (timer > interval)
        {
            flip.Play();
            timer = 0f;
        }
    }
    private void PlayStepClip()
    {
        timer2 += Time.deltaTime;
        if (timer2 > 0.5f)
        {
            AudioSource source = player.GetComponent<AudioSource>();
            AudioClip clip;
            clip = steps[Random.Range(0, 12)];
            source.clip = clip;
            source.Play();
            timer2 = 0f;
        }

    }

}
