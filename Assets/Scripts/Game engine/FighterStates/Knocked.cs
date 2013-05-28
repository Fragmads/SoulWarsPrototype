// Knocked.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Move : A state that describe a fighter flying away after an attack
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Knocked : AFighterState {
	
	// Destun by right/left input in second
	private static float DestunFactor = 0.05f;
	
	// In second
	public float KnockTime;
	public float PureKnockTime;
	
	public KnockBack knockBack;
	
	private float LastRStickX = 0;
	
	public bool isInTechWindow =false;
	
	// Method
	//
	
	new void Start(){
		
		base.Start();
		
		
		if(this.KnockTime < this.PureKnockTime){
			
			this.KnockTime = this.PureKnockTime;
			
		}
		
		// play the knocked animation
		this.gameObject.animation.Play("knocked", PlayMode.StopAll);
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Knocked";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO DI's, 
		
		this.isInTechWindow = input.TechWindow;
		
		// If the player destun
		if( this.PureKnockTime <= 0 && (((this.LastRStickX > 0.9) && (input.LeftStickX < -0.9)) || ((this.LastRStickX < -0.9) && (input.LeftStickX > 0.9))) ){
			
			this.KnockTime -= Knocked.DestunFactor;
			this.LastRStickX = input.LeftStickX;
			
		}
		
	}
	
	void FixedUpdate(){
		
		// Decrease knocked time
		this.KnockTime -= Time.deltaTime;
		this.PureKnockTime -= Time.deltaTime;
		
		// If there is no more KnockTime
		if(this.KnockTime < 0){
			// You are not Knocked anymore
			this.EndKnocked();			
		}
		
	}
	
	public void EndKnocked(){
		
		this.KnockTime = 0;
		this.PureKnockTime = 0;
		GameObject.Destroy(this.knockBack);
		// TODO transform the remaining KnockBack into a Momentum
		GameObject.Destroy(this);
		
		// If you are airborne when the knockBack End
		if(this.fighter.gameObject.GetComponent<Airborne>() != null){
			
			this.fighter.State = this.fighter.gameObject.GetComponent<Airborne>();
			
			// Play the airborne animation
			this.gameObject.animation.Play("airborne");		
		}
		
	}
	
}
