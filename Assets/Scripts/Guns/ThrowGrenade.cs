using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    public Rigidbody prefabToThrow;
    public float throwForce = 100f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Throw();
        }
    }

    void Throw()
    {
        Rigidbody rb = Instantiate(
            prefabToThrow,
            transform.position,
            transform.rotation
        );

        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }
}
