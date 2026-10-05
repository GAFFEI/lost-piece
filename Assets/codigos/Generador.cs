using UnityEngine;
using UnityEngine.SceneManagement;

public class Generador : MonoBehaviour
{


    public GameObject[] objetos;
    public float tiempoentreenemigo;
    public float terminar_nivel;

    public float[] time;

    public int num = 0;
    public GameObject[] Tipo;

    public float range_spawn_x = 8;
    public float range_spawn_x2 = -8;
    public float range_spawn_y = 8;
    public float range_spawn_y2 = -8;
    public int nivel;

    public float r;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (num < objetos.Length)
        {
            r = Random.Range(range_spawn_y2, range_spawn_y);

            Instantiate(objetos[num], new Vector3(r,0,0) , Quaternion.identity);
            
            num++;
        }


    }
}
