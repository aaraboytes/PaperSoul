using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFoll : MonoBehaviour
{
    
    public GameObject objectToFollow;
    
    public float speed = 2.0f;
	
	void Start()
	{
		transform.position = transform.position + objectToFollow.transform.position*0.001f;
	}
	
	
    
    void Update() {
       /*  float interpolation = speed * Time.deltaTime;
		
		Vector3 velObj = objectToFollow.GetComponent<Rigidbody>().velocity;
		float movinObj = velObj.magnitude;
		
		Vector3 position = this.transform.position;
		Debug.Log(movinObj);
		if ( movinObj < 0.4f)
		{
			
			position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
			position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
			position.z = objectToFollow.transform.position.z - 12.0f;
			
        }
        this.transform.position = position; */
    }
}