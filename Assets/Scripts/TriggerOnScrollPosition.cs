using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerOnScrollPosition : MonoBehaviour
{
	[Tooltip("Trigger position in percent from the bottom of the list")]
	public int triggerTresholdInPercent;
	public bool resetTriggerOnContentChange = true;
	public UnityEvent OnScrollPosition;

	private float lastPosition;
	private float contentHeight;

	private void Start() {
		ScrollRect scrollRect = GetComponent<ScrollRect>();
		float triggerPosition = this.triggerTresholdInPercent / 100f;
		scrollRect.onValueChanged.AddListener((Vector2 vector) => {
			float relativePosition = (scrollRect.content.rect.height * scrollRect.verticalNormalizedPosition) / scrollRect.content.rect.height;

			if (relativePosition < triggerPosition && (lastPosition > triggerPosition || resetTriggerOnContentChange && contentHeight != scrollRect.content.rect.height)) {	
				OnScrollPosition.Invoke();
			}

			contentHeight = scrollRect.content.rect.height;
			lastPosition = relativePosition;
		});

		contentHeight = scrollRect.content.rect.height;
		lastPosition = (scrollRect.content.rect.height * scrollRect.verticalNormalizedPosition) / scrollRect.content.rect.height;
	}
}
