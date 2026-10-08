using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Generador : MonoBehaviour
{

    //los que aparecen
    public List <GameObject> objetos;
    

    public float[] time;

    public int num = 0;
    //distintos tipos de objetos
    public List <GameObject> Tipo;

    public float range_spawn_x = 3;
    public float range_spawn_x2 = -3;
    public float range_spawn_y = 3;
    public float range_spawn_y2 = -3;
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
        


        for (int i = 0; i < Tipo.Count; i++)
        {


            ry = Random.Range(range_spawn_y2, range_spawn_y);
            rx = Random.Range(range_spawn_x2, range_spawn_x);

            GameObject objeto = Instantiate(Tipo[i], new Vector3(rx, ry, 0), Quaternion.identity);

            objetos.Add(objeto);
        }


    }


    public void Destruir() 
    {
        for (int i = objetos.Count - 1; i >= 0; i--)
        {
            Destroy(objetos[i]);
            objetos.RemoveAt(i);
           

            

        }

    }

}
