using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;


public class Selectmenu : MonoBehaviour
{

    public TextMeshProUGUI Tstart;
    public TextMeshProUGUI Tquit;
    public TextMeshProUGUI TH1;
    public TextMeshProUGUI TH2;
    public TextMeshProUGUI TH3;
    public TextMeshProUGUI TH4;
    public TextMeshProUGUI TH5;

    public int control = 0;
    public bool change;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Tstart = GameObject.Find("Start").GetComponent<TextMeshProUGUI>();
        Tquit = GameObject.Find("Exit").GetComponent<TextMeshProUGUI>();
        TH1 = GameObject.Find("HighScore1").GetComponent<TextMeshProUGUI>();
        TH2 = GameObject.Find("HighScore2").GetComponent<TextMeshProUGUI>();
        TH3 = GameObject.Find("HighScore3").GetComponent<TextMeshProUGUI>();
        TH4 = GameObject.Find("HighScore4").GetComponent<TextMeshProUGUI>();
        TH5 = GameObject.Find("HighScore5").GetComponent<TextMeshProUGUI>();
        change = true;
        TH1.text = "1:" + End_result.HS1 + " " + End_result.HSN1;
        TH2.text = "2:" + End_result.HS2 + " " + End_result.HSN2;
        TH3.text = "3:" + End_result.HS3 + " " + End_result.HSN3;
        TH4.text = "4:" + End_result.HS4 + " " + End_result.HSN4;
        TH5.text = "5:" + End_result.HS5 + " " + End_result.HSN5;
    }

    // Update is called once per frame
    void Update()
    {
        if (control == 0 && change == true)
        {
            Tstart.color = Color.yellow;
            Tquit.color = Color.white;
            change = false;
        }
        else if(control == 1 && change == true)
        {
            Tquit.color = Color.yellow;
            Tstart.color = Color.white;
            change = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && control == 0)
        {
            control = 1;
            change = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && control == 1)
        {
            control = 0;
            change = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (control == 0) 
            {
                SceneManager.LoadScene("Campo_de_busqueda");
            }

            if (control == 1)
            {
                Application.Quit();
            }

        }

    }
}
