using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;
    private PlayerControls playerControls;
    private Rigidbody2D rb; 
    private Collider2D col;


    // Awake

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();

    }
    // Start is called before the first frame update
    private void Start()
    {
        playerControls.PlayerMap.Jump.performed += _ => Jump();
    }

    private void Jump(){
        if (IsGrounded()){
            rb.AddForce( new Vector2(0,jumpSpeed), ForceMode2D.Impulse );
        }
    }

    private bool IsGrounded(){
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRightPoint = transform.position;
        bottomRightPoint.x += col.bounds.extents.x;
        bottomRightPoint.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint,bottomRightPoint,ground);
    }

    // Update is called once per frame 
    private void Update()
    {
        float movementPlayerInput = playerControls.PlayerMap.Move.ReadValue<float>();

        Vector3 currentPosition = transform.position;
        currentPosition.x += movementPlayerInput * speed * Time.deltaTime;
        transform.position = currentPosition;
        Debug.Log("updating");
    }
}
