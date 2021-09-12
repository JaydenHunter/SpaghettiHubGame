//Written by Jayden Hunter
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
	public GameManager gameManager;
	private TextMeshProUGUI tmpMoney;

	private void Awake()
	{
		tmpMoney = GetComponent<TextMeshProUGUI>();
	}
	private void Update()
	{
		tmpMoney.text = $"Money: ${gameManager.money}";
	}
}
