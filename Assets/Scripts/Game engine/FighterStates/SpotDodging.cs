// SpotDodging.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// SpotDodging : A state describing a fighter dodging attacks
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class SpotDodging : AFighterState {

	// Method
	//
	
	private float animationLength = 1f;
	
	private float dodgeStart = 0.2f;
	private float dodgeEnd = 0.8f;
		
	private float dodgeTime = 0f;
	
	private bool GuardInput = false;
	
	public new void Start(){
		
		base.Start();
		
		// Play the dodging animation
		this.gameObject.animation.Play("dodging", PlayMode.StopAll);	
		
		this.animationLength = this.fighter.SpotDodgeLength;
		this.dodgeStart = this.fighter.SpotDodgeStart;
		this.dodgeEnd = this.fighter.SpotDodgeEnd;
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Dodging";
	}
		
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// Do nothing
			
		// Report the last guard input
		this.GuardInput = input.Guard;
		
	}	
	
	public void FixedUpdate(){
		
		// Increase the dodge time
		this.dodgeTime += Time.fixedDeltaTime;
		
		// If it's the end of the spotdodge
		if(this.dodgeTime >= this.animationLength ){
			
			if(this.GuardInput){
				// Fighter go back to guard
				this.fighter.InvincibilityTime = 0;
				Guarding guarding = this.fighter.gameObject.AddComponent<Guarding>();
				this.fighter.State = guarding;
				GameObject.Destroy(this);
			}
			else {
				// Fighter go back to a standing position
				this.fighter.InvincibilityTime = 0;
				Standing standing = this.fighter.gameObject.AddComponent<Standing>();
				this.fighter.State = standing;
				GameObject.Destroy(this);
			}
		}
		
		if(this.dodgeTime > this.dodgeStart && this.fighter.InvincibilityTime <=0 && this.dodgeTime < this.dodgeEnd){
			
			this.fighter.InvincibilityTime = this.dodgeEnd - this.dodgeTime;
			
		}
		
		if(this.dodgeTime > this.dodgeEnd){
			this.fighter.InvincibilityTime = 0;
		}
		
	}
	
	
}
