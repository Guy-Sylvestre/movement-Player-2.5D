using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public CharacterController character;
    public float jumpForce = 10f;
    public float gravity = -20f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Animator animator;
    public Transform model;
    void Start()
    {
        
    }

    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        direction.x = dirX * speed;

        // Convertion de la recuperation de l'axe horizontal
        float convertValueDirX = Mathf.Abs(dirX);
        animator.SetFloat("Speed", convertValueDirX);

        // Verifier si le joueur est au sol
        bool isGround = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
        animator.SetBool("isGrounded", isGround);


        // Ajouter de la grvite pour faire redecendre le joueur lorsqu'il saut
        direction.y += gravity * Time.deltaTime;
        if (isGround)
        {
            
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        }

        // Fliper le personnage en fonction de sa direction
        if (dirX != 0)
        {
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(dirX, 0f, 0f));
            model.rotation = newRotation;
        }

        // Deplacement finale avec la methode Move()
        character.Move(direction * Time.deltaTime);
    }
}
