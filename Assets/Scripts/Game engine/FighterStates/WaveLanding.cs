// WaveLanding.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 29/05/2013
//
// WaveLanding : A state describing a fighter landing on the ground after performing an air dodge
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class WaveLanding : AFighterState {
	
	// Side of the WaveLand
	public bool isLeft;
	public bool isRight;
	
	// Remaining strength of the WaveLand
	public float strength;
	
	// Time before the end of the AirDodge/WaveLand
	public float length;
	
	// Send the name of this state
	public override string getStateName() {
		return "WaveLanding";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// Nothing to do
		
	}
	
	public void FixedUpdate(){
		
		// Reduce the WaveLanding time
		this.length -= Time.fixedDeltaTime;
		
		if(this.length <= 0){
			
			// TODO go to a Standing position
			Standing standing = this.fighter.gameObject.AddComponent<Standing>();
			this.fighter.State = standing;
			
			GameObject.Destroy(this);
			
		}
		
	}
	
}
