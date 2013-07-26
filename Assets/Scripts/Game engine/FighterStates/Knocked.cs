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
		
		// If you have a KnockBack applying on you
		if(this.knockBack != null && (input.LeftStickX != 0 || input.LeftStickY != 0)){
			
			// Get the LStick angle
			float x = input.LeftStickX;
			float y = input.LeftStickY;
				
			float angleStick = Vector2.Angle( new Vector2(1,0), new Vector2(x,y));
			// Vector2.Angle always return a 180° pos angle, so this is needed when angle > 180
			if(y < 0){
				angleStick = 360 - angleStick;
			}
			
			
			
			
			
			float DIAngle = Mathf.Sin( Mathf.Deg2Rad * ( angleStick  - this.knockBack.angle)) * this.knockBack.DIFactor * Vector2.Distance(Vector2.zero, new Vector2(x,y));
			
			// Calculate a new angle for the Knockback, after the DI
			this.knockBack.angle += DIAngle /60;
			
			Debug.Log("Knocked.readCommand - DI angleStick = "+angleStick+" DI angle = "+DIAngle+" Kb.angle = "+this.knockBack.angle);
		
		}
		
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
