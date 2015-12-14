﻿using UnityEngine;

public class HpPotion : Item {

	/// <summary>
	/// 	Metoda uruchamiana podczas utworzenia mikstury
	/// </summary>
	public override void Start ()
	{
		base.Start ();
	}

	/// <summary>
	/// 	Metoda zawierająca efekt działania przedmiotu.
	/// </summary>
	/// <remarks>
	/// 	W tym przypadku, odnawiamy życie gracza o określoną ilość.
	/// </remarks>
	public override void EffectOfItem(Collision2D other)
	{
		Debug.Log ("Działam !");
	}
}
