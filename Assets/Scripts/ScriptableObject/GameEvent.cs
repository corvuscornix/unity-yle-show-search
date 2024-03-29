﻿using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject {

	private readonly List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();

	public void Invoke(T argument = default) {
		for (int i = listeners.Count - 1; i >= 0; i--) {
			listeners[i].OnEventRaised(argument);
		}
	}

	public void RegisterListener(GameEventListener<T> listener) {
		listeners.Add(listener);
	}

	public void UnregisterListener(GameEventListener<T> listener) {
		listeners.Remove(listener);
	}

	public int GetListenersCount() {
		return listeners.Count;
	}

	internal List<GameEventListener<T>> GetListeners() {
		return listeners;
	}
}
