using UnityEngine;
using System.Collections;

public class HeroiControle : MonoBehaviour {

	public float forcaPulo;
	public int velocidadeDePercurso, velocidadeRotacao;
	public bool controlePulo;

	// Use this for initialization
	void Start () {
		velocidadeDePercurso = 15;
		controlePulo = true;
		forcaPulo = 10;
		velocidadeRotacao = 70;
	}
	
	// Update is called once per frame
	void Update () {
		this.Controles ();
	}

	void Controles()
	{
		controlePulo = Physics.Raycast (transform.position, -Vector3.up, 5F);

		if (Input.GetKey (KeyCode.UpArrow)||Input.GetKey (KeyCode.W)) {
			//GameObject.Find ("mario").animation.Play ("Andando");
			transform.Translate (velocidadeDePercurso * Time.deltaTime,0 , 0);
		}
		
		if (Input.GetKey (KeyCode.DownArrow)||Input.GetKey (KeyCode.S)){
			//GameObject.Find ("mario").animation.Play ("Andando");
			transform.Translate (-velocidadeDePercurso * Time.deltaTime,0, 0);
		}
		
		if (Input.GetKey (KeyCode.RightArrow)||Input.GetKey (KeyCode.D)) {
			//GameObject.Find ("mario").animation.Play ("Andando");
			transform.Rotate (0, velocidadeRotacao * Time.deltaTime, 0);
		}
		
		if (Input.GetKey (KeyCode.LeftArrow)||Input.GetKey (KeyCode.A)) {
			//GameObject.Find ("mario").animation.Play ("Andando");
			transform.Rotate (0, -velocidadeRotacao * Time.deltaTime, 0);
		}
		
		///Pulo
		if (controlePulo) {
			this.rigidbody.velocity = new Vector3(0, Input.GetAxis("Jump") * forcaPulo, 0);
		}
		
		
		/// Colocar animaçoes
		if (!controlePulo) {
			if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.W)) {
				//GameObject.Find ("mario").animation.Stop ();
			}
			
			if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
				//GameObject.Find ("mario").animation.Stop ();
			}
			
			if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D)) {
				//GameObject.Find ("mario").animation.Stop ();
			}
			
			if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.A)) {
				//GameObject.Find ("mario").animation.Stop ();
			}
		}
	}


	void OnCollisionEnter (Collision collider)
	{
		//if (collider.gameObject.tag == "Chao") {
		//	controlePulo = false;		
		//}
	}
}
