using System;
using UnityEngine;

public class Background: MonoBehaviour
{
    //"ciclado" se usa para determinar si background cicla por tiempo, o si no lo hace
    //si ciclado = true, entonces ciclara cada cierto tiempo

    public bool ciclado;
    public Sprite[] backgrounds;
    public SpriteRenderer backgroundRenderer;

    public void BackgroundChange()
    {
        int index = UnityEngine.Random.Range(0, backgrounds.Length);

        if (index >= 0 && index < backgrounds.Length)
        {
            backgroundRenderer.sprite = backgrounds[index];
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (ciclado)
        {
            InvokeRepeating("BackgroundChange", 0f, 10f);
        }
        else
        {
            BackgroundChange();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
