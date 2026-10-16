using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool dragging;
    public bool Win = false;
    public Vector2 offset;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;

    public int ID;
    public int ID_CA = 100;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!dragging) return; 
       
        var mousepos = GetMousePos();
        transform.position = mousepos - offset;
        ID_CA = GameObject.Find("Generador").GetComponent<Generador>().ID_Casilla;

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
        if (Win)
        {
            Debug.Log("Ganaste");
            GameObject.Find("Generador").GetComponent<Generador>().ganar_nivel = true;
        }
       
    }



    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Objeto")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                rigidbody2D.AddForce(Vector2.left * 20);
                Debug.Log("choque");

            }

           
        }

        if (collision.gameObject.tag == "Casilla" && ID_CA == ID)
        {
            Win = true;
            Debug.Log("Correcto");
            
        }



        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        Win = false;
    }


}
