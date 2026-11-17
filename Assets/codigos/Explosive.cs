using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //check para ver si objeto se encuentra activo
    public bool active = false;
    //check adicional para asegurar que no hayan temporizadores infinitos
    public bool isTimerRunning = false;
    public GameObject explosion;
    public GameObject explosionAudio;

    private ScreenShake screenShake;


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

    //codigo explosion
    public void Explode()
    {
        /*
        screenShake = Camera.main.GetComponent<ScreenShake>();
        StartCoroutine(screenShake.Shake(0.5f, 0.5f));
        */
        Instantiate(explosion, transform.position, Quaternion.identity);
        Instantiate(explosionAudio, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    //codigo para termporizador para explosion
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);


        Explode();
    }

    // Update is called once per frame
    void Update()
    {
        //codigo para activacion de temporizador
        if (active && !(isTimerRunning)) {
            Debug.Log("Objeto activado: " );


            isTimerRunning = true;
            StartCoroutine(Timer());

        }

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
        //activation of item, also have to add this one life for when an explosion touches an explosive
        active = true;


        dragging = true;
       
        offset = GetMousePos() - (Vector2)transform.position;
    }

    public void Soltar()
    {
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

            }
                

            

           
        }


        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        rigidbody2D.linearVelocity = Vector2.zero;
        Win = false;
        impacts = false;
    }


}
