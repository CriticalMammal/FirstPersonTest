using UnityEngine;
using System.Collections;

public class GrassBend : MonoBehaviour {

	public float bentHeight = 0f;
	public float lerpSpeedBend = 0.01f;
	public float lerpSpeedUnbend = 0.01f;
	private float originalHeight;
	private float originalY;
	private bool isBent;

	// Use this for initialization
	void Start () {
		originalHeight = this.transform.localScale.y;
		originalY = this.transform.position.y;
		isBent = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (isBent == true)
		{
			Vector3 currentScale = new Vector3(this.transform.localScale.x,
			                               this.transform.localScale.y,
			                               this.transform.localScale.z);
			Vector3 goalScale = new Vector3(currentScale.x, bentHeight, currentScale.z);
			Vector3 newScale = Vector3.Lerp(currentScale, goalScale, lerpSpeedBend);

			if (newScale.y <= bentHeight+0.001f) //if reached max bend
			{
				//isBent = false;
			}
			
			this.transform.localScale = new Vector3(newScale.x, newScale.y, newScale.z);

			//compensate height difference
			float newY = originalY - (originalHeight - this.transform.localScale.y);
			Vector3 newPos = new Vector3(this.transform.position.x, newY,
			                             this.transform.position.z);
			this.transform.position = newPos;
		}
		else
		{
			if (this.transform.localScale.y+0.001f < originalHeight)
			{
				Vector3 currentScale = new Vector3(this.transform.localScale.x,
				                                   this.transform.localScale.y,
				                                   this.transform.localScale.z);
				Vector3 goalScale = new Vector3(currentScale.x, originalHeight, currentScale.z);
				Vector3 newScale = Vector3.Lerp(currentScale, goalScale, lerpSpeedUnbend);
				
				this.transform.localScale = new Vector3(newScale.x, newScale.y, newScale.z);

				//compensate height difference
				float newY = originalY - (originalHeight - this.transform.localScale.y);
				Vector3 newPos = new Vector3(this.transform.position.x, newY,
				                             this.transform.position.z);
				this.transform.position = newPos;
			}
			else if (transform.position.y != originalY)
			{
				Vector3 newPos = new Vector3(this.transform.position.x, originalY,
				                             this.transform.position.z);
				this.transform.position = newPos;
			}
		}

	}

	void LateUpdate()
	{
		isBent = false;
	}

	void OnTriggerStay(Collider other)
	{
		isBent = true;
		//Debug.Log("collided");
	}

	public void BendGrass()
	{
		isBent = true;
	}
}
