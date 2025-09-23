using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public const float damage = 100000;
    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == 8 || other.gameObject.layer == 11)
        {
            Destroy(other.gameObject.GetComponent<FixedJoint2D>());
        }
        else
        {
            other.gameObject.GetComponentInParent<Horse>()?.Hit(damage); 
        }
    }

}
