using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class BallIncreaser : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _self;
    [SerializeField]
    private CircleCollider2D _collider;
    [SerializeField]
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private MeshFilter _meshFilter;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private int _splits;
    
    private Mesh _mesh;
    public Vector3[] _vertices;
    public int[] _triangles;
    private bool _canSpawn;
    private float _sinceSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (!_self || !_collider || !_rigidbody || !_meshFilter) { 
            throw new MissingReferenceException("One or more components do not have references!");
        }

        // IMPORTANT!
        // Timescale adjust
        Time.timeScale = 1.5f;

        // Setup the edge collider
        _collider.radius = _radius;
        _canSpawn = false;
        _sinceSpawn = Time.time;

        // Setup the mesh and render
        _mesh = new Mesh();
        _meshFilter.mesh = _mesh; // Set to our custom mesh

        DrawCircle();

        // Initial push
        // _rigidbody.AddForce(new Vector2(Random.Range(-2f, 2f), Random.Range(-1f, 2f)));
        _rigidbody.AddForce(new Vector2(5, 5));
    }

    private void DrawCircle() {
        _collider.radius = _radius;
        _vertices = GenerateCirclePoints(_radius, _splits);
        _triangles = GenerateCircleTriangles(_vertices);
        _mesh.Clear();
        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles;
    }

    // Update is called once per frame
    private Vector3[] GenerateCirclePoints(float radius, int splits) {

        double stepsize = 2 * Math.PI / splits;
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i <= splits; i++) {
            points.Add(
                new Vector3(
                    radius * (float) Math.Cos(i * stepsize), 
                    radius * (float) Math.Sin(i * stepsize), 
                    0
                )
            );
        }

        return points.ToArray();
    }

    private int[] GenerateCircleTriangles(Vector3[] vertices) {
        List<int> triangles = new List<int>();

        for (int i = 0; i < vertices.Length - 2; i++) {
            triangles.Add(0);
            triangles.Add(i + 2);
            triangles.Add(i + 1);
        }

        return triangles.ToArray();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Collisions
        ContactPoint2D[] contacts = new ContactPoint2D[other.contactCount];
        other.GetContacts(contacts);

        // Get new 
        Vector3 contact = contacts[0].point;
        
        // Change ball colour
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.color = new Color(
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f),
            Random.Range(0.0f, 1.0f)
        );

        _radius *= 1.01f;
        DrawCircle();
    }
}
