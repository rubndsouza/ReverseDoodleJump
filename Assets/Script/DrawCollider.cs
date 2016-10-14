using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DrawCollider : MonoBehaviour 
{
	private LineRenderer line; // Reference to LineRenderer
	private Vector3 mousePos;	
	private Vector3 startPos;	// Start position of line
	private Vector3 endPos;	// End position of line
	private int lineObjNum = 0;


	void OnTriggerEnter(Collider hit)
    {
		Debug.Log("hello");
		line.SetColors(Color.red, Color.red);
	}
	void Update() 
	{
		/*if(Input.GetMouseButtonDown(1)) {
					Debug.Log("rc");

			//GameObject myGameObject = new GameObject("Test Object"); // Make a new GO.
			Rigidbody2D gameObjectsRigidBody = gameObject.AddComponent<Rigidbody2D>(); // Add the rigidbody.
			gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.
			gameObjectsRigidBody.gravityScale = 0.2f;
			SpriteRenderer spr = gameObject.GetComponent<SpriteRenderer>();
			spr.enabled = true;
		}*/
		
		// On mouse down new line will be created 
		if(Input.GetMouseButtonDown(0))
		{
			
			if(line == null) {
				Debug.Log("line is null ");
				createLine();
			}
				
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			mousePos.z = 0;
			line.SetPosition(0,mousePos);
			startPos = mousePos;
		}
		else if(Input.GetMouseButtonUp(0))
		{
			if(line)
			{
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;
				line.SetPosition(1,mousePos);
				endPos = mousePos;
				addColliderToLine();
				line = null;
				lineObjNum++;
			}
		}
		else if(Input.GetMouseButton(0))
		{
			if(line)
			{
				mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mousePos.z = 0;
				line.SetPosition(1,mousePos);
			}
		}
	}
	// Following method creates line runtime using Line Renderer component
	private void createLine()
	{
		if(line == null) {
		GameObject lineObj = new GameObject("Line" + lineObjNum);
		
		line =  lineObj.AddComponent<LineRenderer>();
		Debug.Log("createLine");
		line.material =  new Material(Shader.Find("Diffuse"));
		line.SetVertexCount(2);
		line.SetWidth(0.1f,0.1f);
		line.SetColors(Color.red, Color.red);
		line.useWorldSpace = true; 
		//lineObj.localPosition=new Vector3(x, y, 0);
		}		
	}
	// Following method adds collider to created line
	private void addColliderToLine()
	{
		Debug.Log("addColliderToLine");
		//GameObject lineObj = GameObject.Find("Line" + lineObjNum);
        EdgeCollider2D col = new GameObject("Collider").AddComponent<EdgeCollider2D>();
        col.transform.parent = line.transform;

        float lineLength = Vector3.Distance (startPos, endPos); // length of line
		Vector3 midPoint = (startPos + endPos)/2;
        //col.transform.position = midPoint; // setting position of collider object
        List<Vector2> newVerticies = new List<Vector2>();
        newVerticies.Add(startPos);
        newVerticies.Add(midPoint);
        newVerticies.Add(endPos);
        col.points = newVerticies.ToArray();
		// Following lines calculate the angle between startPos and endPos
		/*float angle = (Mathf.Abs (startPos.y - endPos.y) / Mathf.Abs (startPos.x - endPos.x));
		if((startPos.y<endPos.y && startPos.x>endPos.x) || (endPos.y<startPos.y && endPos.x>startPos.x))
		{
			angle*=-1;
		}
		angle = Mathf.Rad2Deg * Mathf.Atan (angle);
		col.transform.Rotate (0, 0, angle);*/
	}
}