using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {
    public GameObject prefab;
    public Transform player;
    public LayerMask noTransitable;
    public Vector2 tamañoGrid;
    public float radio_nodo;
    public float diferencia;
    public float nivel;
    Nodo[,] grid;
    float diametroNodo;
    int tamañoX, tamañoY;
    public List<Nodo> path;
    private void Start()
    {
        diametroNodo = radio_nodo * 2;
        tamañoX = Mathf.RoundToInt(tamañoGrid.x / diametroNodo);
        tamañoY = Mathf.RoundToInt(tamañoGrid.y / diametroNodo);
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Nodo[tamañoX, tamañoY];
        Vector3 inferiorIzq = transform.position - Vector3.right * tamañoGrid.x / 2 - Vector3.forward * tamañoGrid.y / 2;
        for(int x= 0; x<tamañoX; x++){
            for (int y = 0; y < tamañoY; y++)
            {
                Vector3 punto = inferiorIzq + Vector3.right * (x * diametroNodo + radio_nodo) 
                    + Vector3.forward * (y * diametroNodo + radio_nodo);
                bool transitable = !(Physics.CheckSphere(punto,radio_nodo,noTransitable)); // la magia
                grid[x, y] = new Nodo(transitable, punto,x,y);
            }
        }
    }
    public List<Nodo> GetVecinos(Nodo n)
    {
        List<Nodo> vecinos = new List<Nodo>();

        for (int x = -1; x<=1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int cheackX = n.gridX + x;
                int cheackY = n.gridY + y;

                if(cheackX >= 0 && cheackX <tamañoX  && cheackY >= 0 && cheackY < tamañoY)
                {
                    vecinos.Add(grid[cheackX, cheackY]);
                }
            }
        }
        return vecinos;
    }
    public Nodo puntoDelNodo(Vector3 posicion)
    {
        float porcentajeX = (posicion.x + tamañoGrid.x / 2) / tamañoGrid.x;
        float porcentajeY = (posicion.z + tamañoGrid.y / 2) / tamañoGrid.y;
        porcentajeX = Mathf.Clamp01(porcentajeX);//Sujeta el valor entre 0 y 1 y devuelve el valor.
        porcentajeY = Mathf.Clamp01(porcentajeY);

        int x = Mathf.RoundToInt((tamañoX-1) * porcentajeX);
        int y = Mathf.RoundToInt((tamañoY - 1) * porcentajeY);

        return grid[x, y];
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(tamañoGrid.x, 1, tamañoGrid.y));
        if (grid != null)
        {
            Nodo playerNode = puntoDelNodo(player.position);
            foreach(Nodo n in grid)
            {
                Gizmos.color = (n.transitable) ? Color.white : Color.red;
                if (path != null)
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                //Gizmos.color = (n.transitable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.nodo_Posicion, Vector3.one * (diametroNodo - .1f));
            }
        }
    }
    public void showRecorrido()
    {
        if (grid != null)
        {
            Nodo playerNode = puntoDelNodo(player.position);
            foreach (Nodo n in grid) {
                if( nivel == 1)
                {
                    if (player.position.y < 6)
                    {
                        if (path != null) {
                            if (path.Contains(n))
                            {
                                float diferenciaX = n.nodo_Posicion.x - playerNode.nodo_Posicion.x;
                                float diferenciaZ = n.nodo_Posicion.z - playerNode.nodo_Posicion.z;
                                Vector3 position = n.nodo_Posicion;
                                position.y = position.y + 1;
                                diferencia = diferenciaZ;
                                if (diferenciaX == 5 || diferenciaX == -5 || diferenciaZ == 5 || diferenciaZ == -5)
                                {
                                    
                                    GameObject circle = Instantiate(prefab, position, transform.rotation);
                                    Destroy(circle, 0.2f);
                                    //Quaternion rotacion = Quaternion.LookRotation(position);
                                    //prefab.transform.rotation = Quaternion.Slerp(prefab.transform.rotation, rotacion, 3f * Time.deltaTime);
                                }
                                else if(diferenciaX == 8 || diferenciaX == -8 || diferenciaZ == 8 || diferenciaZ == -8)
                                { 
                                    GameObject circle = Instantiate(prefab, position, transform.rotation);
                                    Destroy(circle, 0.2f);
                                }

                            }
                        }
                    }
                }
                else if(nivel == 2)
                {
                    if (player.position.y > 5)
                    {
                        if (path != null)
                        {
                            if (path.Contains(n))
                            {
                                float diferenciaX = n.nodo_Posicion.x - playerNode.nodo_Posicion.x;
                                float diferenciaZ = n.nodo_Posicion.z - playerNode.nodo_Posicion.z;
                                Vector3 position = n.nodo_Posicion;
                                position.y = position.y + 1;
                                if (diferenciaX == 3 || diferenciaX == -3 || diferenciaZ == 3 || diferenciaZ == -3)
                                {
                                    
                                    GameObject circle = Instantiate(prefab, position, transform.rotation);
                                    Destroy(circle, 0.5f);
                                    //Quaternion rotacion = Quaternion.LookRotation(position);
                                    //prefab.transform.rotation = Quaternion.Slerp(prefab.transform.rotation, rotacion, 3f * Time.deltaTime);
                                }
                                else if (diferenciaX == 6 || diferenciaX == -6 || diferenciaZ == 6 || diferenciaZ == -6)
                                {
                                    GameObject circle = Instantiate(prefab, position, transform.rotation);
                                    Destroy(circle, 0.5f);
                                }
                                else if (diferenciaX == 9 || diferenciaX == -9 || diferenciaZ == 9 || diferenciaZ == -9)
                                {
                                    GameObject circle = Instantiate(prefab, position, transform.rotation);
                                    Destroy(circle, 0.5f);
                                }

                            }
                        }
                    }
                }
                

            }
        }
    }

}
