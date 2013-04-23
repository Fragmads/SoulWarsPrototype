// HitStun.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Move : A state that describe a fighter getting hit by an attack
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class HitStun : AFighterState {

	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "HitStun";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO SDI's
			
	}	
	
}
