// Airborne.cs
// Author : Fragmads
// Package : Game engine/FighterStates
// Last modification date : 03/09/2012
//
// Airborne : A state describing a fighter in the air
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Airborne : AFighterState {
	
	// Properties
	//
	
	
	public int JumpLeft = 1;
	
	private bool JumpReleased = false;
	
	// Method
	//
	
	public new void Start(){
		
		base.Start();
		
		// Assign the number of double jump left to the fighter
		this.JumpLeft = this.fighter.DoubleJump;
		
	}
	
	// Send the name of this state
	public override string getStateName() {
		return "Airborne";
	}
	
	// Read the command send by the player, and interpret them
	public override void readCommand (InputCommand input ){
		// TODO double jump, air attack, air dodge, air control
		
		// Forcing player to release jump before performing a double jump
		if(!input.Jump){
			this.JumpReleased = true;
		}
		
		// Double jumping
		if(input.Jump && this.JumpLeft > 0 && this.gameObject.GetComponent<Jumping>() == null && this.JumpReleased){
			this.JumpLeft --;
			
			Momentum m = this.gameObject.AddComponent<Momentum>();
			m.strength = this.fighter.DoubleJumpStrength;
			m.reduction = this.fighter.DoubleJumpReduction;
			m.angle = 90;	
		
			
			XMomentum xMom = this.gameObject.GetComponent<XMomentum>();
			xMom.strength = this.fighter.DoubleJumpHorizontalFactor * input.RightStickX;
			
			
			
		}
		
	}	
	
	void FixedUpdate() {
		
		
		
		// Check if the fighter as land on a platform
		this.CheckForPlatforms();
		
	}// End FixedUpdate
	
	
	// Check if the fighter land on a platform
	private void CheckForPlatforms(){
		
		// For every platform in this stage
		foreach(Platform p in Platform.StagePlatforms){
			
			// Check if this fighter has landed
			if (p.CheckIfLanded(this.fighter)){
				
				// Set the fighter height to the platform's
				this.fighter.gameObject.transform.position = new Vector3( this.fighter.gameObject.transform.position.x, p.gameObject.transform.position.y , 0);
				
				// If you are not knocked while you hit the ground
				if(this.gameObject.GetComponent<Knocked>() == null){
					
					// Stop the fighter from falling
					//this.fighter.SpeedY = 0;
					
					Landing landing = this.gameObject.AddComponent<Landing>();
					this.fighter.State = landing;
					this.OnGround(p);
					
					
					
					
				}
				
				// If you are knocked when you hit the ground and do a tech
				else if(this.gameObject.GetComponent<Knocked>().isInTechWindow){
					
					// Stop the fighter from falling
					//this.fighter.SpeedY = 0;
					
					// You are no longer in the air, and not knocked anymore
					Teching teching = this.gameObject.AddComponent<Teching>();
					this.fighter.State = teching;
					
					Object.Destroy(this.gameObject.GetComponent<Knocked>());
					this.OnGround(p);
					
					
				}
				// If you are knocked when you hit the ground and do not tech
				else {
					
					float downStrength = 0;
					// For all the momentum of this fighter
					foreach (Momentum m in this.gameObject.GetComponents<Momentum>()){
						
						downStrength += m.vector.y;
						
					}
					
					// If the fighter is splashed to the ground
					if( downStrength < -(this.fighter.Weight * this.fighter.SplashFactor) ){
						
						// TODO deal damage and make the fighter rebounce
						
					}
					// The fighter is lying on the floor
					else {
						
						// Stop the fighter from falling
						//this.fighter.SpeedY = 0;
						
						Lying lying = this.gameObject.AddComponent<Lying>();
						this.fighter.State = lying;
						this.OnGround(p);
						
						
					}
						
				}
				
				// You can only land on one platform
				break;
				
			}
			
			
			// Ledge grab
			
			float fighterEdgeY;
			float fighterEdgeX;
			
			if(p.LeftEdge != null){
				fighterEdgeY = this.gameObject.transform.position.y - p.LeftEdge.gameObject.transform.position.y;
				fighterEdgeX = this.gameObject.transform.position.x - p.LeftEdge.gameObject.transform.position.x;
				
				//If you can reach the left edge
				if( ((fighterEdgeY < 0) && (fighterEdgeY > -Edge.grabDistanceY)) && ((fighterEdgeX < 0) && (fighterEdgeX > -Edge.grabDistanceX)) ){
					
					
					
					Attacking attacking = this.gameObject.GetComponent<Attacking>();
					// If the fighter is not attacking
					if( attacking == null){
						
						if( this.fighter.isFacingLeft ){
							// TODO grab ledge	
						} 
						
					}
					// If he can grab the ledge
					else if (attacking.Attack.BlindAutoEdgeGrab || (attacking.Attack.AutoEdgeGrab && this.fighter.isFacingLeft)){
						
						// TODO grab ledge
						
					}
					
					
				}
			}
			else if (p.RightEdge != null){
				fighterEdgeY = this.gameObject.transform.position.y - p.RightEdge.gameObject.transform.position.y;
				fighterEdgeX = this.gameObject.transform.position.x - p.RightEdge.gameObject.transform.position.x;
				
				if(((fighterEdgeY < 0) && (fighterEdgeY > -Edge.grabDistanceY)) && ((fighterEdgeX > 0) && (fighterEdgeX < Edge.grabDistanceX))){
					
					Attacking attacking = this.gameObject.GetComponent<Attacking>();
					// If the fighter is not attacking
					if( attacking == null){
						
						if( this.fighter.isFacingRight ){
							// TODO grab ledge	
						} 
						
					}
					// If he can grab the ledge
					else if (attacking.Attack.BlindAutoEdgeGrab || (attacking.Attack.AutoEdgeGrab && this.fighter.isFacingRight)){
						
						// TODO grab ledge
						
					}
					
				}
			}
			
			
		}
		
	}
	
	// Grab an edge
	private void GragEdge(Edge e){
		
		// In case of blind auto edge grab
		if(this.fighter.isFacingLeft != e.isLeft){
			
			this.fighter.TurnAround();
			
		}
		
		// Grab the edge, it will give the fighter invicibility
		e.EdgeGrabbed(this.fighter);
		
		// Stop the fighter movement
		Momentum.Clean(this.fighter);
		
		this.fighter.SpeedX = 0;
		this.fighter.SpeedY = 0;
		
		
	}
	
	private void OnGround(Platform p){
		
		Debug.Log("Airborne : On the ground");
		Debug.Log("SpeedY "+this.fighter.SpeedY);
		
		// You are no longer in the air, and not knocked anymore
		OnGround onGround = this.gameObject.AddComponent<OnGround>();
		onGround.platform = p;
		Object.Destroy(this);
		
	}
	
	
}
