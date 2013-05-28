// KnockBack.cs
// Author : Fragmads
// Package : Physic engine
// Last modification date : 21/05/2013
//
// KnockBack : Describe a force given by a hit
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class KnockBack : MonoBehaviour {
	
	// angle in degree
	public float angle;
	
	// Strength of the knockback
	public float strength;
	
	// Length of the knockback, in second
	public float length;
	
	
	public Vector2 vector = new Vector2(0,0);
	
	// Use this for initialization
	void Start () {
		this.RecalculateVector();
	}
	
	// Update is called once per frame
	public void FixedUpdate () {
		
		// If this Knockback is not in a move dummy
		if(this.gameObject.GetComponent<Move>() == null){
		
			// Reduce the time since this KnockBack exist
			this.length -= Time.fixedDeltaTime;
			
			// If the Knockback end
			if(this.length <= 0){
				
				GameObject.Destroy(this);
				
			}
		}
		
	}
	
	// Recompute the vector creation
	public void RecalculateVector(){
		
		this.vector = new Vector2( Mathf.Cos(this.angle * Mathf.Deg2Rad) * this.strength, Mathf.Sin(this.angle * Mathf.Deg2Rad) * this.strength );
		
	}
	
}
