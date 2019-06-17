using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour {

	public UnityEvent onOpen, onClose;

	private CanvasGroup canvasGroup;

	public bool IsOpened { get; internal set; }

	private void Awake() {
		canvasGroup = GetComponent<CanvasGroup>();
		if (!canvasGroup) {
			canvasGroup = gameObject.AddComponent<CanvasGroup>();
		}

		gameObject.SetActive(true);
	}

	private void Start() {
		DisableCanvas();
	}

	public void Open(bool preventDefault = false) {

		IsOpened = true;

		if (!preventDefault) {
			EnableCanvas();
		}
		Debug.Log("open");
		onOpen.Invoke();
	}

	public void Close(bool preventDefault = false) {

		if (!IsOpened) return;
		Debug.Log("close");
		IsOpened = false;

		if (!preventDefault) {
			DisableCanvas();
		}

		onClose.Invoke();
	}

	public void DisableCanvas() {
		GetComponent<Canvas>().enabled = false;
		Debug.Log("disable");
	}

	public void EnableCanvas() {
		GetComponent<Canvas>().enabled = true;
	}
}
