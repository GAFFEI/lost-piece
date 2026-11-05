using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_result : MonoBehaviour
{


    public TextMeshProUGUI Name;
    public TextMeshProUGUI End_points;

    public int Received;

    public static int HS1 = 0;
    public static int HS2 = 0;
    public static int HS3 = 0;
    public static int HS4 = 0;
    public static int HS5 = 0;
    public static string HSN1 = "";
    public static string HSN2 = "";
    public static string HSN3 = "";
    public static string HSN4 = "";
    public static string HSN5 = "";



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Received = Generador.EScore;
        End_points.text = ""+Received;




    }

    // Update is called once per frame
    void Update()
    {

        if (Name.text.Length == 4 && Input.GetKeyDown(KeyCode.Return))
        {
            if(Received >= HS1)
            {
                HS5 = HS4;
                HSN5 = HSN4;
                HS4 = HS3;
                HSN4 = HSN3;
                HS3 = HS2;
                HSN3 = HSN2;
                HS2 = HS1;
                HSN2 = HSN1;
                HS1 = Received;
                HSN1 = Name.text;
                SceneManager.LoadScene("Menu");
            }
            else if(Received >= HS2)
            {
                HS5 = HS4;
                HSN5 = HSN4;
                HS4 = HS3;
                HSN4 = HSN3;
                HS3 = HS2;
                HSN3 = HSN2;
                HS2 = Received;
                HSN2 = Name.text;
                SceneManager.LoadScene("Menu");



            }
            else if(Received >= HS3)
            {
                HS5 = HS4;
                HSN5 = HSN4;
                HS4 = HS3;
                HSN4 = HSN3;
                HS3 = Received;
                HSN3 = Name.text;
                SceneManager.LoadScene("Menu");
            }
            else if(Received >= HS4)
            {
                HS5 = HS4;
                HSN5 = HSN4;
                HS4 = Received;
                HSN4 = Name.text;
                SceneManager.LoadScene("Menu");
            }
            else if(Received >= HS5)
            {
                HS5 = Received;
                HSN5 = Name.text;
                SceneManager.LoadScene("Menu");
            }
            else
            {
                SceneManager.LoadScene("Menu");
            }



        }
        
    }
}
