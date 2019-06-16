using UnityEngine;

public abstract class GameEventListener<T> : MonoBehaviour
{
	public abstract void OnEventRaised(T argument);
}