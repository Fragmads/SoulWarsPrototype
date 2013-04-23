// Attacking.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Attacking : A state describing a fighter attacking
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Attacking : AFighterState {

	// Properties
	//
	
	public Move Attack;
	
	// The length of this attack in second
	public int Length;
	
	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "Attacking";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO go to next attack, Air control
				
	}	
	
	
}
