// Throw.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 31/05/2013
//
// Throw : A cinematic where a fighter execute a throw on another
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Throw : MonoBehaviour {
	
	// Properties
	//
	
	public string animationName = "throw";
	
	public float animationLength = 1f;
	public float ReleaseTime = 0.5f;
	
	// String used to describe the game object used in the throw animation to determine the position of the fighter being thrown
	public string ennemyPosition = "thrown_01";
	
	// Base Damage of the throw
	public int damage = 10;
	
	// Base knock back of the throw
	public KnockBack baseKnockBack;
	
	
	private float time = 0f;
	
	// Method
	//
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	
}
