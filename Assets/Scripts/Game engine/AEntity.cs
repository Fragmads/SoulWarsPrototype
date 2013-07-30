// AEntity.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 03/09/2012
//
// AEntity : an entity is an actor of the game which may be moved
// 
// State : Uncomplete



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AEntity : MonoBehaviour {
	
		
	// Move Speed on x and y axis
	public float SpeedX = 0f;
	public float SpeedY = 0f;
	
	
	public void FixedUpdate(){
		
		// If the entity don't get any knockback
		if(this.gameObject.GetComponent<KnockBack>() == null){
			// Calculate applied momentum
			this.ApplyMomentum();
		}
		else {
			this.ApplyKnockBack();
		}
		
		
		// Move the entity to it's new position
		this.transform.position = new Vector3 (this.transform.position.x + this.SpeedX/60, this.transform.position.y + this.SpeedY/60, 0);
		
		
	}
	
	public void ApplyMomentum(){
		
		float x = 0;
		float y = 0;
		
		// For every momentum attached to this entity
		foreach (Momentum m in this.gameObject.GetComponents<Momentum>()){
			
			// In the case of the gravity
			if(m is GravityMomentum){
				
				// You apply it only if the entity is not jumping
				if(this.gameObject.GetComponent<JumpMomentum>() == null){
					// Add the strength
					x += m.vector.x;
					y += m.vector.y;
				}
				
			}
			else {
				// Add the strength
				x += m.vector.x;
				y += m.vector.y;
			}
		}
		
		// Apply it to the actual speed of the entity
		
		this.SpeedX = x;
		this.SpeedY = y;
		
	}
	
	public void ApplyKnockBack(){
		
		float x = 0;
		float y = 0;
		
		// For every momentum attached to this entity
		foreach (KnockBack k in this.gameObject.GetComponents<KnockBack>()){
			
			// If you are no longer in Hit Lag
			if(k.HitLags < 0){
			
				// Add the strength
				x += k.vector.x;
				y += k.vector.y;
			}
			else{
				// Decrease the hit lag by one
				k.HitLags --;
				
			}
			
		}
		
		// Apply it to the actual speed of the entity
		
		this.SpeedX = x;
		this.SpeedY = y;
		
	}
	
}
