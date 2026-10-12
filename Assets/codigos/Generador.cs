using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Generador : MonoBehaviour
{

    //los que aparecen
    public List <GameObject> objetos;
    

    public float time;

    public int num = 0;
    public int R = 0;
    public int RT = 0;
    public int ID_Casilla;
    //distintos tipos de objetos
    public List <GameObject> Tipo;
    public List<GameObject> Tipo_casilla;

    public GameObject Casilla;

    public float range_spawn_x = 3;
    public float range_spawn_x2 = -3;
    public float range_spawn_y = 2;
    public float range_spawn_y2 = -2;
    public int nivel;

    public float ry;
    public float rx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerarObjetos();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Destruir();
        }


    }

    public void GenerarObjetos()
    {
        


        for (int i = 0; i < 4; i++)
        {


            ry = Random.Range(range_spawn_y2, range_spawn_y);
            rx = Random.Range(range_spawn_x2, range_spawn_x);
            RT = Random.Range(0, 3);

            if (i == 0) 
            {
                
                Casilla = Tipo_casilla[RT];
                Instantiate(Casilla, new Vector3(2, 2, 0), Quaternion.identity);
                ID_Casilla = RT;
            }
            else
            {

                R = Random.Range(0, 2);
                if (R == 0) 
                {
                    RT = Random.Range(0,ID_Casilla);


                }
                else
                {
                    RT = Random.Range(ID_Casilla + 1, 3);
                }

                if(ID_Casilla == 0)
                {
                    RT = Random.Range(1, 3);
                }

            }

            GameObject objeto = Instantiate(Tipo[RT], new Vector3(rx, ry, 0), Quaternion.identity);

            objetos.Add(objeto);
        }


    }


    public void Destruir() 
    {
        for (int i = objetos.Count - 1; i >= 0; i--)
        {
            Destroy(objetos[i]);
            objetos.RemoveAt(i);
            Destroy(Casilla);



        }

    }

}
