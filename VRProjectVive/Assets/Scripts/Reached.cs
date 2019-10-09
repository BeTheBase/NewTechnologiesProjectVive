using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reached : MonoBehaviour
{
    public GameObject ObjToLerp;
    public int ItemsToFind = 0;
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.name == "WON")
        {
            Debug.Log("WONWONWON");
            EventManager.OnGameEndedCaller();
        }
        if(collision.gameObject.name == "FIND")
        {
            ItemsToFind--;
            if (ItemsToFind <1)
            {
                ObjToLerp.SetActive(false);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
