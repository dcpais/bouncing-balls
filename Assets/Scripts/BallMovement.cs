using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.PackageManager;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallMovement : MonoBehaviour
{
    
    public GameObject self;
    public Transform transform;
    public CircleCollider2D collider;
    public Rigidbody2D rigidbody;
    public double speed;
    public Vector2 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        this.transform = (Transform) self.GetComponent("Transform");
        this.collider = (CircleCollider2D) self.GetComponent("CircleCollider2D");
        this.rigidbody = (Rigidbody2D) self.GetComponent("Rigidbody2D");
        this.direction = new Vector2(Random.Range(-0.5f, -0.5f), Random.Range(-0.5f, -0.5f));
        this.direction = this.direction.normalized;
        this.speed = 3.0f;

        if (self == null) { 
            throw new NullReferenceException("GameObject not set");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D[] contacts = new ContactPoint2D[20]; 
        other.GetContacts(contacts);
        Vector2 newDirection = contacts[0].normal;
    }
}
