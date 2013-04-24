// Platform.cs
// Author : Fragmads
// Package : Game engine
// Last modification date : 20/09/2012
//
// Platform : describe a platform on which a character can land 
// 
// State : Uncomplete


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : MonoBehaviour {
	
	public static List<Platform> StagePlatforms;
	
	// Properties
	//
	
	
	public float Length;
	
	public bool CanDrop = false;
	
	// Edges of this platform
	public Edge RightEdge = null;
	
	public Edge LeftEdge = null;
	
	
	// Method
	//
	
	
	// Use this for initialization
	void Start () {
		
		// Initialize the edge
		if(this.RightEdge != null){
			this.RightEdge.platform = this;
			
			// Place the edge
			this.RightEdge.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + (this.Length/2), this.gameObject.transform.position.y, 0);
			
			this.RightEdge.isRight = true;
			this.RightEdge.isLeft = false;
		}
		
		if(this.LeftEdge != null){
			this.LeftEdge.platform = this;
			
			// Place the edge
			this.LeftEdge.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - (this.Length/2), this.gameObject.transform.position.y, 0);
			
			this.LeftEdge.isLeft = true;
			this.LeftEdge.isRight = false;
			
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool CheckIfLanded(AEntity entity){
		
		
		// If the fighter is above, and will land in the next update
		if ((entity.gameObject.transform.position.y > this.gameObject.transform.position.y) && (entity.SpeedY/60 < -(entity.gameObject.transform.position.y - this.gameObject.transform.position.y)) ) {
			
			
			if((entity.gameObject.transform.position.x <= (this.gameObject.transform.position.x + this.Length/2)) && (entity.gameObject.transform.position.x >= (this.gameObject.transform.position.x - this.Length/2)) ) {
			
				Debug.Log("Platform.CheckIfLanded : Landing");
				
				return true;
			}
		}
				
		return false;
	}
	
}
