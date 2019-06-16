using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WWWImage : MonoBehaviour
{
	public string url;

	IEnumerator Start() {
		if (url.Length > 0) {
			using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url)) {
				yield return webRequest.SendWebRequest();

				if (webRequest.error != null) {
					Debug.Log(webRequest.error + ": " + url);
				} else {
					Image image = GetComponent<Image>();
					Texture2D texture = DownloadHandlerTexture.GetContent(webRequest);

					image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
				}
			}
		}
	}

	public void LoadImage(string url) {
		this.url = url;

		Image image = GetComponent<Image>();
		image.sprite = null;

		StartCoroutine(Start());
	}
}