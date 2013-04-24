// Attack.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 24/04/2013
//
// Move : An Attack inside a fighter move, there can be several attack per move
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack : MonoBehaviour {
	
	// Properties
	//
	
	public int Damage = 10;

	// Beginning and end of this attack in frame
	public int StartFrame;
	public int EndFrame;
	
	
	// List of the hiboxes this attack will use
	public List<GameObject> Hitbox;
	
	// List of target that this attack has hit
	public List<AEntity> TargetHit;
	
	// Use this for initialization
	void Start () {
	
	}
}
