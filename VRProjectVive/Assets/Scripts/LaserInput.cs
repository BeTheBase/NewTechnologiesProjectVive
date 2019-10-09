using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class LaserInput : MonoBehaviour
{
    public SteamVR_Action_Boolean grabPinch; //Grab Pinch is the trigger, select from inspecter
    public SteamVR_Input_Sources inputSource = SteamVR_Input_Sources.Any;//which controller
    public static GameObject currentObject;
    bool click = false;
    int currentID;
    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    void OnEnable()
    {
        if (grabPinch != null)
        {
            grabPinch.AddOnChangeListener(VRController_OnInteract_ButtonPressed, inputSource);
        }
    }

    private void OnDisable()
    {
        if (grabPinch != null)
        {
            grabPinch.RemoveOnChangeListener(VRController_OnInteract_ButtonPressed, inputSource);
        }
    }

    private void VRController_OnInteract_ButtonPressed(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources, bool isConnected)
    {
        switch(inputSource)
        {
            case SteamVR_Input_Sources.RightHand:
                Vector3 fwd = transform.TransformDirection(Vector3.forward);
                RaycastHit hitt;
                if(Physics.Raycast(transform.position, transform.forward, out hitt, 100f))
                {
                    Debug.Log("Interact");
                    int id = hitt.collider.gameObject.GetInstanceID();

                    if (currentID != id)
                    {
                        currentID = id;
                        currentObject = hitt.collider.gameObject;
                        string name = currentObject.name;
                        if (name == "Button")
                        {
                            Debug.Log("We hitted VR");
                            currentObject.GetComponent<Button>().onClick.Invoke();
                            click = false;
                        }
                        else if (name == "FPSPlayer")
                        {
                            Debug.Log("We found the thief!");
                            EventManager.OnGameEndedCaller();
                        }
                        else if (name == "Etan")
                        {
                            currentObject.SetActive(false);
                        }
                    }
                }
                /*
                RaycastHit[] hits;
                hits = Physics.RaycastAll(transform.position, transform.forward, 100.0f);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    int id = hit.collider.gameObject.GetInstanceID();

                    if (currentID != id)
                    {
                        currentID = id;
                        currentObject = hit.collider.gameObject;
                        string name = currentObject.name;
                        if (name == "Button")
                        {
                            Debug.Log("We hitted VR");
                            currentObject.GetComponent<Button>().onClick.Invoke();
                            click = false;
                        }
                        else if(name == "FPSPlayer")
                        {
                            Debug.Log("We found the thief!");
                            EventManager.OnGameEndedCaller();
                        }
                        else if(name == "Etan")
                        {
                            currentObject.SetActive(false);
                        }
                    }
                }*/
                EventManager.OnActivateLightningCaller();
                break;
            case SteamVR_Input_Sources.LeftHand:
                EventManager.OnActivateUICaller();
                break;
            default:
                break;
        }
        
    }
}
