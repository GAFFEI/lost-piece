using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public bool dragging = false;

    public bool collected = false;
    
    public bool impacts = false;
    public Vector2 offset;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody2D;


    public Phisics_obj up;
    public Phisics_obj down;
    public Phisics_obj left;
    public Phisics_obj right;

    public AudioSource aud2;
    public AudioClip col;
    public AudioClip pickup;
    public AudioClip drop;
    public bool SC = false;


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



        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(
                Input.mousePosition
            );

            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

            if (hits.Length > 0)
            {
                Collider2D topObject = hits
                    .OrderByDescending(hit =>
                    {
                        SpriteRenderer sr =
                            hit.GetComponent<SpriteRenderer>();

                        if (sr == null)
                            sr = hit.GetComponentInParent<SpriteRenderer>();

                        return sr != null ? sr.sortingOrder : 0;
                    })
                    .First();

                Debug.Log("Objeto seleccionado: " +
                          topObject.gameObject.name);
            }
        }





    }

    void OnMouseDown()
    {
        if(dragging == false)
        {
            aud2.PlayOneShot(pickup, 0.3f);
        }
        dragging = true;
        spriteRenderer.sortingOrder = 1;
        offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnMouseUp()
    {
        if (dragging && collected == false)
        {
            aud2.PlayOneShot(drop, 0.05f);
        }

        dragging = false;
        if (collected)
        {
            GameObject.Find("Generador").GetComponent<Generador>().AddTime();
            Destroy(gameObject);
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

        if (collision.gameObject.tag == "R Slot")
        {
            collected = true;
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
        collected = false;
        impacts = false;
        SC = false;
    }


}
