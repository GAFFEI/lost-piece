using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool dragging = false;
    public bool Win = false;
    public bool impacts = false;
    public Vector2 offset;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;

    public int ID;
    public int ID_CA = 100;

    public Phisics_obj up;
    public Phisics_obj down;
    public Phisics_obj left;
    public Phisics_obj right;

    public AudioSource aud2;
    public AudioClip col;
    public AudioClip pickup;
    public AudioClip drop;
    public bool SC = false;

    private Generador generador;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        generador = FindFirstObjectByType<Generador>();
        if (generador != null)
        {
            ID_CA = generador.ID_Casilla;
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (!dragging) return; 
       
        var mousepos = GetMousePos();
        transform.position = mousepos - offset;


        if (generador != null)
        {
            ID_CA = generador.ID_Casilla;
        }

    }

    public void Agarrar()
    {
        if(dragging == false)
        {
            aud2.PlayOneShot(pickup, 0.3f);
        }
        dragging = true;
        offset = GetMousePos() - (Vector2)transform.position;
    }

    public void Soltar()
    {
        if (dragging && Win == false)
        {
            aud2.PlayOneShot(drop, 0.05f);
        }

        dragging = false;
        if (Win)
        {
            Debug.Log("Ganaste");
            if (generador != null)
            {
                generador.ganar_nivel = true;
            }

        }

    }



    Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.gameObject.tag == "Objeto" && dragging)
        {

            Dragger dra = collision.GetComponent<Dragger>();
            dra.impacts = true;
        }

        if(collision.gameObject.tag == "direction" && impacts)
        {
            if(collision.GetComponent<Phisics_obj>() != null)
            {
                Phisics_obj obj = collision.GetComponent<Phisics_obj>();
                if(obj.direction == Vector2.up)
                {
                    rigidbody2D.AddForce(Vector2.down * 20);
                    
                }
                else if(obj.direction == Vector2.down)
                {
                    rigidbody2D.AddForce(Vector2.up * 20);
                }
                
                if (obj.direction == Vector2.left)
                {
                    rigidbody2D.AddForce(Vector2.right * 20);
                }
                else if(obj.direction == Vector2.right)
                {
                    rigidbody2D.AddForce(Vector2.left * 20);
                }

                if(SC == false)
                {
                    aud2.PlayOneShot(col, 0.05f);
                    SC = true;
                }

            }

            



        }

        if (collision.gameObject.tag == "Casilla" && ID_CA == ID)
        {
            Win = true;
            Debug.Log("Correcto");
            
        }

        if (collision.gameObject.tag == "WD")
        {
            rigidbody2D.AddForce(Vector2.left * 150);

        }

        if (collision.gameObject.tag == "WI")
        {
            rigidbody2D.AddForce(Vector2.right * 150);

        }

        if (collision.gameObject.tag == "WU")
        {
            rigidbody2D.AddForce(Vector2.down * 150);

        }

        if (collision.gameObject.tag == "WB")
        {
            rigidbody2D.AddForce(Vector2.up * 150);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        Win = false;
        impacts = false;
        SC = false;
    }


}
