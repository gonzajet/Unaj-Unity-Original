using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASearch : MonoBehaviour {
    Grid grid;
    public Transform seeker, target;
    public Camera FPSCamara;
    public float horizontalSpeed;
    public float verticalSpeed;
    public Vector3 destinoPos;

    float H;
    float V;
    public GameObject PB,P1;
    Nodo nodoInicio, nodoDestino;
    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Start()
    {
        GameObject destino= GameObject.Find("Destino2");
        if (destino != null)
        {
            destinoPos=destino.transform.position;
        }
    }
    private void Update()
    {
        Asearch(seeker.position, target.position);
        H = horizontalSpeed * Input.GetAxis("Mouse X");
        V = verticalSpeed * Input.GetAxis("Mouse Y");

        seeker.transform.Rotate(0, H, 0);
        FPSCamara.transform.Rotate(-V, 0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            seeker.transform.Translate(0, 0, 0.2f);
            grid.showRecorrido();
        }


        else if (Input.GetKey(KeyCode.S))
        {
            seeker.transform.Translate(0, 0, -0.2f);
            grid.showRecorrido();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            seeker.transform.Translate(0, 0, 0.5f);
            grid.showRecorrido();
        }
        
    }
    private void LateUpdate()
    {
        
    }

    void Asearch(Vector3 Posinicio, Vector3 Posdestino)
    {
        if (this.destinoPos != null)
        {
            Posdestino = destinoPos;
        }
        nodoInicio = grid.puntoDelNodo(Posinicio);
        nodoDestino = grid.puntoDelNodo(Posdestino);
        if (Posdestino.y > 7 && Posinicio.y < 7 ){//Player esta abajo obj arriba
            nodoDestino = grid.puntoDelNodo(PB.transform.position);
            //Posdestino = rampa.transform.GetChild(0).position;
        }
        else if(Posdestino.y < 7 && Posinicio.y > 7)//Player esta arriba obj abajo
        {
            nodoDestino = grid.puntoDelNodo(P1.transform.position);
            
        }
        /*else{
            rampa = GameObject.Find("UniversidadPiso2/Rampa");
            int componentesRampa = rampa.transform.childCount;
            for (int i = 1; i <= componentesRampa; i++)
                rampa.transform.GetChild(i).GetComponent<Renderer>().material.color = Color.white;
        }*/

        List<Nodo> abiertos = new List<Nodo>();
        HashSet<Nodo> cerrados = new HashSet<Nodo>();
        abiertos.Add(nodoInicio);

        while(abiertos.Count > 0)
        {
            Nodo actual = abiertos[0];
            for (int i = 1; i < abiertos.Count; i++)
            {
                if (abiertos[i].fCost < actual.fCost 
                    || abiertos[i].fCost == actual.fCost && abiertos[i].hCost < actual.hCost)
                //si los costos son menor o el costo es igual y el heuristico es menor.
                {
                    actual = abiertos[i];
                }
            }
            abiertos.Remove(actual);
            cerrados.Add(actual);
            if (actual == nodoDestino)
            {
                RetracePath(nodoInicio, nodoDestino);
                return;
            }
                
            foreach(Nodo vecino in grid.GetVecinos(actual))
            {
                if (!vecino.transitable || cerrados.Contains(vecino))
                    continue;
                int newMovCostDeVecino = actual.gCosto + GetDistancia(actual, vecino);
                if(newMovCostDeVecino < vecino.gCosto || !abiertos.Contains(vecino))
                {
                    vecino.gCosto = newMovCostDeVecino;
                    vecino.hCost = GetDistancia(vecino, nodoDestino);
                    vecino.parent = actual;
                    if (!abiertos.Contains(vecino))
                        abiertos.Add(vecino);
                }
            }
            
        }

    }
    void RetracePath(Nodo startNode, Nodo endNode)
    {
        List<Nodo> path = new List<Nodo>();
        Nodo currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        path.Reverse();

        grid.path = path;
        
    }

    private int GetDistancia(Nodo a, Nodo b)
    {
        
        int distX = Mathf.Abs(a.gridX - b.gridX);
        int distY = Mathf.Abs(a.gridY - b.gridY);
        if (distX > distY)
          return 14 * distY + 10 * (distX-distY);
        return 14 * distX + 10 * (distY - distX);
        //return 10 * (distX + distY) + (12 * 10) * Mathf.Min(distX, distY);  //manhatan
        //return 10 * (distX + distY); // manhatan diagonal
        //return 10*(int) Mathf.Sqrt(distX*distX + distY*distY); //euclidiana
    }
}
