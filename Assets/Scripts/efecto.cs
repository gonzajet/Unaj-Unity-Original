using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class efecto : MonoBehaviour {

    public Material mat1, mat2, mat3;
    public Renderer rend;
    private float cont=0f;
	// Use this for initialization
	void Start () {
        rend=GetComponent<Renderer>();
        cont = Random.Range(0, 2);
        if (cont == 0)
            rend.material = mat3;
        else if (cont==1)
            rend.material = mat2;
        else if (cont == 2)
            rend.material = mat1;
    }
	
	// Update is called once per frame
	void Update () {

    }
}
