// AFighterState.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// AFighterState : A state that a Fighter can enter
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public abstract class AFighterState : MonoBehaviour {
	
	
	// Properties
	//
	
	public Fighter fighter;
	
	// Method
	//
	
	// Use this for initialization
	public void Start () {
		
		this.fighter = this.gameObject.GetComponent<Fighter>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
						
		
	}
	
	// Send the name of the current state
	public abstract string getStateName();
	
	// Read the command send by the player, and interpret them
	public abstract void readCommand (InputCommand input );	
	
}
