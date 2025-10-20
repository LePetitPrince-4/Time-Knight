using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharpBit : MonoBehaviour
{
    public const float requiredMagnitude = 40f;
    public bool active = true;
    [SerializeField] public Horse horse;
    public List<SpriteRenderer> flagBits;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!horse)
        {
            return;
        }
        
        Vector2 force = other.relativeVelocity;

        float angles = gameObject.transform.eulerAngles.z;
        angles = angles * Mathf.Deg2Rad;

        Vector2 normalizingAngle = new Vector2(Mathf.Sin(angles),Mathf.Cos(angles));

        Debug.Log($"Force:{force} normalizingAngle :{normalizingAngle}");


        float magnitude = (force*normalizingAngle).magnitude;
        
        Debug.Log($"Magnitude:{magnitude}");

        if (!active)
        {
            return;
        }
        if (other.gameObject.layer == 11)
        {
            Armour armour = other.gameObject.GetComponent<Armour>();

            if (armour != null && armour.Hit(magnitude))
            {            
                horse.swordHandle.Snap(1);
            }
            return;
        }

        if (other.gameObject.layer != 8)
        {
            Horse otherHorse = other.gameObject.GetComponentInParent<Horse>();
            if (otherHorse.Hit(magnitude))
            {

                if (horse.active)
                {
                    horse.swordHandle.Fix();
                }
                else
                {
                    horse.swordHandle.Snap(1);
                }
                

            }
        }
    }

    

    
    
    
    
}
