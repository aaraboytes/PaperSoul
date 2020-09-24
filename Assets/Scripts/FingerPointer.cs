using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerPointer : MonoBehaviour
{
    [SerializeField] GameObject _particle;
    [SerializeField] Vector3 _offset;
    public bool Active => active;
    public GameObject TouchedObject => touchingGameObject;

    private bool active = false;
    private Camera camera;
    private GameObject touchingGameObject;
    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        active = Input.GetMouseButton(0);
        if (active)
        {
            Vector2 mousePos = Input.mousePosition;
            Ray screenRay = camera.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(screenRay, out hit))
            {
                touchingGameObject = hit.collider.gameObject;
                _particle.transform.position = hit.point + _offset;
            }
        }
    }
    public void SetActive(bool newActive)
    {
        active = newActive;
    }
}
