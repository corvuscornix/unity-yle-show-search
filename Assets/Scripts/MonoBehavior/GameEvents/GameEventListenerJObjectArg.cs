using Newtonsoft.Json.Linq;
using UnityEngine.Events;

[System.Serializable]
public class UnityJObjectEvent : UnityEvent<JObject> { }

public class GameEventListenerJObjectArg : GameEventListener<JObject>
{
	public GameEventJObject Event;
	public UnityJObjectEvent Response;

	private void OnEnable() {
		Event.RegisterListener(this);
	}

	private void OnDisable() {
		Event.UnregisterListener(this);
	}

	public override void OnEventRaised(JObject argument) {
		Response.Invoke(argument);
	}
}