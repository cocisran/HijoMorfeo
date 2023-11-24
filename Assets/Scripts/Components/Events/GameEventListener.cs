using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object> { };
public class GameEventListener : MonoBehaviour
{
    // Game event al cual escuchamos
    [Header("EventBrodcast")]
    public GameEvent gameEventBroadcaster;
    public CustomGameEvent response;

    private void OnEnable()
    {
        gameEventBroadcaster.RegisterListener(this);
    }


    private void OnDisable()
    {
        gameEventBroadcaster.UnregisterListener(this);
    }


    public void OnEventRaised(Component sender, object data)
    {
        response.Invoke(sender, data);
    }
}
