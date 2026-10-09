using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool dragging;
    public Vector2 offset;
    SpriteRenderer spriteRenderer;
    Rigidbody2D Rigidbody2D;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (!dragging) return; 
       
        var mousepos = GetMousePos();
        transform.position = mousepos - offset;

    }

    void OnMouseDown()
    {
        dragging = true;
        spriteRenderer.sortingOrder = 1;
        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        dragging = false;
       
    }



    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        
        Rigidbody2D.AddForce(Vector2.left * 20);
        
        

        Debug.Log("choque");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D.linearVelocity = Vector2.zero;
    }


}
