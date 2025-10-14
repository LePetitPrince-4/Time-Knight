using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharpBit : MonoBehaviour
{
    [SerializeField] private const float requiredMagnitude = 200f;
    public bool active = true;
    [SerializeField] public Horse horse;
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

        if (magnitude < requiredMagnitude || !active)
        {
            return;
        }
        if (other.gameObject.layer == 11)
        {
            FixedJoint2D attachmentPoint = other.gameObject.GetComponent<FixedJoint2D>();

            if (attachmentPoint != null)
            {            
                horse.swordHandle.Snap(1);
                other.gameObject.GetComponentInParent<Horse>().armourRemaining--;
                Destroy(attachmentPoint);
                other.gameObject.GetComponentInParent<ActivePlayer>().rigidbody2CleanUp.Add(other.gameObject.GetComponent<Rigidbody2D>());
            }
            return;
        }

        if (other.gameObject.layer != 8)
        {
            
            other.gameObject.GetComponentInParent<Horse>()?.Hit(magnitude);
        }
    }

    

    
    
    
    
}
