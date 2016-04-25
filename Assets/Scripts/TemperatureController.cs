using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TemperatureController : MonoBehaviour {

	[SerializeField]
	private Image tempBarImage = null;
	[SerializeField]
	private RectTransform TempBarTextRectTransform = null;
	private float textWidth = 0;
	private float textXPosRatio = 0;
	[SerializeField]
	private float currentTemp;
	[SerializeField]
	private float maxTemp = 0;
	[SerializeField]
	private float rateOfLoss = 0;
	[SerializeField]
	private float rateOfRecovery = 0;

	private bool heating = false;
	private float timer = 0;
	[SerializeField]
	private float interval = 0;

	private LevelManager levelManager;

	void Start () {
		currentTemp = maxTemp;

		levelManager = FindObjectOfType<LevelManager>();

		textWidth = TempBarTextRectTransform.sizeDelta.x;
		textXPosRatio = -TempBarTextRectTransform.sizeDelta.x / 2;
	}

	void Update () {
		if (timer >= interval) {
			if (heating) {
				currentTemp += rateOfRecovery;
			} else {
				currentTemp -= rateOfLoss;
			}

			UpdateTemperatureBar();
			timer = 0;

			currentTemp = Mathf.Clamp(currentTemp, 0, maxTemp);

			if (currentTemp == 0) {
				Die();
			}
		}

		timer += Time.deltaTime;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (collider.gameObject.tag == "HeatSource") {
			heating = true;
		}
	}

	void OnTriggerExit2D (Collider2D collider) {
		if (collider.gameObject.tag == "HeatSource") {
			heating = false;
		}
	}

	void UpdateTemperatureBar () {
		float fillRatio = currentTemp / maxTemp;

		// fill/empty bar
		tempBarImage.fillAmount = fillRatio;

		// control width of temperature text element
		TempBarTextRectTransform.sizeDelta = new Vector2 (fillRatio * textWidth, TempBarTextRectTransform.sizeDelta.y);
		// control posX of temperature text element
		TempBarTextRectTransform.localPosition = new Vector3 (textXPosRatio * (-fillRatio + 1), TempBarTextRectTransform.localPosition.y, TempBarTextRectTransform.localPosition.z);
	}

	void Die () {
		levelManager.LoadLevel("P5_Freezing");
	}
}
