using UnityEngine;
using System.Collections;

public class ChefeScript : MonoBehaviour {
	public int Velocidade;
	private Vector3 dist;
	private float distancia;
	public int vida;
	private bool controleGolpe, noAtaque;
	private float tempoAtaque;
	// Use this for initialization
	void Start () {
		Velocidade = 20;
		vida = 5;
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
		
		if(distancia <70 && distancia > 20 && !noAtaque)
		{
			Vector3 posicaoTemporaria = this.transform.position;
			transform.LookAt (GameObject.Find("Heroi").transform.position);
			//this.transform.position.x = posicaoTemporaria.x;
			//this.transform.position.y = posicaoTemporaria.y;
			transform.Translate (0, 0, Velocidade * Time.deltaTime);
			//GameObject.Find("Enemy").animation.Play("InimigoCorrendo");
			this.transform.FindChild("Boss").gameObject.animation.Play("ChefeAndando");
		}
		
		if (this.gameObject.transform.position.y <= -100) {
			Destroy(this.gameObject);		
		}
		
		if(distancia >= 70)
		{
			this.transform.FindChild("Boss").gameObject.animation.Play("ChefeParado");
		}
		
		///ATAQUE
		if(distancia <= 20 && !noAtaque)
		{
			GameObject.Find("Boss").animation.Play("ChefeAtacando1");
			
			tempoAtaque = Time.time;
			//tempoAtaque+= GameObject.Find("Tahon").animation["HeroiAtacando1"].time;
			tempoAtaque+=1.5F;
			//controleGolpe = false;
			noAtaque = true;
		}
		if (Time.time >= tempoAtaque) {
			noAtaque = false;
		}
		
		if (!this.transform.FindChild("Boss").gameObject.animation.IsPlaying ("ChefeAtacando1")) {
			controleGolpe = false;	
			noAtaque = false;
		}
	}
	
	void OnTriggerStay (Collider other) {
		//Debug.Log ("na colisao");
		//Debug.Log ("controleGolpe " + controleGolpe);
		//Debug.Log ("noAtaque " + noAtaque);
		
		
		if (other.tag == "Heroi" && !controleGolpe && noAtaque) {
			//Debug.Log ("pontuou!!!");
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
			Application.LoadLevel("Paragens");
			
		}
		
	}
}
