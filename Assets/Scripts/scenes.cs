using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class scenes : MonoBehaviour {
    
    public void toSimulation (int numEscena) {
        SceneManager.LoadScene(numEscena);
	}
}
