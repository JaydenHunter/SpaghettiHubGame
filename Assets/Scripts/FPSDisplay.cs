//Written by Jayden Hunter
using TMPro;
using UnityEngine;

/// <summary>
/// Used to display the current FPS
/// </summary>
public class FPSDisplay : MonoBehaviour
{
	public TextMeshProUGUI fpsDisplay; //Reference to the text display

	private float deltaTime;

	private void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		fpsDisplay.text = $"FPS: {Mathf.CeilToInt(1.0f/deltaTime)}";
	}
}
