using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FirstPersonMovement : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] private float speed = 10f;
    [SerializeField] private float gravity = -9.81f;

    Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
