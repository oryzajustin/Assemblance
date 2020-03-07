using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpritePlayerController : MonoBehaviourPun
{
    [SerializeField] float walk_speed;
    [SerializeField] float turn_smooth_time;

    private float turn_smooth_velocity;

    [SerializeField] float speed_smooth_time;
    private float speed_smooth_velocity;
    private float curr_speed;

    private float speed;
    //private float animation_speed_percent;
    //private Animator animator;

    //private float gravity = -12f;
    //private float velocity_y;
    //[SerializeField] float jump_speed;

    private Transform camera_transform;

    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        //animator = this.GetComponent<Animator>();
        controller = this.GetComponent<CharacterController>();
        camera_transform = Camera.main.transform;
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * y;

        move = move.normalized;

        controller.Move(move * walk_speed * Time.deltaTime);
    }
}
