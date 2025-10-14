using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public const float Damage = 100000;
    public const float deathVelocity = 500f;
    public bool blocking = true;
    public void OnCollisionEnter2D(Collision2D other)
    {
        Horse horse = other.gameObject.GetComponentInParent<Horse>(); 

         if (!blocking)
        {
            if (!horse)
            {
                Destroy(other.collider);
            }
        }
        else if ( other.gameObject.layer == 11)
        {
            if (other.gameObject.GetComponent<FixedJoint2D>())
            Destroy(other.gameObject.GetComponent<FixedJoint2D>());
            horse.armourRemaining--;
            other.gameObject.GetComponentInParent<ActivePlayer>().rigidbody2CleanUp.Add(other.gameObject.GetComponent<Rigidbody2D>());

        }else if(other.gameObject.layer == 8)
        {
            GameObject otherGameObject = other.gameObject;

            int count = 0;
            while (!horse || count < 5)
            {
                otherGameObject = otherGameObject?.transform?.parent?.gameObject;
                Debug.Log(count +": " + otherGameObject.name);

                horse = otherGameObject.GetComponent<Horse>();
                if (horse)
                {
                    break;
                }
                count++;
            }
            
            SwordHandle handle = horse.swordHandle;
            handle.Snap(5);
        }else if (other.gameObject.layer == 6)
        {
            return;
        }
        else if (horse && (other.relativeVelocity.magnitude > deathVelocity || horse.offScript))
        {
            
            horse.Hit(Damage);
        }
        
    }

}
