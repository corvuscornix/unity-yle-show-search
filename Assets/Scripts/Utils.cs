using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Utils : MonoBehaviour
{

	/*
	 * Replace all the placeholders (text within square brackets) with matching properties from incoming data.
	 */
	internal static string FillTemplateFromJObject(string fillTemplate, JObject data, bool nullIfNoValue = false) {

		string pattern = @"\[[\w\.\[\]]*\]";
		Regex rgx = new Regex(pattern);
		string result = fillTemplate;
		foreach (Match match in rgx.Matches(fillTemplate)) {
			string value = (string)data.SelectToken(match.Value.Substring(1, match.Value.Length - 2));
			if (nullIfNoValue && value == null) return null;
			result = result.Replace(match.Value, value);
		}

		return result;
	}
}
