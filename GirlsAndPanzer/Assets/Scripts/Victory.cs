using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour
{
	public GameObject victoryUI;
	public GameObject escapeMenu;

	public void Start()
	{
		TimeSet(1);
	}

	private void OnEnable()
	{
		Enemy.onEnemyDeath += CallUi;
		Cursor.lockState = CursorLockMode.None;
	}

	private void OnDisable()
	{
		Enemy.onEnemyDeath -= CallUi;
	}

	public void CallUi()
	{
		victoryUI.SetActive(true);
		Cursor.lockState = CursorLockMode.None;
		//TimeSet(0);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			escapeMenu.SetActive(true);
			TimeSet(0);
			Cursor.lockState = CursorLockMode.None;
		}

		if (!escapeMenu.activeSelf)
		{
			Cursor.lockState = CursorLockMode.Locked;
			TimeSet(1);
		}
		else
		{
			Cursor.lockState = CursorLockMode.None;
		}
	}

	public void TimeSet(int condition)
	{
		switch(condition)
		{
			case 1: Time.timeScale = 1;
				break;
			case 0: Time.timeScale = 0;
				break;
		}
	}
}
