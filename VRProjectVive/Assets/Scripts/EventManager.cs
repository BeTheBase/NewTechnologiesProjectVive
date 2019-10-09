using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void ActivateLightning();
    public static ActivateLightning OnActivateLightningCaller { get; set; }

    public delegate void ActivateUI();
    public static ActivateUI OnActivateUICaller { get; set; }

    public delegate void GameEnded();
    public static GameEnded OnGameEndedCaller { get; set; }
}
