using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PleyerController : MonoBehaviour {

	//si esta mirando a la izquierda o a la derecha
	public enum FACE_DIRECTION
	{
		LEFT = -1,
		RIGHT = 1
	}

	public FACE_DIRECTION direction = FACE_DIRECTION.RIGHT; //inicia mirando a la derecha


	public static PleyerController player = null; //acceder al jugador

	public bool canJump = true; // si ya salto no salta mas
	public bool canMove = true;
	public bool isOnTheGround = false; //al inicio esta volando tiempo a aterrisar en el suelo

	//modificar la fuersa del salto
	public float jumpPower = 600; //saltar fuersa
	public float jumpTimeout = 1.0f; // tiempo entre el salto (avilitarse)
	public float maxSpeed = 30.0f; // velocidad maxima del personaje en pixeles)

	//movimiento
	public string horizontalAxis = "Horizontal";
	public string verticalAxis = "Vertical";
	public string jumpBotton = "Jump";


	private Rigidbody2D theRigibody = null; //saber como esta el cuerpo fisico de cosalta etc
	private Transform theTransform = null; // acceder a la posicion
	public BoxCollider2D feetCollider = null; // si esta tocando o no el suelo
	public LayerMask groundLayer; //manejar la capa del suelo para los objetos que esten en el suelo

	//vida del personaje
	public static float Health {
		get {
			return _health;
		}

		set {
			_health = value;

			if (_health <= 0) {
				Die();
			}
		}
	}

	//cantidad de la vida
	[SerializeField]
	private static float _health = 100.0f;

	void Awake(){
		theRigibody = GetComponent<Rigidbody2D> ();
		theTransform = GetComponent<Transform> ();
	}

	//para saltar
	private void Jump(){
		if (!isOnTheGround || !canJump)   //si no esta en el suelo o no puede saltar
			return;   //no hace nada
		theRigibody.AddForce (Vector2.up * jumpPower);//aplicar fuersa al salto
		canJump = false;   //despues de saltar ya no podra saltar
		Invoke ("EnableJump", jumpTimeout);  //activar salto
	}
	//despues de saltar ya podra saltar
	private void EnableJump(){
		canJump = true;
	}

	//para  aterrizar
	private bool GetGrounded(){
		// esta o no esta en el suelo
		Vector2 boxCenter = new Vector2 (theTransform.position.x, theTransform.position.y) + feetCollider.offset;
		//comprobar si estoy chocando
		Collider2D[] hitCollider = Physics2D.OverlapAreaAll(boxCenter, feetCollider.size, groundLayer);
		//SI ESTA COLICIONANDO CON ALGUIEN
		if (hitCollider.Length > 0) {
			return true;
		} else {
			return false;
		}
	}


	//para girar el personaje
	private void ChangeDirection(){
		//cambiar la direccion
		direction = (FACE_DIRECTION)((int)direction * -1.0f);
		//cambiar a donde va mirar
		Vector3 localScale = theTransform.localScale;
		localScale.x *= -1.0f;
		theTransform.localScale = localScale;
	}

	//me puedo no no me puedo mover
	void FixedUpdate(){
		if (!canMove || Health <= 0)
			return;

		isOnTheGround = GetGrounded();

		float horizontal = CrossPlatformInputManager.GetAxis (horizontalAxis);
		theRigibody.AddForce (Vector2.right * horizontal * maxSpeed);//aplicar fuerza a en la horizontal


		//girar el personaje
		if ((horizontal < 0 && direction != FACE_DIRECTION.LEFT) || (horizontal > 0 && direction != FACE_DIRECTION.RIGHT))
			ChangeDirection ();//CAMBIAR LA DIRECCION 

		if (CrossPlatformInputManager.GetButton (jumpBotton)) {//si estamos saltoando pulsando boton
			Jump();
		}

		theRigibody.velocity = new Vector2(
			Mathf.Clamp(theRigibody.velocity.x, -maxSpeed, maxSpeed),//velocidad
		    Mathf.Clamp(theRigibody.velocity.y, -Mathf.Infinity, jumpPower)//velocidad
			);

	}


	void OnDestroy(){
		player = null;
	}

	static void Die(){
		Destroy (PleyerController.player.gameObject);
	}


	public static void Reset(){
		Health = 100.0f;
	}

}
