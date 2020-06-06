using System;
using UnityEngine;

namespace GameScripts
{
    public class BotMovement : MonoBehaviour
    {
        public float speed = 10.0f;
        public Vector3 destination;

        private void Awake()
        {
            destination = transform.position;
        }

        private void Update()
        {
            float step = (speed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }
}