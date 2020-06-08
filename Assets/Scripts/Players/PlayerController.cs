using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Controls the movement of the 3D character
public class PlayerController : MonoBehaviourPun
{
    [SerializeField]
    private float walk_speed;
    
    [SerializeField]
    private float turn_smooth_time;
    private float turn_smooth_velocity;

    [SerializeField]
    private float speed_smooth_time;
    
    private float speed_smooth_velocity;
    private float curr_speed;

    private float speed;
    private float animation_speed_percent;
    private Animator animator;

    private Transform camera_transform;
    private CharacterController controller;
    private PlayableCharacter selfCharacter;

    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        camera_transform = Camera.main.transform;

        selfCharacter = this.GetComponent<PlayableCharacter>();

        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (photonView.IsMine)  // Determines if you're the correct player
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // controller input
        Vector2 input_direction = input.normalized; // normalize the direction vector to prevent fast diagonal movement
        if (input_direction != Vector2.zero) // rotate the player model towards the input direction
        {
            float target_rotation = Mathf.Atan2(input_direction.x, input_direction.y) * Mathf.Rad2Deg + camera_transform.eulerAngles.y;
            this.transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(this.transform.eulerAngles.y, target_rotation, ref turn_smooth_velocity, turn_smooth_time);
        }

        selfCharacter.direction = transform.forward;

        float target_speed = walk_speed * input_direction.magnitude; // The speed we want to reach
        curr_speed = Mathf.SmoothDamp(curr_speed, target_speed, ref speed_smooth_velocity, speed_smooth_time); // Damp to the target speed from our current speed

        Vector3 velocity = this.transform.forward * curr_speed;// Velocity

        animation_speed_percent = input_direction.magnitude; // Handles the animation speed percent
        animator.SetFloat("speedPercent", animation_speed_percent, speed_smooth_time, Time.deltaTime); // Dampen the animation to the target animation

        controller.Move(velocity * Time.deltaTime);
    }
}
