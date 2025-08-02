using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] protected PlaneControls planeControls;
    [SerializeField] private Rigidbody2D rigidbody;
    private ForceMode2D flyingForceMode = ForceMode2D.Force;
    private float forwardSpeed = PaceKeeper.forwardSpeed;
    [SerializeField] private Transform paceKeeper;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private float liftRatio;
     private const float idealAngleOfAttack = 13;
    private const float targetHeight = 25;
    private const float rubberBandStrength = 0.1f;

    private const int firingLimit = 10;
    [SerializeField] private int currentCoolDown = 10;
    [SerializeField] private bool hit = false;
    private bool inAir = false;



    [SerializeField] private bullet bullet;



    public void FixedUpdate()
    {

        float torque;

        Vector2 forces = Vector2.zero;


        if ( !hit)
        {
            forces.x = forwardSpeed;
            if (planeControls.faster)
            {
                forces.x += forwardSpeed;

            }


        }


        Vector2 distanceToPaceKeeper = paceKeeper.position - transform.position;
        distanceToPaceKeeper = distanceToPaceKeeper * distanceToPaceKeeper;


        rigidbody.AddRelativeForce(forces, flyingForceMode);
        rigidbody.AddForce(distanceToPaceKeeper* rubberBandStrength* rubberBandStrength, flyingForceMode);


        if (planeControls.up == planeControls.down)
        {
            torque = 0;
        }
        else if (planeControls.up)
        {
            torque = rotationSpeed;
        }else
        {
            torque = -rotationSpeed;
        }


        Vector3 localScale = transform.localScale;

        float rotation = rigidbody.rotation % 180;


        if (rigidbody.rotation> 95 && localScale.y == 1)
        {
            localScale.y = -1;
            transform.localScale = localScale;

        }
        else if (rigidbody.rotation < 85 && localScale.y == -1){
            localScale.y = 1;
            transform.localScale = localScale;

        };
        if (planeControls.fire && currentCoolDown <=0)
        {
            currentCoolDown = firingLimit;
            bullet.Fire(transform);
            return;

        }else if (!bullet.active)
        {
            currentCoolDown--;
        }

        rigidbody.AddTorque(torque);

        if (!inAir && transform.position.y > 5)
        {
            inAir = true;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (inAir && (collision.gameObject != bullet.gameObject))
        {
            Hit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != bullet.gameObject)
        {
            Hit();
        }
    }

    private void Hit()
    {
        hit = true;
        rigidbody.gravityScale = 1f;
        Camera.main.gameObject.GetComponent<CameraScript>().RemovePlane(transform);

    }

}
