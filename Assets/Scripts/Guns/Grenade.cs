using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grenade : MonoBehaviour
{
    float timer = 2;
    float countdown;
    bool has_exploded = false;

    [SerializeField] GameObject explosion_effect;
    [SerializeField] int power = 10;
    [SerializeField] float radius = 5f;

    void Start() => countdown = timer;

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !has_exploded) Explode();
    }

    void Explode()
    {
        has_exploded = true;

        // Visuals
        if (explosion_effect != null)
        {
            GameObject particle = Instantiate(explosion_effect, transform.position, transform.rotation);
            Destroy(particle, 1f);
        }

        // Physics Logic
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider col in colliders)
        {
            // Talk to the new script on the enemy
            EnemyKnockback enemy = col.GetComponent<EnemyKnockback>();
            if (enemy != null)
            {
                Vector3 pushDir = col.transform.position - transform.position;
                pushDir.y = 0;

                float distance = pushDir.magnitude;
                float falloff = 1 - (distance / radius);
                Vector3 finalVelocity = pushDir.normalized * power * falloff;

                enemy.ApplyKnockback(finalVelocity);
            }
        }

        // Now it is safe to destroy the grenade
        Destroy(gameObject);
    }
}