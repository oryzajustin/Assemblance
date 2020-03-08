using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviourPun
{
    [SerializeField] float walk_speed;
    [SerializeField] float turn_smooth_time;

    private float turn_smooth_velocity;

    [SerializeField] float speed_smooth_time;
    private float speed_smooth_velocity;
    private float curr_speed;

    private float speed;
    private float animation_speed_percent;
    private Animator animator;

    //private float gravity = -12f;
    //private float velocity_y;
    //[SerializeField] float jump_speed;

    private Transform camera_transform;

    private CharacterController controller;

    private PlayableCharacter selfCharacter;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        camera_transform = Camera.main.transform;

        selfCharacter = this.GetComponent<PlayableCharacter>();

        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
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

        float target_speed = walk_speed * input_direction.magnitude; // the speed we want to reach
        curr_speed = Mathf.SmoothDamp(curr_speed, target_speed, ref speed_smooth_velocity, speed_smooth_time); // damp to the target speed from our current speed

        Vector3 velocity = this.transform.forward * curr_speed;// + Vector3.up * velocity_y; // velocity

        animation_speed_percent = input_direction.magnitude; // handles the animation speed percent
        animator.SetFloat("speedPercent", animation_speed_percent, speed_smooth_time, Time.deltaTime); // dampen the animation to the target animation

        controller.Move(velocity * Time.deltaTime);
    }
}
