using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerOnScrollPosition : MonoBehaviour
{
	[Tooltip("Trigger position in percents from the bottom of the list")]
	public int triggerTresholdInPercent;
	public bool resetTriggerOnContentChange = true;
	public UnityEvent OnScrollPosition;

	private float lastPosition;
	private float contentHeight;

	private void Start() {
		ScrollRect scrollRect = GetComponent<ScrollRect>();
		float triggerPositionNormalized = this.triggerTresholdInPercent / 100f;
		scrollRect.onValueChanged.AddListener((Vector2 vector) => {
			float relativePositionNormalized = (scrollRect.content.rect.height * scrollRect.verticalNormalizedPosition) / scrollRect.content.rect.height;

			/*
			 * Trigger OnScrollPosition event if
			 * 1) scroll position of current content height is less than trigger position threshold and
			 * 2) resetTriggerOnContentChange is disabled or content height has changed since last iteration
			 */
			if (relativePositionNormalized < triggerPositionNormalized && (lastPosition > triggerPositionNormalized || resetTriggerOnContentChange && contentHeight != scrollRect.content.rect.height)) {	
				OnScrollPosition.Invoke();
			}

			contentHeight = scrollRect.content.rect.height;
			lastPosition = relativePositionNormalized;
		});

		contentHeight = scrollRect.content.rect.height;
		lastPosition = (scrollRect.content.rect.height * scrollRect.verticalNormalizedPosition) / scrollRect.content.rect.height;
	}
}
