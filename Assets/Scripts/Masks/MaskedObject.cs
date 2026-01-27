using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MaskedObject : MonoBehaviour
{

    [HideInInspector] public List<bool> maskGroups;
    public List<bool> Layer;    
    void Start() 
    {
        maskGroups = new List<bool>(Layer.Count);
        for(int i = 0; i < Layer.Count; i++)
        {
            maskGroups.Add(false);
        }
    }
    void Update()
    {
        CollidersControl();

    }
    int activeMask = -1;
    bool shouldRender = true;
    private void CollidersControl()
    {
        activeMask = -1; 
        for (int i = 0; i < maskGroups.Count; i++)
        {
            if (maskGroups[i])
            {
                activeMask = i;
                break;
            }
        }
        if(activeMask == -1)
        {
            shouldRender = true;
        }
        else
        {
            shouldRender = (activeMask < Layer.Count && Layer[activeMask]);
        }
        gameObject.GetComponent<BoxCollider>().enabled = shouldRender;
    }
}
