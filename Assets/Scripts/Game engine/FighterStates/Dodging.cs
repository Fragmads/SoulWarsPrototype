// Dodging.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Dodging : A state describing a fighter dodging attacks
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Dodging : AFighterState {

	// Method
	//
	
	
	public new void Start(){
		
		base.Start();
		
		// Play the dodging animation
		this.gameObject.animation.Play("dodging", PlayMode.StopAll);	
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Dodging";
	}
		
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Do nothing
			
	}	
	
	
}
