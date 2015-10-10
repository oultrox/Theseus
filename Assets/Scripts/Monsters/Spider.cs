﻿using UnityEngine;
using System.Collections;

public class Spider : Monster {

	[Tooltip ("Czas oczekiwania przed rozpoczęciem ataku ( w sekundach )")]
	[SerializeField]
	private long _spiderAttackDelay;
	[SerializeField]
	private long _spiderSleepMode;

	private bool _readyToAttack = false;
	private double _timeToAttack, _timeToWait;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		_Rig2D = GetComponent<Rigidbody2D>();
		_axis = new Vector2(0, 0);
	}

	public override void Attack()
	{
		//atak
	}

	public override void Chase()
	{
		if (_readyToAttack) {

			if(Time.time >= _timeToAttack)
			{

				Debug.Log ("Atakuje !!!!");
				_axis = _targetToAttack.position - transform.position;
				if (_axis.x < 0)
					_axis.x = -1;
				else
					_axis.x = 1;
				if (_axis.y < 0)
					_axis.y = -1;
				else
					_axis.y = 1;
				_Rig2D.AddForce(_axis * _maxSpeed);
				if(Time.time >= _timeToWait)
				{
					_readyToAttack = false;
					_Rig2D.MovePosition (_Rig2D.position);
				}
			}
		} else {
			Debug.Log ("Szykuje atak !");
			_timeToAttack = Time.time + _spiderAttackDelay;
			_timeToWait = _timeToAttack + _spiderSleepMode;
			_readyToAttack = true;
		}
	}

	public override void Walking()
	{
		_readyToAttack = false;
		_Rig2D.MovePosition (_Rig2D.position);
	}
}