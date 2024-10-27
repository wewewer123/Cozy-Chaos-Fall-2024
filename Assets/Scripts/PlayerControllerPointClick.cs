using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerPointClick : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject crowManager;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScareCrow();
        }
    }
    private void ScareCrow()
    {
        Debug.Log("scaring crows 1");
        if(GetComponentInChildren<BoxCollider2D>().IsTouchingLayers(7))
        {
            Debug.Log("scaring crows 2");
            for (int i = 0; crowManager.GetComponent<CrowManager>().Crows.Count > i; i++)
            {
                Debug.Log("scaring crows 3");
                if (GetComponentInChildren<BoxCollider2D>().IsTouching(crowManager.GetComponent<CrowManager>().Crows[i].gameObject.GetComponent<BoxCollider2D>()))
                {
                    Debug.Log("scaring crows 4");
                    crowManager.GetComponent<CrowManager>().Crows[i].FlyAway();
                }
            }
        }
    }
}

