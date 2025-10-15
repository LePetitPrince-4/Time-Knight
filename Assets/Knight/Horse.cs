using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Horse : MonoBehaviour
{

    public KnightHandler handler;
    [SerializeField] public SharpBit swordStabbingCode;
    [SerializeField] public SwordHandle swordHandle;

     public Rigidbody2D horseHead;
    [SerializeField] private SpriteRenderer sword;
    [SerializeField] private SpriteRenderer swordHandleRender;
    
    [SerializeField] private SpriteRenderer rider;
    public ActivePlayer player;
    public const int ArmourStartingValue = 8;
    public int armourRemaining = 8;
    public bool active;
    public bool offScript;
    public void Start()
    {
        player = GetComponentInParent<ActivePlayer>();
        Color riderColour;
        Color swordColour;
        Color horseColour = player.horseColour;

        if (active)
        {
            riderColour = player.RiderColour;
            swordColour = player.SwordColour;
        }
        else
        {
            riderColour = player.NPCRiderColour;
            swordColour = player.NPCRiderColour;
        }

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites)
        {
            if (sprite.gameObject.layer == 12)
            {
                if (active)
                {
                    sprite.color = riderColour;
                }
                else
                {
                    Destroy(sprite.gameObject);
                }
            }
            else if (sprite.gameObject.layer != 11)
            {
                sprite.color = horseColour;
            }
            else
            {
                sprite.color = player.SwordColour;
            }
        }
        
        rider.color = riderColour;
        sword.color = swordColour;
        swordHandleRender.color = swordColour;

    }

    public void DeclareOffScript()
    {
        if (offScript)
        {
            Destroy(this);
        }
        offScript = true;
        Destroy(rider);
    }


    public void Hit(float damage)
    {
        if (handler.stillPlaying && armourRemaining != ArmourStartingValue)
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