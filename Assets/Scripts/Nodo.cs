using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodo {
    public bool transitable;
    public Vector3 nodo_Posicion;
    public int gridX,gridY;
    public Nodo parent;

    public int gCosto, hCost;

    public Nodo(bool _transitable, Vector3 _posicion, int _gridX, int _gridY)
    {
        this.transitable = _transitable;
        this.nodo_Posicion = _posicion;
        this.gridX = _gridX;
        this.gridY = _gridY;
    }
    public int fCost
    {
        get
        {
            return gCosto + hCost;
        }
    }
}
