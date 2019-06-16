using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Utils : MonoBehaviour
{

	public void DebugPrint(Vector2 vector) {
		Debug.Log("scrolling: " + vector);
	}

	/*
	 * Replace all the placeholders (text within square brackets) with matching properties from incoming data.
	 * MIGHT also work for template strings that contain JPath with square brackets like "id=[my.json.values[0].value]" 
	 */
	internal static string FillTemplateFromJObject(string fillTemplate, JObject data) {

		string pattern = @"\[[\w\.\[\]]*\]";
		Regex rgx = new Regex(pattern);
		string result = fillTemplate;
		foreach (Match match in rgx.Matches(fillTemplate)) {
			result = result.Replace(match.Value, (string)data.SelectToken(match.Value.Substring(1, match.Value.Length - 2)));
		}

		return result;
	}
}
