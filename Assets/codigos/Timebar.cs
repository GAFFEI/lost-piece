using System.Globalization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timebar : MonoBehaviour
{

    public UnityEngine.UI.Image relleno;
    public float maxtime;
    public float tiempo;

    public bool restart = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxtime = 100;
        tiempo = 100;
    }

    // Update is called once per frame
    void Update()
    {
        tiempo -= Time.deltaTime;

        relleno.fillAmount = tiempo / maxtime;

        if (restart)
        {
            restore();
            restart = false;
        }

    }

  
    public void restore()
    {

        if(maxtime > 50)
        {
            maxtime = maxtime - 1;
        }
        

        tiempo = maxtime;
    }







}
