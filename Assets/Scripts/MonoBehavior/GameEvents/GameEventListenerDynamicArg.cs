using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityDynamicEvent : UnityEvent<dynamic> { }

[CreateAssetMenu]
public class GameEventDynamic : GameEvent<dynamic> { };

public class GameEventListenerDynamicArg : GameEventListener<dynamic>
{
	public GameEventDynamic Event;
	public UnityDynamicEvent Response;

	private void OnEnable() {
		Event.RegisterListener(this);
	}

	private void OnDisable() {
		Event.UnregisterListener(this);
	}

	public override void OnEventRaised(dynamic argument) {
		Response.Invoke(argument);
	}
}