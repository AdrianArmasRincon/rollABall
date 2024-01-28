using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this for Text
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // Add this for SceneManager

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;
    public float speed = 0;
    public float jumpForce = 5.0f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        // Jump
        if (IsGrounded() && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        // Check if the player is below the map
        if (transform.position.y < 0)
        {
            // Reset the player's position to (0, 0, 0)
            transform.position = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 10)
        {
            winTextObject.SetActive(true);

            // Call the LoadMainMenuDelayed function after 5 seconds
            Invoke("LoadMainMenuDelayed", 5f);
        }
    }

    void LoadMainMenuDelayed()
    {
        // Switch to the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }

    bool IsGrounded()
    {
        // Implement your ground check method here (e.g., raycast or other appropriate method).
        // Return true if grounded, false otherwise.
        return true; // Placeholder; replace this with your actual ground check.
    }
}
