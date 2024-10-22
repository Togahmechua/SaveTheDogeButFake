using System;
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

    private void Awake()
    {
        OnInit();
    }

    private void OnEnable()
    {
        /*lineRenderer = null;
        rb = null;
        edgeCollider = null;*/
        listPoint.Clear();

        //OnInit();

        rb.simulated = false;
        lineRenderer.positionCount = 0;
        /*edgeCollider.points = Array.Empty<Vector2>();
        edgeCollider.points = new Vector2[0];*/
        edgeCollider.isTrigger = true;
        LevelManager.Ins.isDrawed = false;
    }

    private void OnInit()
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
            SoundFXMNG.Ins.PlaySFX(SoundFXMNG.Ins.bee);
            LevelManager.Ins.isDrawed = true;
            edgeCollider.SetPoints(listPoint);
            LevelManager.Ins.StartGame();
            UIManager.Ins.InGameCanvas.ActiveClock();   
            rb.simulated = true;
            this.enabled = false;
            if (listPoint.Count > 1)
            {
                edgeCollider.isTrigger = false;
            }
        }

        if (!Input.GetMouseButton(0))
            return;

        if (UIManager.Ins.InGameCanvas.fillBar.fillAmount <= 0)
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
            UIManager.Ins.InGameCanvas.DecreaseFillAmount();
        }
    }
}
