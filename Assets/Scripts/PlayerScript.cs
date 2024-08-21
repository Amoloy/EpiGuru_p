using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float ForwardSpeed = 5f;
    public float SidewaysSpeed = 4f;
    public float MinX = -5.8f;
    public float MaxX = 5.8f;
    
    private bool moveLeft = false;
    private bool moveRight = false;
    
    private GameManager _gameManager;
    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * ForwardSpeed * Time.deltaTime);
        
        //float horizontalInput = Input.GetAxis("Horizontal");
        
        //Vector3 newPosition = transform.position + Vector3.right * horizontalInput * SidewaysSpeed * Time.deltaTime;

        if (moveLeft && moveRight || !moveLeft && !moveRight)
        {
            return;
        }

        Vector3 newPosition = transform.position;
        
        if (moveLeft)
        {
            newPosition += Vector3.left * SidewaysSpeed * Time.deltaTime;
        }
        else if (moveRight)
        {
            newPosition += Vector3.right * SidewaysSpeed * Time.deltaTime;
        }
        
        newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        
        transform.position = newPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            _gameManager.GameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            _gameManager.Finish();
        }
    }
    
    public void OnLeftButtonDown()
    {
        moveLeft = true;
    }

    public void OnLeftButtonUp()
    {
        moveLeft = false;
    }

    public void OnRightButtonDown()
    {
        moveRight = true;
    }

    public void OnRightButtonUp()
    {
        moveRight = false;
    }
}
