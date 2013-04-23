// Standing.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Standing : A state describing a fighter standing still
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Standing : AFighterState {
	
	// Platform the fighter is on
	public Platform platform;
	
	// Method
	//
	
	
	public new void Start () {
		
		base.Start();
		
		// Stop the speed when you are walking
		XMomentum groundMomentum = this.gameObject.GetComponent<XMomentum>();
		
		if(groundMomentum != null){
			groundMomentum.strength = 0;
		}
		
		/*
		// Start the standing animation
		this.gameObject.animation.wrapMode = WrapMode.Loop;
		//this.gameObject.animation.CrossFade("Standing", 0f, PlayMode.StopAll);
		this.gameObject.animation.CrossFade("idle", 0f, PlayMode.StopAll);
		*/
		
		
	}
	
	
	// Send the name of this state
	public override string getStateName() {
		return "Standing";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO run, jump, guard, attack, walking, Dashing
				
		// Walking
		if (!input.RightStickDash && input.RightStickX != 0){
			
			Walking walking = this.gameObject.AddComponent<Walking>();
			this.fighter.State = walking;
			walking.platform = this.platform;
			Object.Destroy(this);
			
		}
		// Dashing
		else if(input.RightStickDash && input.RightStickX != 0){
			
			Dashing dashing = this.gameObject.AddComponent<Dashing>();
			this.fighter.State = dashing;
			dashing.platform = this.platform;
			Object.Destroy(this);
			
		}
		
		// Jumping
		else if(input.Jump){
			
			Jumping jumping = this.gameObject.AddComponent<Jumping>();
			this.fighter.State = jumping;
			Object.Destroy(this);
			
		}
		
	}	
	
	public void FixedUpdate(){
		
		// When standing, a fighter Horizontal speed tend to quickly reduce to 0
		if(this.fighter.SpeedX != 0){
			this.fighter.SpeedX = this.fighter.SpeedX/(this.fighter.GroundFriction);
			
			// If the speed is low enough, consider it's 0
			if(Mathf.Abs(this.fighter.SpeedX) < 1){
				this.fighter.SpeedX = 0;
			} 
			
		}
		
		/*
		// Prevent the fighter from falling under the platform
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, this.platform.gameObject.transform.position.y, 0 );
		this.fighter.SpeedY = 0;
		*/
		
	}
	
}