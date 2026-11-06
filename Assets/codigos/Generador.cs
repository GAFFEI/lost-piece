using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class Generador : MonoBehaviour
{

    //los que aparecen
    public List <GameObject> objetos;

    [SerializeField] private Timebar T;

    public TextMeshProUGUI texto;

    public AudioSource aud;
    public AudioClip Fit;
    public float time;

    public int num = 0;
    public int R = 0;
    public int RT = 0;
    public int ID_Casilla;
    //distintos tipos de objetos
    public List <GameObject> Tipo;
    public List<GameObject> Tipo_casilla;

    public List<GameObject> Bombas;


    public GameObject Casilla;

    public bool ganar_nivel = false;

    public float range_spawn_x = 3;
    public float range_spawn_x2 = -3;
    public float range_spawn_y = 2;
    public float range_spawn_y2 = -2;
    public int nivel;

    public int puntaje = 0;

    public float ry;
    public float rx;

    public static int EScore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerarObjetos();
        
        nivel = 0;
        texto = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Destruir();
        }

        if (ganar_nivel)
        {
            if(T != null)
            {
                puntaje = (int)(puntaje + T.tiempo);
                T.restart = true;
            }

            aud.PlayOneShot(Fit, 0.3f);
            
            texto.text = "Puntaje:" + puntaje.ToString();
            Destruir();
            GenerarObjetos();
            

            nivel = nivel + 2;
            ganar_nivel=false;
        }

        if (T.End)
        {
            EScore = puntaje;

            Destruir();
            SceneManager.LoadScene("GameEnd");
        }



    }

    public void GenerarExplosivos()
    {
        if ((nivel > 10) || true)
        {
            for (int i = 0; (i <= (nivel / 10) )|| (i < 2); i++)
            {
                ry = Random.Range(range_spawn_y2, range_spawn_y);
                rx = Random.Range(range_spawn_x2, range_spawn_x);
                RT = Random.Range(0, 2);


                GameObject objeto = Instantiate(Bombas[RT], new Vector3(rx, ry, 0), Quaternion.identity);
                
                
                objetos.Add(objeto);
            }
        }
    }

    public void GenerarObjetos()
    {
        


        for (int i = 0; i < 10 + nivel; i++)
        {


            ry = Random.Range(range_spawn_y2, range_spawn_y);
            rx = Random.Range(range_spawn_x2, range_spawn_x);
            RT = Random.Range(0, 20);

            if (i == 0) 
            {
                
                Casilla = Instantiate(Tipo_casilla[RT], new Vector3(0, -4, 0), Quaternion.identity); ;
               
                ID_Casilla = RT;
            }
            else
            {

                
                if (ID_Casilla == 0 && RT == ID_Casilla) 
                {

                    RT = RT + 1;

                }
                else if(ID_Casilla == 2 && RT == ID_Casilla)
                {
                    RT = RT - 1;
                }
                else if(ID_Casilla == RT)
                {
                    RT = RT - 1;
                }

                

            }

            GameObject objeto = Instantiate(Tipo[RT], new Vector3(rx, ry, 0), Quaternion.identity);

            objetos.Add(objeto);
        }

        GenerarExplosivos();

    }


    public void Destruir() 
    {
        for (int i = objetos.Count - 1; i >= 0; i--)
        {
            if(objetos[i] != null)
            {
                Destroy(objetos[i]);
                objetos.RemoveAt(i);
            }
           
        }

        Destroy(Casilla);

    }

}
