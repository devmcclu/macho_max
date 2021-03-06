﻿using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    //Speed multiplyer affected by macho
    public float speedMulti = 1f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    PlayerMacho playerMacho;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerMacho = GetComponent<PlayerMacho>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    void Move (float h, float v)
    {
        movement.Set(h, 0f, v);

        if (playerMacho.isMacho)
        {
            speedMulti = 2.0f;
        }
        else
        {
            speedMulti = 1.0f;
        }

        movement = movement.normalized * speed *speedMulti * Time.deltaTime;

        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        bool macho = (h != 0f && playerMacho.isMacho == true) || (v != 0f && playerMacho.isMacho == true);
        anim.SetBool("IsWalking", walking);
        anim.SetBool("IsMacho", macho);
    }
}
