using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CozyChaos2024Fall
{
    public class Crow : MonoBehaviour
    {
        [SerializeField] private float flySpeed = 8f;
        [SerializeField] private Vector3 destination;

        private void Update()
        {
            if ((destination - transform.position).magnitude >= 0.1f)
            {

                transform.position = Vector3.MoveTowards(transform.position, destination, flySpeed * Time.deltaTime);
            }

        }

    }
}
