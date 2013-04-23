// Edge.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 20/09/2012
//
// Edge : describe an edge of a platform
// 
// State : Uncomplete

using UnityEngine;
using System.Collections;

public class Edge : MonoBehaviour {
	
	// The entity grabbing the ledge
	public AEntity grabber = null;
	
	// General distance from where you can grab an edge
	public static float grabDistanceY = 1.0f;
	public static float grabDistanceX = 1.0f;
	
	// Incibility time
	private static float invincibilityBaseTime = 0.5f ;
	private static float invincibilityDecoy = 0.1f;
	
	// Properties
	//
	
	public bool isLeft;
	public bool isRight;
	
	// The next time of invincibility this edge will provide to whoever grab it
	public float InvincibilityTime = Edge.invincibilityBaseTime;
	
	// Method
	//
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		// If the invincibility time as decreased
		if (this.InvincibilityTime < Edge.invincibilityBaseTime) {
			// Add time for the next invincibility this edge provide
			this.InvincibilityTime += Time.deltaTime;
		} 
		// Else, if the invincibility time is too great
		else if (this.InvincibilityTime > Edge.invincibilityBaseTime) {
			// Set it to base value
			this.InvincibilityTime = Edge.invincibilityBaseTime;
		} 
				
	}
	
	// The edge have been grabbed
	public void EdgeGrabbed(AEntity grabber){
		
		this.InvincibilityTime -= Edge.invincibilityDecoy;
		
		// Can't get negative value
		if(this.InvincibilityTime <0){
			this.InvincibilityTime = 0;
		}
		
		this.grabber = grabber;
		
	}
	
	
}
