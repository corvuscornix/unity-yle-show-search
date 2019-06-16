using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class JSONQuery : MonoBehaviour
{
	public StringProperty property;
	public bool replacePlaceholders = false;
	public UnityStringEvent resultHandler;
	public void Run(dynamic data) {

		if (data is JObject && resultHandler != null) {
			string result;
			if (replacePlaceholders) {
				result = Utils.FillTemplateFromJObject(property.value, data);
			} else {
				Debug.Log(property.value);
				result = (string)data.SelectToken(property.value);
			}

			resultHandler.Invoke(result);
		}
	}
}