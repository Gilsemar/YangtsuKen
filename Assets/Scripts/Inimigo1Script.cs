using UnityEngine;
using System.Collections;

public class Inimigo1Script : MonoBehaviour {

	public int Velocidade;
	private Vector3 dist;
	private float distancia;
	private int vida;
	private bool controleGolpe, noAtaque;
	private float tempoAtaque;
	// Use this for initialization
	void Start () {
		Velocidade = 20;
		vida = 3;
		tempoAtaque = Time.time;	
		controleGolpe = false;
		noAtaque = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.Persegue ();
	
	}

	void Persegue()
	{
		dist = this.transform.position - GameObject.Find("Heroi").transform.position;
		distancia = Mathf.Sqrt(dist.x * dist.x + dist.z * dist.z);
		
		if(distancia <50 && distancia > 10 && !noAtaque)
		{
			transform.LookAt (GameObject.Find("Heroi").transform.position);
			transform.Translate (0, 0, Velocidade * Time.deltaTime);
			//GameObject.Find("Enemy").animation.Play("InimigoCorrendo");
			this.transform.FindChild("Enemy").gameObject.animation.Play("InimigoCorrendo");
		}
		
		if (this.gameObject.transform.position.y <= -100) {
			Destroy(this.gameObject);		
		}

		if(distancia >= 50)
		{
			this.transform.FindChild("Enemy").gameObject.animation.Play("InimigoParado");
		}

		///ATAQUE
		if(distancia <= 11 && !noAtaque)
		{
			this.transform.FindChild("Enemy").gameObject.animation.Play("InimigoAtacando1");
			tempoAtaque = Time.time;
			//tempoAtaque+= GameObject.Find("Tahon").animation["HeroiAtacando1"].time;
			tempoAtaque+=1.5F;
			//controleGolpe = false;
			noAtaque = true;
		}
		if (Time.time >= tempoAtaque) {
			noAtaque = false;
		}

		if (!this.transform.FindChild("Enemy").gameObject.animation.IsPlaying ("InimigoAtacando1")) {
			controleGolpe = false;	
			noAtaque = false;
		}
	}

	void OnTriggerStay (Collider other) {
		Debug.Log ("na colisao");
		Debug.Log ("controleGolpe " + controleGolpe);
		Debug.Log ("noAtaque " + noAtaque);
		
		
		if (other.tag == "Heroi" && !controleGolpe && noAtaque) {
			Debug.Log ("pontuou!!!");
			other.GetComponent<HeroiScript>().RecebeDano(1);
			controleGolpe = true;
		}
	}

	public void RecebeDano(int quantidade)
	{
		vida -= quantidade;
		if(vida <=0)
		{
			GameObject.Destroy(this.gameObject);
		}

	}
}
