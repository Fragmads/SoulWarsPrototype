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
		
		// TODO : Calculate applied momentum
		this.ApplyMomentum();
		
		
		// Move the entity to it's new position
		this.transform.position = new Vector3 (this.transform.position.x + this.SpeedX/60, this.transform.position.y + this.SpeedY/60, 0);
		
		
	}
	
	public void ApplyMomentum(){
		
		float x = 0;
		float y = 0;
		
		// For every momentum attached to this entity
		foreach (Momentum m in this.gameObject.GetComponents<Momentum>()){
			
			// Add the strength
			x += m.vector.x;
			y += m.vector.y;
			
		}
		
		// Apply it to the actual speed of the entity
		
		this.SpeedX = x;
		this.SpeedY = y;
		
	}
	
	
}
