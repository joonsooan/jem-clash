using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class UnitMovement : MonoBehaviour
{
    public bool testBool = true;
    
    private Rigidbody2D rb;
    private UnitStats stats;
    
    private Vector2 currentDirection;
    private float movementInterval = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<UnitStats>();
    }

    private void Start()
    {
        StartCoroutine(UpdateDirectionCoroutine());
    }

    private IEnumerator UpdateDirectionCoroutine()
    {
        while (testBool)
        {
            SetRandomDirection();
            yield return new WaitForSeconds(movementInterval);
        }
    }

    private void SetRandomDirection()
    {
        currentDirection = new Vector2(
            Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Debug.Log("Change direction");
    }

    private void Update()
    {
        MoveInCurrentDirection();
    }
    
    private void MoveInCurrentDirection()
    {
        if (currentDirection != Vector2.zero)
        {
            rb.MovePosition(rb.position + currentDirection * (stats.moveSpeed * Time.deltaTime));
        }
    }
}