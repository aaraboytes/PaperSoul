using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvionControl : MonoBehaviour
{
	[SerializeField] LevelManager _levelManager;

	public float thrust = 1.0f;
	public float extraThrust = 0.2f;
	public float area = 0.3f;
	public LineRenderer lineRenderer;
	public Rigidbody body;
	public Vector3 centerOfMass;
	public UnityAction OnCrashed;

	private bool collision;
	private bool firstLaunch = true;
	private float drag;
	private Vector3 oriented;

	Vector3 screenPoint;
	Vector3 scanPos;
	Vector3 offset;

	private bool phyActive = false;
    private void OnDrawGizmosSelected()
    {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position + centerOfMass,0.01f);
    }
    private void Awake()
    {
		body = GetComponent<Rigidbody>();
		body.isKinematic = true;
		body.centerOfMass = centerOfMass;
    }
    void Start()
	{
		lineRenderer.enabled = true;
		_levelManager.OnCoinUsed += EnableLaunch;
	}

	void Update()
	{
        if (lineRenderer.enabled)
        {
			Camera cam = Camera.main;
			screenPoint.z =  - 15f;
			screenPoint = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,50f));
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, screenPoint);

			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((transform.position- screenPoint).normalized),0.5f);
		}

		if (Input.GetMouseButtonDown(0) && !phyActive)
		{
            if (firstLaunch)
            {
				body.isKinematic = false;
				body.AddForce(-transform.forward * thrust, ForceMode.Impulse);
				firstLaunch = false;
				_levelManager.OnOrigamiLaunched?.Invoke();
            }
            else
            {
				Time.timeScale = 1;
				body.angularVelocity = Vector3.zero;
				body.velocity = transform.forward * 0.1f ;
				body.AddForce(-transform.forward * extraThrust, ForceMode.Impulse);

            }
			phyActive = true;
			lineRenderer.enabled = false;
		}
	}

	void FixedUpdate()
	{
		float vel = body.velocity.magnitude;
		if (vel < 0.02f)
		{
			oriented = Vector3.Normalize(screenPoint - transform.position);
		}
		else
		{
			drag = 0.5f * vel * vel * 1.2f * area * 0.08f; 
			body.AddForce(Vector3.up * drag);
			oriented = Vector3.Normalize(body.velocity);
		}
		if(phyActive && !collision)
			transform.rotation = Quaternion.LookRotation(oriented,transform.up);
	}
    private void OnCollisionEnter(Collision other)
    {
		if (other.gameObject.CompareTag("Ground") && !collision)
		{
			collision = true;
			OnCrashed?.Invoke();
		}
    }
    public void EnableLaunch()
    {
		phyActive = false;
		Time.timeScale = 0.2f;
		/*LeanTween.value(1, 0.02f, 0.3f).setOnUpdate((val) => { Time.timeScale = val; });*/
		lineRenderer.enabled = true;
	}
    public void Reset()
    {
		collision = false;
		firstLaunch = true;
		phyActive = false;
		lineRenderer.enabled = true;
    }
}