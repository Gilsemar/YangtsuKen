using UnityEngine;
using System.Collections;

public class HeroiScript : MonoBehaviour {
	public int velocidadePercurso,velocidadeRotacao;
	public bool controlePulo,controleGolpe;
	public float forcaPulo;
	public int vida;
	float tempoAtaque;
	bool noAtaque;
	public GameObject Heroi;
	public GUIText vidaTexto;
	// Use this for initialization
	void Start () {
		controleGolpe = false;
		velocidadePercurso = 60;
		velocidadeRotacao = 60;
		controlePulo = true;
		forcaPulo = 10;
		vida = 10;
		noAtaque = false;
		Heroi.animation ["HeroiCorrendo"].speed = 2F;
		Heroi.animation ["HeroiParado"].speed = 2F;
		Heroi.animation ["HeroiAtacando1"].speed = 2F;
	}
	
	// Update is called once per frame
	void Update () {
		this.Controles ();
	}

	void Controles()
	{
		if (!Heroi.animation.IsPlaying ("HeroiAtacando1")) {
						if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
								Heroi.animation.Play ("HeroiCorrendo");
								transform.Translate (0, 0, velocidadePercurso * Time.deltaTime);
						}
						if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.W)) {
								;
								Heroi.animation.Stop ("HeroiCorrendo");
								Heroi.animation.Play ("HeroiParado");
								transform.Translate (0, 0, velocidadePercurso * Time.deltaTime);
						}
		
						if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
							Heroi.animation.Play ("HeroiCorrendo");
							transform.Translate (0, 0, -velocidadePercurso * Time.deltaTime);
						}
		
						if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
								if (!Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.W))
										Heroi.animation.Play ("HeroiParado");

								transform.Rotate (0, velocidadeRotacao * Time.deltaTime, 0);
						}
		
						if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
								if (!Input.GetKey (KeyCode.UpArrow) && !Input.GetKey (KeyCode.W))
										Heroi.animation.Play ("HeroiParado");

								transform.Rotate (0, -velocidadeRotacao * Time.deltaTime, 0);
						}

						if (Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp (KeyCode.S)) {
							Heroi.animation.Stop ("HeroiCorrendo");
							Heroi.animation.Play ("HeroiParado");
							transform.Translate (0, 0, -velocidadePercurso * Time.deltaTime);
						}
		}

		///ATAQUE...
		if ((Input.GetKeyUp(KeyCode.R) || Input.GetMouseButtonUp(0)) && Time.time >= tempoAtaque)
		{
			GameObject.Find("Tahon").animation.Play("HeroiAtacando1");
			tempoAtaque = Time.time;
			//tempoAtaque+= GameObject.Find("Tahon").animation["HeroiAtacando1"].time;
			tempoAtaque+=0.7F;
			controleGolpe = false;
			noAtaque = true;
		}

		if (Time.time >= tempoAtaque) {
			if(noAtaque)
				Heroi.animation.Play ("HeroiParado");
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
			if (other.tag == "Inimigo1" && !controleGolpe && noAtaque) {
						//Debug.Log ("pontuou!!!");
						other.GetComponent<Inimigo1Script> ().RecebeDano (1);
						controleGolpe = true;
				}

			if (other.tag == "Chefe" && !controleGolpe && noAtaque) {
				//Debug.Log ("pontuou!!!");
				other.GetComponent<ChefeScript>().RecebeDano(1);
				controleGolpe = true;
		}
	}


	public void RecebeDano(int quantidade)
	{
		vidaTexto.text = string.Concat ("VIDA ", vida);
		vida -= quantidade;
		if(vida <=0)
		{
			Application.LoadLevel("GameOver");

		}
		
	}
}
