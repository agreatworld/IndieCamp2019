using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float hp=100f;
    public List<RaycastHit2D> hits;
    public OnCollision collision;
    public bool catching = false;
    private Image forward;
    private GameObject slider;
    public int catchCount = 0;
    private GameObject fightGoast;
    public float timer = 0;
    private float maxAlone = 60f;
    public bool alone=true;
    private Image hurt;
    public GameObject Goast;
	private new Camera camera;
	private CameraFocus cameraFocus;
    private Animator anim;
    private void Awake()
    {
        collision = GetComponent<OnCollision>();
        forward = GameObject.FindWithTag("Forward").GetComponent<Image>();
        slider = GameObject.FindWithTag("Slider");
        hurt = GameObject.FindWithTag("Hurt").GetComponent<Image>();
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		cameraFocus = camera.GetComponent<CameraFocus>();
        Goast = GameObject.FindWithTag("Goast");
        anim = gameObject.GetComponent<Animator>();
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
        if (alone && !catching)
        {
            timer += Time.deltaTime;
            Transparent();
            if (timer > maxAlone)
                Dead(); 
        }
        hurt.color = new Color(1, 1, 1, 1 - hp / 100);
    }
    private void CatchGoast()
    {
        if (Input.GetKeyDown(KeyCode.E)&&Goast.GetComponent<Goast>().catched)
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
                        fightGoast = hits[i].collider.gameObject;
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
			cameraFocus.isFocusing = true;
			camera.transform.DOShakePosition(0.3f, 0.08f);
            forward.fillAmount += 0.07f;
        }
        if (forward.fillAmount == 1)
        {
			cameraFocus.isFocusing = false;
			Debug.Log("Win");
            catchCount++;
            slider.SetActive(false);
            catching = false;
            Destroy(fightGoast);
            anim.SetTrigger("Rec");
        }
        else if (forward.fillAmount == 0)
        {
			cameraFocus.isFocusing = false;
			slider.SetActive(false);
            catching = false;
            fightGoast.transform.position += (fightGoast.transform.position - transform.position).normalized * 0.2f;
        }
            
    }
    private void Transparent()
    {
        gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.00028f);
    }
    private void Dead()
    {
        Debug.Log("Dead");
    }
    private void ChangeHurt()
    {
       
    }
}
