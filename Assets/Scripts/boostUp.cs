using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boostUp : MonoBehaviour
{
    public float boosX = 15.0f;
	public float boosY = 15.0f;
	public float boosZ = 15.0f;
 
     public void OnTriggerStay(Collider other)
     {
         if (other.tag == "avion")
		 {
			 Debug.Log("algo paso");
             other.GetComponent<Rigidbody>().AddForce( new Vector3(boosX, boosY, boosZ));
		 }
     }
}
