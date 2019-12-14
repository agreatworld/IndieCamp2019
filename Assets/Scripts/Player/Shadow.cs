using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    List<GameObject> goasts;
    public float probeRange = 1f;
    public Vector2 velocity;

    public float speed = 5f;
    public float minMovableY;
    public float maxMovableY;
    private float timer = 0;
    private float maxAlone = 60f;
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        goasts = new List<GameObject>(GameObject.FindGameObjectsWithTag("Goast"));
        MoveByGetAxis();
        Accompany();
        if (player.GetComponent<Player>().alone && !player.GetComponent<Player>().catching)
        {
            timer += Time.deltaTime;
            Transparent();

        }
    }
    private void LateUpdate()
    {
        Probe();
    }
    private void Probe()
    {
        if (goasts.Count > 0)
            for (int i = 0; i < goasts.Count; i++)
            {
                if ((goasts[i].transform.position - gameObject.transform.position).magnitude < probeRange)
                {
                    goasts[i].GetComponent<SpriteRenderer>().enabled = true;
                    goasts[i].GetComponent<Goast>().catched = true;
                }
                if ((goasts[i].transform.position - gameObject.transform.position).magnitude > probeRange)
                {
                    goasts[i].GetComponent<Goast>().speed = 2.2f;
                    goasts[i].GetComponent<Goast>().catched = false;
                }

            }
    }
    private void MoveByGetAxis()
    {
        float translationX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float translationY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        if (transform.position.y + translationY > maxMovableY)
        {
            translationY = Mathf.Abs(transform.position.y - maxMovableY) < 0.1f ? 0 : maxMovableY - transform.position.y;
        }
        else if (transform.position.y + translationY < minMovableY)
        {
            translationY = Mathf.Abs(transform.position.y - minMovableY) < 0.1f ? 0 : transform.position.y - minMovableY;
        }
        Vector2 translation = new Vector2(translationX, Mathf.Clamp(translationY, minMovableY, maxMovableY));
        velocity = translation;
        transform.Translate(translation);
    }
    private void Transparent()
    {
        gameObject.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.00028f);
    }
    private void Accompany()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude < 1f)
        {
            timer = 0f;
            player.GetComponent<Player>().alone = false;
            player.GetComponent<Player>().timer = 0f;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            player.GetComponent<Player>().alone = true;
        }

    }
}
