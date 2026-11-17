using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
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

    // --- Control unificado de selección (Dragger o Explosive) ---
    private GameObject objetoSeleccionadoActual;
    private int maxSortingOrder = 0;
    private Camera mainCamera;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        GenerarObjetos();
        
        nivel = 0;
        texto = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            IntentarSeleccionarObjetoSuperior();
        }

        if (Input.GetMouseButtonUp(0) && objetoSeleccionadoActual != null)
        {
            if (objetoSeleccionadoActual.TryGetComponent(out Dragger dragger))
            {
                dragger.Soltar();
            }
            else if (objetoSeleccionadoActual.TryGetComponent(out Explosive explosive))
            {
                explosive.Soltar();
            }

            objetoSeleccionadoActual = null;
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


    private void IntentarSeleccionarObjetoSuperior()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        if (hits.Length == 0) return;

        GameObject mejorObjeto = null;
        int mayorLayer = int.MinValue;
        int mayorOrder = int.MinValue;
        float menorZ = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            // Verificar si el collider pertenece a un Dragger o a un Explosive
            Dragger dragger = hit.GetComponent<Dragger>();
            if (dragger == null) dragger = hit.GetComponentInParent<Dragger>();

            Explosive explosive = hit.GetComponent<Explosive>();
            if (explosive == null) explosive = hit.GetComponentInParent<Explosive>();

            // Determinar el GameObject raíz arrastrable (si no tiene ninguno, ignorar)
            GameObject candidato = null;
            if (dragger != null) candidato = dragger.gameObject;
            else if (explosive != null) candidato = explosive.gameObject;

            if (candidato == null) continue;

            ObtenerInfoOrden(candidato, out int layerVal, out int orderVal);
            float zPos = candidato.transform.position.z;

            if (layerVal > mayorLayer ||
               (layerVal == mayorLayer && orderVal > mayorOrder) ||
               (layerVal == mayorLayer && orderVal == mayorOrder && zPos < menorZ))
            {
                mayorLayer = layerVal;
                mayorOrder = orderVal;
                menorZ = zPos;
                mejorObjeto = candidato;
            }
        }

        if (mejorObjeto != null)
        {
            objetoSeleccionadoActual = mejorObjeto;
            TraerAlFrente(mejorObjeto);

            if (mejorObjeto.TryGetComponent(out Dragger d))
            {
                d.Agarrar();
            }
            else if (mejorObjeto.TryGetComponent(out Explosive e))
            {
                e.Agarrar();
            }
        }
    }

    private void TraerAlFrente(GameObject obj)
    {
        ObtenerInfoOrden(obj, out _, out int ordenActual);

        if (ordenActual == maxSortingOrder && maxSortingOrder > 0) return;

        maxSortingOrder++;

        if (maxSortingOrder >= 32000)
        {
            ReordenarCapasLimpias();
            maxSortingOrder++;
        }

        AsignarSortingOrder(obj, maxSortingOrder);
    }

    private void ObtenerInfoOrden(GameObject obj, out int layerValue, out int orderInLayer)
    {
        if (obj.TryGetComponent(out SortingGroup sg))
        {
            layerValue = SortingLayer.GetLayerValueFromID(sg.sortingLayerID);
            orderInLayer = sg.sortingOrder;
            return;
        }

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.GetComponentInParent<SpriteRenderer>();

        if (sr != null)
        {
            layerValue = SortingLayer.GetLayerValueFromID(sr.sortingLayerID);
            orderInLayer = sr.sortingOrder;
            return;
        }

        layerValue = 0;
        orderInLayer = 0;
    }

    private void AsignarSortingOrder(GameObject obj, int nuevoOrden)
    {
        if (obj.TryGetComponent(out SortingGroup sg))
        {
            sg.sortingOrder = nuevoOrden;
            return;
        }

        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) sr = obj.GetComponentInParent<SpriteRenderer>();

        if (sr != null)
        {
            sr.sortingOrder = nuevoOrden;
        }
    }

    private void ReordenarCapasLimpias()
    {
        objetos.RemoveAll(item => item == null);
        objetos.Sort((a, b) =>
        {
            ObtenerInfoOrden(a, out _, out int ordenA);
            ObtenerInfoOrden(b, out _, out int ordenB);
            return ordenA.CompareTo(ordenB);
        });

        for (int i = 0; i < objetos.Count; i++)
        {
            AsignarSortingOrder(objetos[i], i + 1);
        }

        maxSortingOrder = objetos.Count;
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
