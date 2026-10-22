using System.Globalization;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Timebar : MonoBehaviour
{

    public UnityEngine.UI.Image relleno;
    float maxtime;
    public static float tiempo;

    public int Num;



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


    }

    public static float getTime()
    {
        return tiempo;
    }

    public void restore(int Num)
    {
        maxtime = maxtime - Num;

        tiempo = maxtime;
    }







}
