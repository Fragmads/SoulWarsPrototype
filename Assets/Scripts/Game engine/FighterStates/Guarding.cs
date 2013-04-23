// Guarding.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Move : A state describing a fighter who have raised his guard
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Guarding : AFighterState {

	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "Guarding";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO can grab, jump, dodge, or go back to standing
		
		// Guard to stand
		if(!input.Guard){
			
			// He stand without guard
			this.fighter.State = this.gameObject.AddComponent<Standing>();
			
			Object.Destroy(this);
			
		}
		
	}	
	
}
