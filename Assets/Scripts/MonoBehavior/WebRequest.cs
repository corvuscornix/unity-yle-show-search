using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{

	public string method;
	public string url;
	public int pageSize = -1;

	public UnityStringEvent onResponseHandler, onErrorHandler;

	private const string QUERY_PLACEHOLDER = "[query]";
	private const string OFFSET_PLACEHOLDER = "[offset]";
	private const string PAGESIZE_PLACEHOLDER = "[pagesize]";

	private string query = "";
	private int offset = 0;

	private Coroutine requestCoroutine;

	public void SetQuery(string query = "") {
		this.query = query;
	}

	public void SetPage(int page) {
		this.offset = page * pageSize;
	}

	public void SetNextPage() {
		this.offset += pageSize;
	}

	public void SetPreviousPage() {
		this.offset -= pageSize;
		if (this.offset < 0) this.offset = 0;
	}

	public void Send() {
		string url = this.url;
		if (url.Contains(QUERY_PLACEHOLDER)) {
			url = url.Replace(QUERY_PLACEHOLDER, query);
		}

		if (pageSize > 0 && url.Contains(PAGESIZE_PLACEHOLDER)) {
			url = url.Replace(PAGESIZE_PLACEHOLDER, pageSize.ToString());
		}

		if (url.Contains(OFFSET_PLACEHOLDER)) {
			url = url.Replace(OFFSET_PLACEHOLDER, offset.ToString());
		}

		Send(url, method);
	}

	public void Send(string url, string method) {
		if (requestCoroutine != null) return;
		requestCoroutine = StartCoroutine(PerformRequest(url, method));
	}

	private IEnumerator PerformRequest(string url, string method) {
		using (UnityWebRequest webRequest = new UnityWebRequest(url, method, new DownloadHandlerBuffer(), null)) {
			yield return webRequest.SendWebRequest();

			if (webRequest.error != null) {
				if (onErrorHandler != null) {
					onErrorHandler.Invoke(webRequest.error);
				}
			} else if (onResponseHandler != null) {
				onResponseHandler.Invoke(webRequest.downloadHandler.text);
			}
		}

		requestCoroutine = null;
	}
}
