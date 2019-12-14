using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goast : MonoBehaviour
{
    public float speed = 2f;
    private Vector2 velocity;
    private GameObject shadow;
    private GameObject player;
    public float protectRange = 2f;
    public float attackRange = 2.5f;
    public bool catched = false;
    private float timer = 0f;
    public float attackInterval = 1f;
    private void Awake()
    {
        shadow = GameObject.FindWithTag("Shadow");
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Vector3 movePos = Composite();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        if (catched)
        {
            Catched();
        }
        Move(movePos);
    }
    private void Move(Vector2 move)
    {

        move = move * speed * Time.deltaTime;
        velocity = move;
        transform.Translate(move);
    }
    private Vector3 Composite()
    {
        Vector3 vec = new Vector3();
        if ((gameObject.transform.position - shadow.transform.position).magnitude < protectRange)
        {
            vec = -(shadow.transform.position - gameObject.transform.position).normalized;
        }
        else
        {
            if ((gameObject.transform.position - player.transform.position).magnitude < attackRange&&catched==false)
            {
                Attack();
                vec = Vector3.zero;
            }
            else
            {
                vec = (player.transform.position - gameObject.transform.position).normalized;
                
            }
            if (Vector3.Angle((shadow.transform.position - player.transform.position), (transform.position - shadow.transform.position)) < 90f)
            {
                vec = GetVerticalDir((transform.position - shadow.transform.position));
                if (Vector3.Dot(vec, transform.position-player.transform.position) > 0)
                    vec = -vec;
            }
        }
        return vec;
    }
    public Vector3 GetVerticalDir(Vector3 dir)
    {
        return new Vector3(-dir.y, dir.x).normalized;
    }
    public void Catched()
    {
        if (speed.CompareTo(0f)<0.01f)
            speed = 0.05f;
        speed -= 0.05f;
    }
    private void Attack()
    {
        Debug.Log(timer);
        timer += Time.deltaTime;
        if(timer>attackInterval)
        {
            player.GetComponent<Player>().hp -= 15f;
            timer = 0f;
        }
    }
}
