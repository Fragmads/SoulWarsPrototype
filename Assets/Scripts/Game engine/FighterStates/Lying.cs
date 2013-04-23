// Lying.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// Move : A move that can be executed by a fighter
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Lying : AFighterState {
	
	// Platform the fighter is on
	public Platform platform;
	
	public float LyingMaxTime = 5f;
	
	// Method
	//
	
	// Send the name of this state
	public override string getStateName() {
		return "Lying";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO Standing, rolling, rising attack
			
	}	
	
	
	void FixedUpdate(){
		
		this.LyingMaxTime -= Time.fixedDeltaTime;
		
		// If the fighter is lying for too long.
		if(this.LyingMaxTime <= 0){
			
			// TODO get up automatically
			
			this.LyingMaxTime = 5f;
			
		}
		
	}
	
	
}
