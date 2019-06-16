﻿using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemListManager : MonoBehaviour
{
	public StringProperty squareImageUrlTemplate;
	public GameObject itemTemplate;
	public GameEventJObject itemSelected;

	public void Start() {
		if (itemTemplate.activeSelf) {
			itemTemplate.SetActive(false);
		}
	}

	public void ClearItems() {
		foreach (Transform item in itemTemplate.transform.parent) {
			if (item != itemTemplate.transform) {
				Destroy(item.gameObject);
			}
		}
	}

	public void AddItemsFromJson(string jsonData) {
		dynamic dynamicJsonData = JsonConvert.DeserializeObject(jsonData);

		foreach (dynamic record in dynamicJsonData.data) {
			GameObject item = Instantiate(itemTemplate, itemTemplate.transform.parent);
			item.GetComponentInChildren<TextMeshProUGUI>(true).text = record.title.fi;
			if (record.image != null && record.image.id != null) {
				item.GetComponentInChildren<WWWImage>(true).url = Utils.FillTemplateFromJObject(squareImageUrlTemplate.value, record);
			}
			Button button = item.GetComponentInChildren<Button>();
			button.onClick.AddListener(() => {
				itemSelected.Invoke(record);
			});
			item.SetActive(true);
		}

	}
}
