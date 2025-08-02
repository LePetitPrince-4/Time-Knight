using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private Vector2 finalScale;
    [SerializeField] private Vector2 finalPosition;
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private Transform bulletParent;

    [SerializeField] private int steps;
    [SerializeField] private int maxSteps;
    public bool active;
    public void Fire(Transform plane)
    {
        bulletParent.localPosition = plane.localPosition;
        bulletParent.localRotation = plane.localRotation;
        bulletParent.localScale = plane.localScale;
        bulletParent.parent = plane.parent;

        gameObject.SetActive(true);
        steps = 0;
        active = true;
    }


    public void FixedUpdate()
    {

        steps++;

        if (steps > maxSteps)
        {
            gameObject.SetActive(false);
            active = false;

        }

        float ratio = (float)steps / (float)maxSteps;
        gameObject.transform.localPosition = startingPosition + ratio * finalPosition;
        gameObject.transform.localScale = ratio * finalScale;


    }

}

