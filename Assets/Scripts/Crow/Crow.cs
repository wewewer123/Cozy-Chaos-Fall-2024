using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CozyChaos2024Fall
{
    public class Crow : MonoBehaviour
    {
        [SerializeField] private float flySpeed = 15f;
        [HideInInspector] public Crop target;

        [HideInInspector] public CrowManager crowManager;



        private Vector3 destination;
        private bool isReturning;

        private void Start()
        {
            destination = target.transform.position;
            target.OnCropDestroyed += WhenCropDestroyed;
        }

        private float lastPeckedTime;

        private void Update()
        {
            if ((destination - transform.position).magnitude >= 0.1f)
            {

                transform.position = Vector3.MoveTowards(transform.position, destination, flySpeed * Time.deltaTime);
            }

            if ((destination - transform.position).magnitude <= 0.1f)
            {
                if (isReturning)
                {
                    Destroy(gameObject);
                    return;
                }
                if (Time.time - lastPeckedTime < crowManager.peckingSpeed) return;
                lastPeckedTime = Time.time;
                target.TakeDamage(1);
            }
        }

        private void OnDisable()
        {
            target.OnCropDestroyed -= WhenCropDestroyed;
        }

        private void WhenCropDestroyed(object sender, EventArgs e)
        {
            isReturning = true;
            destination = crowManager.GetRandomEdgeLocation();
        }
    }
}
