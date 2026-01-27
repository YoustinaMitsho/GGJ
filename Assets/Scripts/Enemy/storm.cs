using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class storm : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform fortress;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(fortress.position);

    }
}
