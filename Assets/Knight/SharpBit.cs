using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharpBit : MonoBehaviour
{
    [SerializeField] private float requiredMagnitude;
    private bool active = true;
    public void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name+ " " + other.gameObject.layer);
        
        float magnitude = other.relativeVelocity.magnitude;
        if (magnitude < requiredMagnitude || !active)
        {
            return;
        }
        if (other.gameObject.layer == 11)
        {
            active = false;
            Invoke("Reactive", 1);
            Destroy(other.gameObject.GetComponent<FixedJoint2D>());
            return;
        }

        if (other.gameObject.layer != 8)
        {
            
            other.gameObject.GetComponentInParent<Horse>()?.Hit(magnitude);
        }
    }

    public void Reactive()
    {
        active = true;
    }
    
    
}
