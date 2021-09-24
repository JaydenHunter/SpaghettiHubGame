//Written by Jayden Hunter

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used for loading the Main Scene,Black Cat Scene or Main Menu.
/// Handles automatic saving depending on the scene
/// </summary>
public class SceneLoader : MonoBehaviour
{
	private GameObject gameManager;			//Reference to the game manager
	private SaveManager saveManager;		//Reference to the save manager
	private string sceneNameToLoad = "";	//The scene name in which to load

	private void Awake()
	{
		gameManager = GameObject.Find("GameManager");
		saveManager = gameManager.GetComponent<SaveManager>();
	}

	/// <summary>
	/// Loads the BlackJack minigame and saves cat's data and money data
	/// </summary>
	public void LoadBlackJackGame()
	{
		saveManager.Save();
		sceneNameToLoad = "TomMiniGame";
		StartCoroutine(LoadScene());
	}

	/// <summary>
	/// Loads the Main Cat Scene while saving the money data
	/// </summary>
	public void LoadMainScene()
	{
		saveManager.SaveBlackJack();
		sceneNameToLoad = "Main";
		StartCoroutine(LoadScene());
	}

	/// <summary>
	/// Loads the Main Menu from the Cat Scene
	/// </summary>
	public void LoadMainMenu()
	{
		saveManager.Save();
		sceneNameToLoad = "Main Menu";
		StartCoroutine(LoadScene());
	}

	/// <summary>
	/// Loads in the given scene name
	/// </summary>
	/// <returns></returns>
	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(0.01f);
		SceneManager.LoadScene(sceneNameToLoad);
	}
}
