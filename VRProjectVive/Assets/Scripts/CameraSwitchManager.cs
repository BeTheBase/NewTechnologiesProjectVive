using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class CameraSwitchManager : MonoBehaviour
{
    [SerializeField] private List<Transform> cameraPositions;
    [SerializeField] private List<Button> cameraSwitchButtons;
    [SerializeField] private GameObject vrCameraController;
    [SerializeField] private GameObject guardUIPanel;

    public delegate void OnCameraUpdate(int index);
    public OnCameraUpdate OnCameraUpdateCaller;



    private int positionIndex = 0;

    private SteamVR_TrackedObject trackedObj;       // The tracked object that is the controller

    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller
    public GameObject LookLight;

    void OnEnable()
    {
        if (grabPinch != null)
        {
            grabPinch.AddOnChangeListener(VRController_OnInteract_ButtonPressed, inputSource);
        }
        OnCameraUpdateCaller += UpdateCameraPosition;
        EventManager.OnActivateLightningCaller += TurnLighting;
        EventManager.OnActivateUICaller += TurnUI;
    }

    private void OnDisable()
    {
        if (grabPinch != null)
        {
            grabPinch.RemoveOnChangeListener(VRController_OnInteract_ButtonPressed, inputSource);
        }
        EventManager.OnActivateLightningCaller -= TurnLighting;
        EventManager.OnActivateUICaller -= TurnUI;
    }

    private void Start()
    {
        InitButtons();
    }

    private void VRController_OnInteract_ButtonPressed(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources, bool isConnected)
    {
        Debug.Log("Interact");
    }

    private void TurnUI()
    {
        if (guardUIPanel == null) return;
        Activate(guardUIPanel);

    }

    private void TurnLighting()
    {
        if (LookLight == null) return;
        Activate(LookLight);
        Debug.Log("Look light on/off");
    }

    private void Activate(GameObject obj)
    {
        if (!obj.activeSelf)
            obj.SetActive(true);
        else
            obj.SetActive(false);
    }

    private void InitButtons()
    {
        for(int index = 0; index < cameraSwitchButtons.Count; index++)
        {
            cameraSwitchButtons[index].onClick.AddListener(() => UpdateCameraPosition(index));
        }
    }

    /*private void NextCameraPosition()
    {
        
    }*/

    public void DebugButton()
    {
        Debug.Log("VR click");
    }

    public void UpdateCameraPosition(int _positionIndex)
    {
        if (vrCameraController == null) return;
        vrCameraController.transform.TotalTransformSetByIndex(_positionIndex, cameraPositions);
    }

    private Transform NextCameraPosition()
    {
        if(positionIndex < cameraPositions.Count)
        {
            positionIndex++;
            return cameraPositions[positionIndex];
        }
        else
        {
            positionIndex = 0;
            return cameraPositions[positionIndex];
        }
    }

    private void OnDestroy()
    {
        for (int index = 0; index < cameraSwitchButtons.Count; index++)
        {
            cameraSwitchButtons[index].onClick.RemoveAllListeners();
        }
    }
}
