using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.UI;
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
        if (!_self || !_collider || !_renderer) {
            throw new MissingReferenceException();
        }

        List<Vector2> points = GetPoints(_radius, _splits); // Generate points
        _collider.SetPoints(points); // Give points to collider
        _renderer.positionCount = _splits;
        _renderer.SetPositions(points.ConvertAll(x => (Vector3) x).ToArray()); // Give points to renderer

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

}
