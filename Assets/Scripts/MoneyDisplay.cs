//Written by Jayden Hunter

using TMPro;
using UnityEngine;

/// <summary>
/// Used to display the money the player currently has
/// </summary>
public class MoneyDisplay : MonoBehaviour
{
	public GameManager gameManager; //Reference to the GameManager script
	private TextMeshProUGUI tmpMoney;  //Reference to the text display

	private void Awake()
	{
		tmpMoney = GetComponent<TextMeshProUGUI>();
	}

	private void Update()
	{
		//Updates Text Display to show current money
		tmpMoney.text = $"Money: ${gameManager.money}";
	}
}
