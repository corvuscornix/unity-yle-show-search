using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine;

public class JSONQuery : MonoBehaviour
{
	public StringProperty property;
	public bool replacePlaceholders = false;
	public UnityStringEvent resultHandler;

	/*
	 * Reads a given JSON property (uses JPath syntax) from input data or if replacePlaceHolders is enabled, replaces all properties inside square brackets.
	 */
	public void Run(dynamic data) {

		if (data is JObject && resultHandler != null) {
			string result;
			if (replacePlaceholders) {
				result = Utils.FillTemplateFromJObject(property.value, data);
			} else {
				result = (string)data.SelectToken(property.value);
			}

			resultHandler.Invoke(result);
		}
	}
}