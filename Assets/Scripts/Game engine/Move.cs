// Move.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Move : A move that can be executed by a fighter
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Move : MonoBehaviour {
	
	// Properties
	//
	
	// A list of State from which you can start this move
	public List<AFighterState> StartingStates;
	
	// The State this move will put you in
	public AFighterState State;
	
	// When true, the fighter may grab the ledge 
	public bool AutoEdgeGrab = false;
	// Even when he is not facing the ledge.
	public bool BlindAutoEdgeGrab = false;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
