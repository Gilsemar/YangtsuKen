using UnityEngine;
using System.Collections;

public class HeroiScript : MonoBehaviour {
	public int velocidadePercurso,velocidadeRotacao;
	public bool controlePulo,controleGolpe;
	public float forcaPulo;
	public int vida;
	float tempoAtaque;
	bool noAtaque;
	// Use this for initialization
	void Start () {
		controleGolpe = false;
		velocidadePercurso = 40;
		velocidadeRotacao = 60;
		controlePulo = true;
		forcaPulo = 10;
		vida = 10;
		noAtaque = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.Controles ();
	}

	void Controles()
	{
		if (!GameObject.Find ("Tahon").animation.IsPlaying ("HeroiAtacando1")) {
						if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
								GameObject.Find ("Tahon").animation.Play ("HeroiCorrendo");
								transform.Translate (0, 0, velocidadePercurso * Time.deltaTime);
						}
						if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.W)) {
								;
								GameObject.Find ("Tahon").animation.Stop ("HeroiCorrendo");
								GameObject.Find ("Tahon").animation.Play ("HeroiParado");
								transform.Translate (0, 0, velocidadePercurso * Time.deltaTime);
						}
		
						if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
								//GameObject.Find ("mario").animation.Play ("Andando");
								//transform.Translate (0 , 0, velocidadePercurso * Time.deltaTime);
						}
		
						if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
								if (!Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.W))
										GameObject.Find ("Tahon").animation.Play ("HeroiParado");

								transform.Rotate (0, velocidadeRotacao * Time.deltaTime, 0);
						}
		
						if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
								if (!Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.W))
										GameObject.Find ("Tahon").animation.Play ("HeroiParado");

								transform.Rotate (0, -velocidadeRotacao * Time.deltaTime, 0);
						}
		}

		///ATAQUE...
		if ((Input.GetKeyUp(KeyCode.R) || Input.GetMouseButtonUp(0)) && Time.time >= tempoAtaque)
		{
			GameObject.Find("Tahon").animation.Play("HeroiAtacando1");
			tempoAtaque = Time.time;
			//tempoAtaque+= GameObject.Find("Tahon").animation["HeroiAtacando1"].time;
			tempoAtaque+=1.5F;
			controleGolpe = false;
			noAtaque = true;
		}

		if (Time.time >= tempoAtaque) {
			noAtaque = false;
		}
	
	}


	void ControlesVelho()
	{
		transform.Translate (0, 0,Input.GetAxis("Vertical") * velocidadePercurso * Time.deltaTime);
		transform.Rotate (0, Input.GetAxis("Horizontal") *  velocidadeRotacao * Time.deltaTime, 0);
		transform.Rotate (0, Input.GetAxis("Mouse X") *  velocidadeRotacao * Time.deltaTime * 2, 0);

		controlePulo = Physics.Raycast (transform.position, -Vector3.up, 5F);
		if (controlePulo) {
			this.rigidbody.velocity = new Vector3(0, Input.GetAxis("Jump") * forcaPulo, 0);
		}

		if (Input.GetKeyUp(KeyCode.R) || Input.GetMouseButtonUp(0))
		{
			controleGolpe = false;
		}
	}

	void OnTriggerStay (Collider other) {
		//Debug.Log ("na colisao");
		//Debug.Log ("controleGolpe " + controleGolpe);
		//Debug.Log ("noAtaque " + noAtaque);


			if (other.tag == "Inimigo1" && !controleGolpe && noAtaque) {
						Debug.Log ("pontuou!!!");
						other.GetComponent<Inimigo1Script> ().RecebeDano (1);
						controleGolpe = true;
				}

			if (other.tag == "Chefe" && !controleGolpe && noAtaque) {
				Debug.Log ("pontuou!!!");
				other.GetComponent<ChefeScript>().RecebeDano(1);
				controleGolpe = true;

		}
	}


	public void RecebeDano(int quantidade)
	{
		vida -= quantidade;
		if(vida <=0)
		{
			Application.LoadLevel("GameOver");

		}
		
	}
}
