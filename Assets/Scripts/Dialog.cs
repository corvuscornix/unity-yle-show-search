using UnityEngine;

public class Dialog : MonoBehaviour {
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

	public void Open() {

		IsOpened = true;

		EnableCanvas();
	}

	public void Close() {

		if (!IsOpened) return;

		IsOpened = false;

		DisableCanvas();
	}

	public void DisableCanvas() {
		GetComponent<Canvas>().enabled = false;
	}

	public void EnableCanvas() {
		GetComponent<Canvas>().enabled = true;
	}
}
