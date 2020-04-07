using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controlador : MonoBehaviour {

	public void CambiarEscena(string nombre){
	
		print ("Cambiar a la esena " + nombre);
		SceneManager.LoadScene (nombre);
		PlayerController.Reset ();
	}

	public void Salir(){
	
		print ("Salir del Juego");
		Application.Quit ();
	}


	public void RestarGame()
	{if (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0)) {
			SceneManager.LoadScene ("Level_01");
		}


	}
}
