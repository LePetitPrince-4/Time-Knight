using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class CameraScript : MonoBehaviour
{
    public Rigidbody2D targetLocation;
    public Vector2 startingLocation;
    public float lerp;
    public float speed;

    public void GoToTarget(Rigidbody2D target, float time)
    {
        
        targetLocation = target;
        

        Vector3 startLoc = transform.position;
        
        startingLocation = startLoc;
        
        if (time > 0)
        {
            enabled = true;
        }
        else
        {
            Vector3 targetLoc = target.position;

            targetLoc.z = -10;
            
            transform.position = targetLoc;
            return;
        }

        speed = time;

        lerp = 0;
    }

    public void Update()
    {
        lerp += Time.unscaledDeltaTime / speed;

        Vector3 targetLoc= Vector2.Lerp(startingLocation, targetLocation.position, lerp);

        

        targetLoc.z = -10;
        transform.position = targetLoc;
        
    }
    

}
