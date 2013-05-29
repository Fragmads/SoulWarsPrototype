// UselessStance.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 04/09/2012
//
// UselessStance : A state describing a fighter falling after an aerial special. He can influence his direction, but not attack or dodge.
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UselessStance : AFighterState {

	
	// Send the name of this state
	public override string getStateName() {
		return "UselessStance";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input){
		
		// You can only AirControl in UselessStance
		if(this.fighter.gameObject.GetComponent<Airborne>() != null){
			
			this.fighter.gameObject.GetComponent<Airborne>().AirControl(input);
			
		}		
		
		
	}
	
}
