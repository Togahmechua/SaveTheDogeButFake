using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCtrl : MonoBehaviour
{
    private Vector2 prePoint;

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EdgeCollider2D edgeCollider;
    [SerializeField] private List<Vector2> listPoint = new List<Vector2>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            listPoint.Clear();
            lineRenderer.positionCount = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            edgeCollider.SetPoints(listPoint);
            LevelManager.Ins.StartGame();
            rb.simulated = true;
        }

        if (!Input.GetMouseButton(0))
            return;

        Vector2 newPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector2.Distance(newPoint, prePoint) < 0.25f)
            return;

        prePoint = newPoint;
        listPoint.Add(newPoint);

        lineRenderer.positionCount = listPoint.Count;

        for (int i = 0; i < listPoint.Count; i++)
        {
            lineRenderer.SetPosition(i, listPoint[i]);
        }
    }
}
