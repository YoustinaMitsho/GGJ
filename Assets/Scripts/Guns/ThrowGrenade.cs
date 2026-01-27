using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowGrenade : MonoBehaviour
{
    [Header("Refrences")]
    public Transform cam;
    public Transform attackpoint;
    public GameObject object_to_throw;
    public float throw_force = 10;
    public float raise_force = 10;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Throw();
        }
    }

    private void Throw()
    {
        GameObject projectile = Instantiate(object_to_throw, attackpoint.position, cam.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 throw_direction = cam.forward * throw_force + cam.up * raise_force;
        rb.AddForce(throw_direction, ForceMode.Impulse);
    }
}
