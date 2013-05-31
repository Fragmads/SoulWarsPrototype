// Throwing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 31/05/2013
//
// Throwing : A state describing a fighter Throwing a foe
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Throwing : AFighterState {
	
	// Properties
	//
	
	public Fighter Opponent;
	
	public Move Throw;
	
	public float ReleaseTime = 0.5f;
	
	private float time = 0f;
	
	// Method
	//
	
	// Use this for initialization
	public new void Start () {
		base.Start();
		
		// Disable the opponent
		Disabled disabled = this.Opponent.gameObject.AddComponent<Disabled>();
		this.Opponent.State = disabled;
				
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Throwing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		
		// Nothing to do
		
	}
	
	public void FixedUpdate(){
		
		// TODO
		this.time += Time.fixedDeltaTime;
		
		if(this.time >= this.ReleaseTime){
			
			
			
		}
		
		
	}
	
	
}
