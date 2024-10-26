using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPointClick : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Vector3 destination;

    private Camera _mainCamera;
    private CropManager _cropManager;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cropManager = FindObjectOfType<CropManager>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            destination = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            destination.z = transform.position.z;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _cropManager.Interact(transform.position);
        }

        if ((destination - transform.position).magnitude >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        }
    }

}

