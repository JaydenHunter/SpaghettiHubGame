//Written by Jayden Hunter

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
	private GameObject gameManager;
	private GameManager gameManagerScript;
	private SaveManager saveManager;
	private string sceneNameToLoad = "";
	private void Awake()
	{
		gameManager = GameObject.Find("GameManager");
		gameManagerScript = gameManager.GetComponent<GameManager>();
		saveManager = gameManager.GetComponent<SaveManager>();
	}

	public void LoadBlackJackGame()
	{
		saveManager.Save();
		sceneNameToLoad="TomMiniGame";
		StartCoroutine(LoadScene());
	}

	public void LoadMainScene()
	{
		saveManager.SaveBlackJack();
		sceneNameToLoad = "Main";
		StartCoroutine(LoadScene());
	}

	public void LoadMainMenu()
	{
		saveManager.Save();
		sceneNameToLoad = "Main Menu";
		StartCoroutine(LoadScene());
	}
	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(0.01f);
		SceneManager.LoadScene(sceneNameToLoad);
	}
}
