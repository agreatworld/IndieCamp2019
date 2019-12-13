using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    List<GameObject> goasts;
    public float probeRange = 1f;
    private void Update()
    {
        goasts = new List<GameObject>(GameObject.FindGameObjectsWithTag("Goast"));
    }
    private void LateUpdate()
    {
        Probe();
    }
    private void Probe()
    {
        for(int i = 0; i < goasts.Count; i++)
        {
            if ((goasts[i].transform.position - gameObject.transform.position).magnitude < probeRange)
                goasts[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
