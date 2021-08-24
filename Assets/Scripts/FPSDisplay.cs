using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
	public TextMeshProUGUI fpsDisplay;
	private float deltaTime;
	private void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fpsDisplay.text = $"FPS: {Mathf.CeilToInt(1.0f/deltaTime)}";
	}
}
