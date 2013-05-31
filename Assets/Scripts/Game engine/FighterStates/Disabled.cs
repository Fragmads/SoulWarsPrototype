// Disabled.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 04/09/2012
//
// Disabled : A state describing a fighter disabled (ex : being grabbed)
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Disabled : AFighterState {
	
	private static float StuckBug = 10f;
	
	private float disabledTime = 0;
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		// Clean all previous State
		foreach(AFighterState af in this.fighter.gameObject.GetComponents<AFighterState>()){
			
			if(! (af is Disabled)){
				GameObject.Destroy(af);
			}			
		}
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Disabled";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Do nothing
			
	}	
	
	public void FixedUpdate(){
		
		this.disabledTime += Time.deltaTime;
		
		// In case of stuck bug
		if(this.disabledTime > Disabled.StuckBug ){
			
			// Add a life, and kill this player
			this.fighter.Player.LifeRemaining ++;
			this.fighter.Player.LooseLife();
			
		}
		
	}
	
	
}
