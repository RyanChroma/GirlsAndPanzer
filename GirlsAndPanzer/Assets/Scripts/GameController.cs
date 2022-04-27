using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Game")]
    public Player player;

	[Header("UI")]
	public Text healthText;
    public Text ammoText;
	private void Update()
	{
		healthText.text = "Health: " + player.health;
		ammoText.text = "Bullets: " + player.Ammo;
	}
}