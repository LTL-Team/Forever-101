using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;

    private Animator animator; // SetFloat("moveX", _), SetFloat("moveY", _);

    public LayerMask solidObjectsLayer;

    // private Rigidbody2D rb;

    void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (input.x != 0) input.y = 0;

        if (input != Vector2.zero)
        {
            isMoving = true;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
        else
        {
            isMoving = false;
        }
        animator.SetBool("isMoving", isMoving);
    }

    void FixedUpdate()
    {

        if (isMoving)
        {
            Vector2 position = transform.position;
            position += input * moveSpeed * Time.fixedDeltaTime;

            RaycastHit2D hit = Physics2D.Raycast(position, input, 0.2f, solidObjectsLayer);
            Debug.DrawLine(transform.position, position + input * moveSpeed * Time.fixedDeltaTime * 5, Color.red);
            if (hit.collider == null)
            {
                transform.position = position;
            }
            else if (hit.collider.gameObject.name == "Ad Area") {
                Debug.Log("Ad Area");
                hit.collider.gameObject.GetComponent<InterstitialAd>().ShowAdInsert();
            }

        }

    }
}