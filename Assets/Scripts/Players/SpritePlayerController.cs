using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Handles movement for the 2D sprite character
public class SpritePlayerController : MonoBehaviourPun, IPunObservable
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
    private Animator animator;
    private SpriteRenderer spr;

    private Transform camera_transform;

    private CharacterController controller;

    private PlayableCharacter selfCharacter;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        camera_transform = Camera.main.transform;
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        selfCharacter = GetComponent<PlayableCharacter>();
    }

    void Update()
    {
        if (photonView.IsMine)  // Determines if you're the correct player
        {
            MovePlayer();
        }
    }

    // Handles player movement upon input
    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.up * y;

        move = move.normalized;

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            selfCharacter.direction = move;
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if(move.x > 0)
        {
            spr.flipX = false; // flip sprite
        }
        else if(move.x < 0)
        {
            spr.flipX = true;
        }

        controller.Move(move * walk_speed * Time.deltaTime);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(spr.flipX);
        }
        else
        {
            spr.flipX = (bool)stream.ReceiveNext();
        }
    }
}
