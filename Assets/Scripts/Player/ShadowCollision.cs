using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCollision : OnCollision
{
    
    private void Start()
    {
        base.Start();
    }
    private void Update()
    {
        base.Update();
        
    }
    private void LateUpdate()
    {
        base.LateUpdate();
    }
    public override void HandleHits()
    {
        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.tag == "Goast")
                {
                    Debug.Log(0);
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
            }
        }
        hits.Clear();
    }
}
