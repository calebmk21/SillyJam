using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOverworld : MonoBehaviour
{
    [Header("Player Components")] 
    [SerializeField] private Rigidbody2D rb;
    
    [Header("Controller Settings")]
    [SerializeField] private float speedMult = 1f;

    [Header("Collision Checks")] 
    public Collider2D region;
    
    [Header("Variables")]
    private bool inInteractable = false;
    private Vector2 movement = new Vector2(0, 0);


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speedMult * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Interact(InputAction.CallbackContext context)
    {
      if (inInteractable)
            Debug.Log("Interact");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            inInteractable = true;
        }
        Debug.Log("Entered collision");
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            inInteractable = false;
        }
        Debug.Log("Exited collision");
    }
}
