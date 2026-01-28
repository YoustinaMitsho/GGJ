using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MaskController : MonoBehaviour
{
    public static MaskController Instance { get; private set; }
    public List<GameObject> maskCameras;
    private int currentIndex = -1;
    public bool isColliderMasked;
    public InputAction cycleMask;
    public List<GameObject> masks = new List<GameObject>();
    private List<MaskedObject> allMaskedObjects = new List<MaskedObject>();
    void Awake()
    {
        Instance = this;
        cycleMask.Enable();
        allMaskedObjects = new List<MaskedObject>(FindObjectsByType<MaskedObject>(0));
    }

    void Update()
    {
        CycleMask();
    }

    public void HasSpawnedSmth()
    {
        allMaskedObjects = new List<MaskedObject>(FindObjectsByType<MaskedObject>(0));
    }

    void CycleMask()
    {
        if (cycleMask.WasPressedThisFrame())
        {
            allMaskedObjects = new List<MaskedObject>(FindObjectsByType<MaskedObject>(0));
            currentIndex = (currentIndex + 1) % maskCameras.Count;
            foreach (MaskedObject maskedObj in allMaskedObjects)
            {
                for (int i = 0; i < maskedObj.maskGroups.Count; i++)
                {
                    if (i == currentIndex && !isColliderMasked)
                    {
                        maskedObj.maskGroups[i] = true;
                        masks[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        maskedObj.maskGroups[i] = false;
                        masks[i].gameObject.SetActive(false);
                    }
                }
            }
            for (int i = 0; i < maskCameras.Count; i++)
            {
                maskCameras[i].SetActive(i == currentIndex);
            }
        }
    }
}