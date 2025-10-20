using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHandle : MonoBehaviour
{

    [SerializeField] private FixedJoint2D connector;
    [SerializeField] private Horse horse;
    private Transform parent;
    [SerializeField] private GameObject swordPrefab;
    private Color colour;
    private bool snapped;

    public void FixedUpdate()
    {
        if (!connector.isActiveAndEnabled)
        {
            Snap(2.5f);
        }
    }

    public void Snap(float time)
    {
        if (!snapped)
        {
            StopAllCoroutines();
            StartCoroutine(Snapped(time));
            colour = gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
    
    private IEnumerator Snapped(float delay)
    {
        snapped = true;
        Debug.Log($"snapped for {delay} seconds");
        horse.swordStabbingCode.active = false;
        connector.enabled = false;
         parent = connector.transform.parent;
        yield return new WaitForSeconds(delay);
        Fix();
    }

    public void Fix()
    {
        if (!snapped)
        {
            return;
        }
        StopAllCoroutines();
        GameObject newSword = Instantiate(swordPrefab, parent);
        SharpBit swordStabbingCode = newSword.GetComponentInChildren<SharpBit>();
        horse.swordStabbingCode = swordStabbingCode;
        swordStabbingCode.horse = horse;
        
        
        Destroy(connector.gameObject);
        
        newSword.GetComponent<SpriteRenderer>().color = colour;
        connector = newSword.GetComponent<FixedJoint2D>();
        connector.connectedBody = transform.parent.GetComponent<Rigidbody2D>();

        foreach (SpriteRenderer flag in swordStabbingCode.flagBits)
        {
            if (horse.active)
            {
                flag.color = horse.player.RiderColour;
            }
            else
            {
                Destroy(flag.gameObject);
            }
            
        }
        
        
        snapped = false;
    }
}
