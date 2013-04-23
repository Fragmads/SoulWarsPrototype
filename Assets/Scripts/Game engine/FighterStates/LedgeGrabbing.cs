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

public class LedgeGrabbing : AFighterState {
	
	public Edge edge;
	
	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "LedgeGrabbing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO dropping the ledge, jumping from it, rolling in, standing, rising attacks
			
	}	
	
}
