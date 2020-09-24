using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineTest : MonoBehaviour
{
	
	public Camera cam;
	
	LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
		lineRenderer = gameObject.GetComponent<LineRenderer>();
        //cam = Camera.main;
		//lineRenderer.SetPosition(0, transform.position);
		//lineRenderer.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		Vector3 mos2Space;
		mos2Space = cam.ScreenToWorldPoint (Input.mousePosition);
		Debug.Log(mos2Space.x);
		mos2Space.z = 10.0f;
        lineRenderer.SetPosition(1, mos2Space);
    }
}
