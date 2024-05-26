using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CircleCage : MonoBehaviour
{
    // Components
    [SerializeField]
    private GameObject _self;
    [SerializeField]
    private EdgeCollider2D _collider;
    [SerializeField]
    private LineRenderer _renderer;
    [SerializeField]
    private float _radius;
    [SerializeField]
    private int _splits;

    // Start is called before the first frame update
    void Start()
    {

        List<Vector2> points = GetPoints(_radius, _splits);
        _collider.SetPoints(points);
        _renderer.SetPositions(points.ConvertAll(x => (Vector3) x).ToArray());

    }

    private List<Vector2> GetPoints(float radius, int splits) {
        List<Vector2> points = new List<Vector2>();
        
        double stepsize = 2 * Math.PI / splits;
        for (int i = 0; i <= splits; i++) {
            points.Add(
                new Vector2(
                    radius * (float) Math.Cos(i * stepsize),
                    radius * (float) Math.Sin(i * stepsize)
                )
            );
        }

        return points;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
