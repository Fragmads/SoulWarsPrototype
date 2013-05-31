// Grabbing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 31/05/2013
//
// Grabbing : A state describing a fighter attempting to grab an opponent
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Grabbing : AFighterState {
	
	
	
	
	// Type of grab being perform
	public bool isAttack = false;
	public bool isSpecial = false;
	
	// Use this for initialization
	public new void Start () {
		
		base.Start();
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Grabbing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Do nothing
			
	}
	
	public void FixedUpdate(){
		
		
		
	}
	
	public void OpponentCatched(Fighter Opponent){
		
		
		
	}
	
}
