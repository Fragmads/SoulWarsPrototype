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
	public float LyingMaxTime = 5f;
	
	// Method
	//
	
	
	public new void Start(){
		
		base.Start ();
		
		// play the lying animation
		//this.gameObject.animation.Play("lying", PlayMode.StopAll);
		
		// Stop Movement when you are lying on the floor
		Momentum.Clean(this.fighter);
		
		this.gameObject.GetComponent<XMomentum>().strength = 0;
		
		// play the lying animation
		this.fighter.SetAnimationSpeed("lying");
		this.gameObject.animation.Play("lying", PlayMode.StopAll);
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Lying";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO Standing, rolling, rising attack
			
	}	
	
	
	public void FixedUpdate(){
				
		this.LyingMaxTime -= Time.fixedDeltaTime;
		
		// If the fighter is lying for too long.
		if(this.LyingMaxTime <= 0){
			
			
			// The fighter get up automatically
			Standing s = this.gameObject.AddComponent<Standing>();
			this.fighter.State = s;
			
			GameObject.Destroy(this);
			
		}
		
	}
	
	
}
