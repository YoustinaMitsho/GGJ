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
    public AudioSource maskSwitchSound;
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
            maskSwitchSound.Play();

            // ✅ ALWAYS switch masks visuals (even if no enemies exist)
            for (int i = 0; i < masks.Count; i++)
            {
                masks[i].gameObject.SetActive(i == currentIndex);
                //maskSwitchSound.Play();
            }

            // ✅ Update masked objects if any exist
            foreach (MaskedObject maskedObj in allMaskedObjects)
            {
                for (int i = 0; i < maskedObj.maskGroups.Count; i++)
                {
                    maskedObj.maskGroups[i] = (i == currentIndex && !isColliderMasked);
                }
            }

            // Cameras
            for (int i = 0; i < maskCameras.Count; i++)
            {
                maskCameras[i].SetActive(i == currentIndex);
            }
        }
    }
}