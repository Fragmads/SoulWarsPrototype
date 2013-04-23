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
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public bool CheckIfLanded(AEntity entity){
		
		
		// If the fighter is above, and will land in the next update
		if ((entity.gameObject.transform.position.y > this.gameObject.transform.position.y) && (entity.SpeedY/60 < -(entity.gameObject.transform.position.y - this.gameObject.transform.position.y)) ) {
			
			Debug.Log("Platform.CheckIfLanded : Landing");
			
			return true;
			
		}
				
		return false;
	}
	
}
