using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] private float HP;
    [SerializeField] private float RequiredForAFinalBlow;
    [SerializeField] private SharpBit swordStabbingCode;

     public Rigidbody2D horseHead;
    [SerializeField] private SpriteRenderer sword;
    [SerializeField] private SpriteRenderer rider;
    public bool active;
    public void Start()
    {
        ActivePlayer player = GetComponentInParent<ActivePlayer>();
        Color riderColour = player.RiderColour;
        Color swordColour = player.SwordColour;
        Color horseColour = player.horseColour;


        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.gameObject.layer != 11)
            {
                sprite.color = horseColour;
            }
            else
            {
                sprite.color = player.SwordColour;
            }
        }

        if (active)
        {
            rider.color = riderColour;
            sword.color = swordColour;

        }
        else
        {
            rider.color = Color.black;
            sword.color = Color.black;

        }
    }



    public void Hit(float damage)
    {
        Debug.Log(damage);
        HP -= damage;
        if (HP < 0 && damage > RequiredForAFinalBlow)
        {
            Destroy(this);
        }
    }

    private void OnDestroy()
    {
        List<HingeJoint2D> hinges = new List<HingeJoint2D>(gameObject.GetComponentsInChildren<HingeJoint2D>());

        foreach (HingeJoint2D hinge in hinges)
        {
            hinge.enabled = false;
        }

        List<FixedJoint2D> fixedJoints = new List<FixedJoint2D>(gameObject.GetComponentsInChildren<FixedJoint2D>());

        foreach (FixedJoint2D fixedJoint in fixedJoints)
        {
            fixedJoint.enabled = false;
        }

        swordStabbingCode.enabled = false;
    }

    
}