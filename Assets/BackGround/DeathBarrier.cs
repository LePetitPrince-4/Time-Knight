using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public const float Damage = 100000;
    public const float deathVelocity = 115f;
    public bool blocking = true;
    [SerializeField]private bool testing;
    public void OnCollisionEnter2D(Collision2D other)
    {
        Horse horse = other.gameObject.GetComponentInParent<Horse>();

        if (testing)
        {
            other.gameObject.GetComponentInParent<ActivePlayer>()?.ReturnToTest();
            return;
        }
        
         if (!blocking)
        {
            if (!horse)
            {
                Destroy(other.collider);
            }
        }
        else if ( other.gameObject.layer == 11)
        { 
            other.gameObject.GetComponent<Armour>()?.Hit(Damage);
        }
         else if(other.gameObject.layer == 8)
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
