// Momentum.cs
// Author : Fragmads
// Package : Physic engine
// Last modification date : 17/04/2013
//
// Momentum : Describe a force that give a movement to an entity
// 
// State : Uncomplete


using UnityEngine;
using System.Collections;


public class Momentum : MonoBehaviour {
	
	// 
	public Vector2 vector = new Vector2(0,0);
	
	// Strength of the momentum in unity meter per second
	public float strength = 10f;
	
	// Angle of the momentum in degree
	public float angle = 0f;
	
	// Momentum reduction in unity meter per second lost every second
	public float reduction = 1.0f;
	
	
	// Use this for initialization
	void Start () {
		
		this.RecalculateVector();
		
	}
	
	// FixedUpdate is called once per game frame
	public void FixedUpdate () {
		
		// If the momentum is attached to an entity
		if(this.gameObject.GetComponent<AEntity>() != null){
		
			// Decrease the strength of the momentum
			
			this.strength -= this.reduction/60;
			
			// If the momentum has stopped, doesnt apply to the X momentum or gravity
			if(this.strength <= 0 && !((this is XMomentum)||(this is GravityMomentum))){
				
				Object.Destroy(this);
				this.strength = 0;
				this.reduction = 0;
				this.vector = new Vector2 (0,0);
			}
			else {		
			
				this.RecalculateVector();
				
			}
		}
	}
	
	// Recompute the vector creation
	public void RecalculateVector(){
		
		this.vector = new Vector2( Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.strength, Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.strength );
		
	}
	
	// Free an entity from all it's momentum
	public static void Clean(AEntity e){
		
		foreach(Momentum m in e.gameObject.GetComponents<Momentum>()){
			
			// Destroy the momentum except for the ground momentum
			if(!(m is XMomentum)){
				Object.Destroy(m);
			}
			
		}
		
	}
	
}
