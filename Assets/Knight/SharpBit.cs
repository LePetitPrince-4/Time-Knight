using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharpBit : MonoBehaviour
{
    [SerializeField] private float requiredMagnitude;
    public void OnCollisionEnter2D(Collision2D other)
    {
        
        float magnitude = other.relativeVelocity.magnitude;
        if (magnitude < requiredMagnitude)
        {
            return;
        }
        if (other.gameObject.layer != 8)
        {
            
            other.gameObject.GetComponentInParent<Horse>()?.Hit(magnitude);
        }
    }
}
