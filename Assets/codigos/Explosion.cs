using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    private float radius = 4;
    private float explosionStrenght = 400;


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

    

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        Exploding();
        
    }

    //codigo explosion
    public void Exploding()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D collider in objects)
        {
            Rigidbody2D rb2D = collider.GetComponent<Rigidbody2D>();

            Explosive explosivo = collider.GetComponent<Explosive>();

            if (explosivo != null)
            {
                explosivo.active = true;
            }

            if(rb2D != null)
            {
                Vector2 dir = collider.transform.position - transform.position;

                float distance = 1 + dir.magnitude;
                float finalStrenght = explosionStrenght / distance;

                rb2D.AddForce(dir * finalStrenght);
            }
        }

        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
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

            }
                

            

           
        }


        */
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        Win = false;
        impacts = false;
    }


}
