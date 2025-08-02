using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class PaceKeeper : MonoBehaviour
{

    public const float forwardSpeed = 10;
    [SerializeField] private Rigidbody2D rigidbody;

    // Start is called before the first frame update

    public void FixedUpdate()
    {
        Vector2 forces = Vector2.zero;
        forces.x = forwardSpeed;

        rigidbody.AddRelativeForce(forces, ForceMode2D.Force);
    }

}
