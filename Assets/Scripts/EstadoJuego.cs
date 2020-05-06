using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EstadoJuego : MonoBehaviour
{
    public static EstadoJuego estadoJuego;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void Awake()
    {
        estadoJuego = this;
        DontDestroyOnLoad(gameObject);
    }

    public void GuiarAula (int id)
    {
        Quaternion rotation = gameObject.transform.rotation;
        double primerPiso = 2.0;
        double segundoPiso = 8.43;
        double x;
        double y;
        double z;
        if (id < 30)
        {
            y = primerPiso;
        }
        else
        {
            y = segundoPiso;
        }
        switch (id)
        {
            /*Primer piso*/
            case (11):
                x = 34; z = 24; break;
            case (12):
                x = 25; z = 24; break;
            case (13):
                x = 7; z = 24; break;
            case (14):
                x = -12; z = 45.8; break;
            case (15):
                x = -41; z = 45.8; break;
            case (16):
                x = -10; z = 21; break;
            case (17):
                x = -10; z = 7; break;
            case (18):
                x = -10; z = -10; break;
            case (19):
                x = -10; z = -23; break;
            case (20):
                x= -2.5; z = -40; break;
            case (21):
                x = 25; z = 5; break;
            case (22):
                x = 20.5; z = 94; break;
            /*Segundo piso*/
            case (31):
                x = -12; z = -31; break;
            case (32):
                x = -12; z = -12; break;
            case (33):
                x = -12; z = 6; break;
            case (34):
                x = -12; z = 26; break;
            case (35):
                x = 7; z = 26; break;
            case (36):
                x = 20;z = 26;break;
            case (37):
                x = 34; z = 26; break;
            default:
                x = 34; z = 24; break;
        }

        Vector3 position = new Vector3( (float)x, (float)y, (float)z);
        
        gameObject.transform.SetPositionAndRotation(position, rotation);
    }
}
