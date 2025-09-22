using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    [SerializeField] private float HP;
    [SerializeField] private float RequiredForAFinalBlow;
    [SerializeField] private SharpBit swordStabbingCode;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
