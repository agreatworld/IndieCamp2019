using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hp;
    public List<RaycastHit2D> hits;
    public OnCollision collision;
    private bool catching = false;
    private Image forward;
    private GameObject slider;
    private void Awake()
    {
        collision = GetComponent<OnCollision>();
        forward = GameObject.FindWithTag("Forward").GetComponent<Image>();
        slider = GameObject.FindWithTag("Slider");
    }
    private void Start()
    {
        slider.SetActive(false);
    }
    private void Update()
    {
        hits = collision.hits;
        CatchGoast();
        if (catching)
        {
            Catching();
        }


    }
    private void CatchGoast()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (hits.Count != 0)
            {
                Debug.Log(1);
                for(int i = 0; i < hits.Count; i++)
                {
                    Debug.Log(hits[i].collider.name);
                    if (hits[i].collider.tag == "Goast")
                    {
                        catching = true;
                        forward.fillAmount = 0.5f;
                    }
   
                }
            }
        }
    }
    private void Catching()
    {
        slider.SetActive(true);
        forward.fillAmount -= 0.005f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            forward.fillAmount += 0.1f;
        }
        if (forward.fillAmount == 1)
        {
            Debug.Log("Win");
            slider.SetActive(false);
            catching = false;
        }
        else if (forward.fillAmount == 0)
        {
            slider.SetActive(false);
            catching = false;
            for (int i = 0; i < hits.Count; i++)
            {
                if (hits[i].collider.tag == "Goast")
                {
                    hits[i].collider.gameObject.transform.position += (hits[i].collider.gameObject.transform.position - transform.position).normalized * 0.2f;
                }
            }
        }
            
    }
}
