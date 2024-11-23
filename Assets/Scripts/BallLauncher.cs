using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 endDragPosition;
    public BlockSpawner blockSpawner;
    internal List<Ball> balls = new List<Ball>();

    [SerializeField]
    private Ball ballPrefab;
    public int ballsReady;
    private bool isAiming;
    private Vector2 _shootDirection;
    private bool _isBallsReady = true;
    [SerializeField]
    private LineRenderer aimLineRenderer;
    [SerializeField]
    private float aimLineLength = 5f; // Length of the aiming line


    private void Awake()
    {
        CreateBall();
        aimLineRenderer.enabled = true; // Disable aim line initially
    }

    public void ReturnBall()
    {
        ballsReady = 0;
        foreach (Ball ball in balls)
        {
            if (!ball.isActiveAndEnabled)
            {
                ballsReady++;
            }
        }
        if (ballsReady == balls.Count)
        {
            blockSpawner.SpawnRowOfBlocks();
            _isBallsReady = true;
            aimLineRenderer.enabled = true;
        }
    }

    public void CreateBall()
    {
        var ball = Instantiate(ballPrefab);
        balls.Add(ball);
        ball.gameObject.SetActive(false);
        ballsReady++;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;// Enable the aim line when aiming starts
        }

        // Update aim direction
        if (isAiming)
        {
            Vector2 aimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            aimPosition.y = Mathf.Max(aimPosition.y, -3.5f);
            _shootDirection = (aimPosition - (Vector2)transform.position).normalized;

            // Optional: Add aiming visualization
            UpdateAimLine();
        }

        // Shoot the ball
        if (Input.GetMouseButtonUp(0) && isAiming && _isBallsReady)
        {
            isAiming = false;
            aimLineRenderer.enabled = false; // Disable the aim line when shooting
            EndDrag();
        }
    }

    private void EndDrag()
    {
        _isBallsReady = false;
        StartCoroutine(LaunchBalls());
    }

    private IEnumerator LaunchBalls()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            Ball ball = balls[i];
            ball.transform.position = transform.position;
            ball.gameObject.SetActive(true);
            ball.GetComponent<Rigidbody2D>().AddForce(_shootDirection);

            yield return new WaitForSeconds(0.1f);
        }
        ballsReady = 0;
    }
    private void UpdateAimLine()
    {
        // Define the start and end points of the straight line
        Vector3 startPoint = transform.position;
        Vector3 endPoint = startPoint + (Vector3)_shootDirection * aimLineLength;

        // Set the points for the LineRenderer
        aimLineRenderer.positionCount = 2;
        aimLineRenderer.SetPosition(0, startPoint);
        aimLineRenderer.SetPosition(1, endPoint);
    }

}
