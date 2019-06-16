using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityStringEvent : UnityEvent<string> { }

[CreateAssetMenu]
public class GameEventString : GameEvent<string> { };

public class GameEventListenerStringArg : GameEventListener<string>
{
	public GameEventString Event;
	public UnityStringEvent Response;

	private void OnEnable() {
		Event.RegisterListener(this);
	}

	private void OnDisable() {
		Event.UnregisterListener(this);
	}

	public override void OnEventRaised(string argument) {
		Response.Invoke(argument);
	}
}