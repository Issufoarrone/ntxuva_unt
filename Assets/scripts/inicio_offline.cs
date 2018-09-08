
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class inicio_offline : MonoBehaviour
{
	public static int []Cova_1  = new int[25];
	public static int []Cova_2  = new int[25];
	public static int Jogador_Master = 1 ;
	public static bool Tirar_Pedras_Do_Oponente = false;
	public static string Suspensor_De_Jogo = "off";
	public static string Nome = "";
	public static int Pedras_Retiradas = 0;
	//public  AudioSource source;
	//http://ntxuva.us.openode.io/

	void Start()
	{
		Jogador_Master = 1;
		Actulizar_Painel ();
		Iniciar_Jogo ();

	}


	void Actulizar_Painel ()
	{
		PlayerPrefs.SetInt ("Jogadas_Offline", PlayerPrefs.GetInt ("Jogadas_Offline")-1);

		Alerta ("");

		GameObject.Find ("Jogador_1_Nome").GetComponent<Text> ().text = PlayerPrefs.GetString("Nome");
		GameObject.Find ("Jogador_2_Nome").GetComponent<Text> ().text = "007";

		if (PlayerPrefs.GetString ("som") == "on") {
			GameObject.Find ("btm_som").GetComponent<Image> ().sprite = Resources.Load ("com_som", typeof(Sprite)) as Sprite;

		} else {
			GameObject.Find ("btm_som").GetComponent<Image> ().sprite = Resources.Load ("sem_som", typeof(Sprite)) as Sprite;

		}

	}





	public static void Iniciar_Jogo()
	{

		//Arumar o Tabuleiro
		for (int i = 1; i <= 24; i++) 
		{

			GameObject.Find ("cova_1_" + i).GetComponent<Image> ().sprite = Resources.Load("1_2", typeof(Sprite)) as Sprite;
			GameObject.Find ("cova_2_" + i).GetComponent<Image> ().sprite = Resources.Load("2_2", typeof(Sprite)) as Sprite;
			Cova_1 [i] = 2;
			Cova_2 [i] = 2;
		}

		if (Jogador_Master == 1) 
		{
			Alerta ("Inicio do Jogo, você começa o jogo");
		}

		if (Jogador_Master == 2) 
		{
			Alerta ("Inicio do Jogo, Aguarde, a jogada do seu oponente");
		}

		Suspensor_De_Jogo = "off";

	}



	public  IEnumerator Destribuir_Pedras(GameObject Objecto)
	{
		
		int cova = Object_To_Cova_Num(Objecto);
		int tabuleiro = Object_To_Tabuleiro_Num (Objecto);
		int dados = Cova_1 [cova];


		if (Jogador_Master == 1) {

			//Verificar se deve-se retirar as covas do oponente
			if (!Tirar_Pedras_Do_Oponente) {
				//nao tirar pedras do oponente
				//Verificar se a cova possui dados suficientes


				//Verificar se clicou no tabuleiro 1
				if (tabuleiro == 1) {
				
					//Tabuleiro 1
					if (Cova_1 [cova] != 0) {

						if (Cova_1 [cova] != 1 || (Cova_1 [cova] == 1 && Pode_Jogar_Com_Uma_Pedra ())) {

							if (dados > 0) {


								//Esvaziar a cova que foi clicada
								Cova_1 [cova] = 0;
								GameObject.Find ("cova_" + tabuleiro + "_" + cova).GetComponent<Image> ().sprite = Resources.Load ("1_0", typeof(Sprite)) as Sprite;

								int Proxima_Cova = cova;

								while (true) {


									Proxima_Cova++;

									if (Proxima_Cova > 24) {
										Proxima_Cova = 1;	
									}

									Cova_1 [Proxima_Cova] = Cova_1 [Proxima_Cova] + 1;
									GameObject.Find ("cova_" + tabuleiro + "_" + Proxima_Cova).GetComponent<Image> ().sprite = Resources.Load ("1_" + Cova_1 [Proxima_Cova], typeof(Sprite)) as Sprite;

									dados--;

									if (dados == 0) {
										
										if (Cova_1 [Proxima_Cova] != 1) {

											//Esvaziar a cova que foi clicada

											dados = Cova_1 [Proxima_Cova];

											Cova_1 [Proxima_Cova] = 0;
											GameObject.Find ("cova_" + tabuleiro + "_" + Proxima_Cova).GetComponent<Image> ().sprite = Resources.Load ("1_0", typeof(Sprite)) as Sprite;
										
										} else {

											//verificar se bateu
											int[] Cova_Oposta = Tabuleiro_Covas_Oposta (Proxima_Cova);

											if (Proxima_Cova >= 1 && Proxima_Cova <= 12 && Cova_2 [Cova_Oposta [0]] != 0) {
												//Bateu
												Cova_2 [Cova_Oposta [0]] = 0;
												Cova_2 [Cova_Oposta [1]] = 0;
												GameObject.Find ("cova_2_" + Cova_Oposta [0]).GetComponent<Image> ().sprite = Resources.Load ("2_0", typeof(Sprite)) as Sprite;
												GameObject.Find ("cova_2_" + Cova_Oposta [1]).GetComponent<Image> ().sprite = Resources.Load ("2_0", typeof(Sprite)) as Sprite;

												//Tirar 2 covas do oponente
												Tirar_Pedras_Do_Oponente = true;
												//Alerta

												if ((2 - Pedras_Retiradas) == 1) {
													Alerta ("Retire " + (2 - Pedras_Retiradas) + " Pedra do oponente...");
												} else {
													Alerta ("Retire " + (2 - Pedras_Retiradas) + " Pedras do oponente...");
												}
											} else {

												//enviar notificacao ao outro player
												Jogador_Master = 2;
												Alerta ("Aguarde, a jogada do seu oponente");
												//
												//Listar covas disponiveis em um array
													//verificar se essa cova pode ser jogada
												//Escolher um cova
												//Escolher Cova a Jogar[Logica]
												//int rndNum  = Random.Range(1, 24);
												yield return new WaitForSeconds (1f);
												yield return DPO (Robo_Escolher_Cova_Para_Jogar());

											}

											Verificar_Vencedor ();
											break;
										}

									} else {



										/*
										GameObject Obj = Instantiate(Resources.Load("Wave_01")) as GameObject;;
										Obj.transform.SetParent(GameObject.Find ("Canvas").transform);
										Obj.transform.localPosition = GameObject.Find ("cova_" + tabuleiro + "_" + Proxima_Cova).transform.localPosition;
										Obj.transform.localScale = Vector3.one;
*/
										if (PlayerPrefs.GetString ("som") == "on") 
										{
											GameObject.Find ("Audio_Mover_Pedra").GetComponent<AudioSource> ().Play ();

										}
										//yield return new WaitForSeconds (0.9f);
										//Destroy(GameObject.Find ("Wave_01(Clone)"),0.5f);
									}

									yield return new WaitForSeconds (0.4f);


								}


							}

						} else {
							//Cova sem dados suficientes, ainda possui covas com mais de um dado
							Alerta ("Cova sem dados suficientes\njogue covas com mais de 1 dado");
						}


					} else {
						//Cova vazia, escolha uma cova com dados
						Alerta ("Cova vazia\njogue covas com dados");
					}


				} else {
					//jogue dados do seu tabuleiro
					Alerta ("jogue os seus dados");
				}

			} else {

				//verificar se escolheu a cova do oponente
				if (tabuleiro == 2) {

					// verificar se a cova possui dados
					if (Cova_2 [cova] != 0) {
					
						//Tirar pedras do oponente
						if (Pedras_Retiradas < 2) {


							Cova_2 [cova] = 0;
							GameObject.Find ("cova_" + tabuleiro + "_" + cova).GetComponent<Image> ().sprite = Resources.Load ("2_0", typeof(Sprite)) as Sprite;

							Pedras_Retiradas++;



						} else {
							//Ja Tirou dados suficientes do oponente
						}

						if (Pedras_Retiradas == 2) {
							//Completou o limite de 2 covas
							Tirar_Pedras_Do_Oponente = false;
							Pedras_Retiradas = 0;

							Alerta ("continue suas jogadas");

						}

						Verificar_Vencedor ();

					} else {
						//Cova de oponente sem dados, escolha uma cova com dados
						Alerta ("Cova sem dados\nescolha covas com um dado no minimo");
					}


				} else {
					//Alert Escolha covas do oponente
					Alerta ("Retire " + (2 - Pedras_Retiradas) + " Pedras do oponente...");
				}



			}

		} else {
			Alerta ("Nao e' sua vez de Jogas,\n Aguarde a jogada do seu oponente");
		}

	}





	public static IEnumerator DPO(int cova)
	{
		
		int tabuleiro = 2;
		int dados = Cova_2 [cova];

		Debug.Log ("Cova: "+cova);

		//Esvaziar a cova que foi clicada
		Cova_2 [cova] = 0;
		GameObject.Find ("cova_" + tabuleiro + "_" + cova).GetComponent<Image> ().sprite = Resources.Load ("2_0", typeof(Sprite)) as Sprite;

		int Proxima_Cova = cova;

		while (true) {

			Proxima_Cova++;

			if (Proxima_Cova > 24) {
				Proxima_Cova = 1;	
			}

			Cova_2 [Proxima_Cova] = Cova_2 [Proxima_Cova] + 1;
			GameObject.Find ("cova_" + tabuleiro + "_" + Proxima_Cova).GetComponent<Image> ().sprite = Resources.Load ("2_" + Cova_2 [Proxima_Cova], typeof(Sprite)) as Sprite;

			dados--;

			if (dados == 0) {

				if (Cova_2 [Proxima_Cova] != 1) {

					//Esvaziar a cova que foi clicada

					dados = Cova_2 [Proxima_Cova];

					Cova_2 [Proxima_Cova] = 0;
					GameObject.Find ("cova_" + tabuleiro + "_" + Proxima_Cova).GetComponent<Image> ().sprite = Resources.Load ("2_0", typeof(Sprite)) as Sprite;

					// Listar as covas que podem ser retiradas no oponente
					// Escolher uma das covas

				} else {

					//verificar se bateu
					int[] Cova_Oposta = Tabuleiro_Covas_Oposta (Proxima_Cova);

					if (Proxima_Cova >= 1 && Proxima_Cova <= 12 && Cova_1 [Cova_Oposta [0]] != 0) {
						//Bateu
						Cova_1 [Cova_Oposta [0]] = 0;
						Cova_1 [Cova_Oposta [1]] = 0;
						GameObject.Find ("cova_1_" + Cova_Oposta [0]).GetComponent<Image> ().sprite = Resources.Load ("1_0", typeof(Sprite)) as Sprite;
						GameObject.Find ("cova_1_" + Cova_Oposta [1]).GetComponent<Image> ().sprite = Resources.Load ("1_0", typeof(Sprite)) as Sprite;

						//Tirar 2 covas do oponente [Logica]
						yield return new WaitForSeconds (1f);
						Robo_Escolher_Retirar_Oponente_Cova ();
						yield return new WaitForSeconds (1f);
						Robo_Escolher_Retirar_Oponente_Cova ();
						yield return new WaitForSeconds (1f);
						yield return DPO (Robo_Escolher_Cova_Para_Jogar ());


					} else {

						//enviar notificacao ao outro player
						Jogador_Master = 1;
						Alerta ("Agora e' sua vez de jogar");

					}

					Verificar_Vencedor ();
					break;


				}

			} 
			else {
				if (PlayerPrefs.GetString ("som") == "on") {
					GameObject.Find ("Audio_Mover_Pedra").GetComponent<AudioSource> ().Play ();
				}
			}



			yield return new WaitForSeconds (0.4f);

		}


	}

	public static void Robo_Escolher_Retirar_Oponente_Cova()
	{

		List<int> covas_disponiveis = new List<int>();
		List<int> covas_disponiveis_que_podem_me_bater = new List<int>();

		bool Cova_Com_Um_Dado = true; 

		for (int i = 1; i <= 24; i++) 
		{
			if(Cova_1 [i]>=2)
			{ 
				Cova_Com_Um_Dado = false; 
			}
		}

				
		//verificar covas com dados
		for (int i = 1; i <= 24; i++) 
		{
			if((Cova_1 [i]>=1 && Cova_Com_Um_Dado) || Cova_1 [i]>=2)
			{ 
				covas_disponiveis.Add (i);
			}
		}



		if (covas_disponiveis.Count > 0) {

			//Verificar as covas que podem me bater
			//Verificar as covas em que pode bater
			for (int i = 0; i < covas_disponiveis.Count; i++) {


				//int []Cova_1_Temp  = new int[25];
				int []Cova_1_Temp  = new int[25];

				for (int i_i = 1; i_i <= 24; i_i++) 
				{
					Cova_1_Temp[i_i] = Cova_1[i_i];
				}

				int dados_temp = Cova_1_Temp [covas_disponiveis[i]]; 
				int Proxima_Cova_temp = covas_disponiveis[i];

				while (true) {

					Proxima_Cova_temp++;

					if (Proxima_Cova_temp > 24) {
						Proxima_Cova_temp = 1;	
					}

					Cova_1_Temp [Proxima_Cova_temp] = Cova_1_Temp [Proxima_Cova_temp] + 1;

					dados_temp--;

					if (dados_temp == 0) {

						if (Cova_1_Temp [Proxima_Cova_temp] != 1) {

							//Esvaziar a cova que foi clicada

							dados_temp = Cova_1_Temp [Proxima_Cova_temp];

							Cova_1_Temp [Proxima_Cova_temp] = 0;


						} else {

							//verificar se bateu
							int[] Cova_Oposta = Tabuleiro_Covas_Oposta (Proxima_Cova_temp);

							if (Proxima_Cova_temp >= 1 && Proxima_Cova_temp <= 12 && Cova_2 [Cova_Oposta [0]] != 0) {
								//Pode Bater
								covas_disponiveis_que_podem_me_bater.Add (covas_disponiveis[i]);
							} 

							break;

						}

					}

				}

			}


			int Cova_Escolhida;

			if (covas_disponiveis_que_podem_me_bater.Count>0) {
				Cova_Escolhida = covas_disponiveis_que_podem_me_bater [Random.Range (0, covas_disponiveis_que_podem_me_bater.Count)];

			} else {
				Cova_Escolhida = covas_disponiveis[Random.Range(0, covas_disponiveis.Count)];
			}

			Cova_1 [Cova_Escolhida] = 0;
			GameObject.Find ("cova_1_" + Cova_Escolhida).GetComponent<Image> ().sprite = Resources.Load ("1_0", typeof(Sprite)) as Sprite;

		}

		Verificar_Vencedor ();
	}

	public static int Robo_Escolher_Cova_Para_Jogar()
	{

		List<int> covas_disponiveis = new List<int>();
		List<int> covas_disponiveis_que_pode_bater = new List<int>();

		//Verificar se pode escolher covas com um dado

		bool Cova_Com_Um_Dado = true; 

		for (int i = 1; i <= 24; i++) 
		{
			if(Cova_2 [i]>=2)
			{ 
				Cova_Com_Um_Dado = false; 
			}
		}

		//verificar covas com dados
		for (int i = 1; i <= 24; i++) 
		{
			if((Cova_2 [i]>=1 && Cova_Com_Um_Dado) || Cova_2 [i]>=2)
			{ 
				covas_disponiveis.Add (i);
			}
		}


		//Verificar as covas em que pode bater
		for (int i = 0; i < covas_disponiveis.Count; i++) {


			//int []Cova_1_Temp  = new int[25];
			int []Cova_2_Temp  = new int[25];

			for (int i_i = 1; i_i <= 24; i_i++) 
			{
				Cova_2_Temp[i_i] = Cova_2[i_i];
			}

			int dados_temp = Cova_2_Temp [covas_disponiveis[i]]; 
			int Proxima_Cova_temp = covas_disponiveis[i];

			while (true) {

				Proxima_Cova_temp++;

				if (Proxima_Cova_temp > 24) {
					Proxima_Cova_temp = 1;	
				}

				Cova_2_Temp [Proxima_Cova_temp] = Cova_2_Temp [Proxima_Cova_temp] + 1;

				dados_temp--;

				if (dados_temp == 0) {

					if (Cova_2_Temp [Proxima_Cova_temp] != 1) {

						//Esvaziar a cova que foi clicada

						dados_temp = Cova_2_Temp [Proxima_Cova_temp];

						Cova_2_Temp [Proxima_Cova_temp] = 0;


					} else {

						//verificar se bateu
						int[] Cova_Oposta = Tabuleiro_Covas_Oposta (Proxima_Cova_temp);

						if (Proxima_Cova_temp >= 1 && Proxima_Cova_temp <= 12 && Cova_1 [Cova_Oposta [0]] != 0) {
							//Pode Bater
							covas_disponiveis_que_pode_bater.Add (covas_disponiveis[i]);
						} 

						Verificar_Vencedor ();
						break;

					}

				}

			}

		}

			//Verificar as covas em que pode bater mais uma vez
		//Caso nao tenha onde Bater, verificar as covas que pode ser batido

		//int Cova_Escolhida = covas_disponiveis[Random.Range(0, covas_disponiveis.Count)];

		if (covas_disponiveis_que_pode_bater.Count>0) {
			int Cova_Escolhida = covas_disponiveis_que_pode_bater [Random.Range (0, covas_disponiveis_que_pode_bater.Count)];
			return Cova_Escolhida;
		} else {
			int Cova_Escolhida = covas_disponiveis[Random.Range(0, covas_disponiveis.Count)];
			return Cova_Escolhida;
		}


	}







	public IEnumerator  Efeito_Escrevendo()
	{


		GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "Escrevendo...";
		yield return new WaitForSeconds (1.0f);
		GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "";
		/*
		while (true) {



			if (GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text == "...") 
			{
				GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "";
				break;
			}

			if (GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text == "..") 
			{
				GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "...";
				break;
			}

			if (GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text == ".") 
			{
				GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "..";
			}

			if (GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text == "") 
			{
				GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = ".";
			}

			yield return new WaitForSeconds (0.4f);

		}
*/

		//GameObject.Find ("efeito_escrevendo_text").GetComponent<Text> ().text = "";

	}

	public static bool Pode_Jogar_Com_Uma_Pedra()
	{
		//Arumar o Tabuleiro
		for (int i = 1; i <= 24; i++) 
		{
			if(Cova_1 [i]>=2){ return false; }
		}

		return true;
	}


	public static int Object_To_Cova_Num(GameObject Objecto)
	{

		if(Objecto.name=="cova_1_1"){ return 1; }
		if(Objecto.name=="cova_1_2"){ return 2; }
		if(Objecto.name=="cova_1_3"){ return 3; }
		if(Objecto.name=="cova_1_4"){ return 4; }
		if(Objecto.name=="cova_1_5"){ return 5; }
		if(Objecto.name=="cova_1_6"){ return 6; }
		if(Objecto.name=="cova_1_7"){ return 7; }
		if(Objecto.name=="cova_1_8"){ return 8; }
		if(Objecto.name=="cova_1_9"){ return 9; }
		if(Objecto.name=="cova_1_10"){ return 10; }
		if(Objecto.name=="cova_1_11"){ return 11; }
		if(Objecto.name=="cova_1_12"){ return 12; }
		if(Objecto.name=="cova_1_13"){ return 13; }
		if(Objecto.name=="cova_1_14"){ return 14; }
		if(Objecto.name=="cova_1_15"){ return 15; }
		if(Objecto.name=="cova_1_16"){ return 16; }
		if(Objecto.name=="cova_1_17"){ return 17; }
		if(Objecto.name=="cova_1_18"){ return 18; }
		if(Objecto.name=="cova_1_19"){ return 19; }
		if(Objecto.name=="cova_1_20"){ return 20; }
		if(Objecto.name=="cova_1_21"){ return 21; }
		if(Objecto.name=="cova_1_22"){ return 22; }
		if(Objecto.name=="cova_1_23"){ return 23; }
		if(Objecto.name=="cova_1_24"){ return 24; }

		if(Objecto.name=="cova_2_1"){ return 1; }
		if(Objecto.name=="cova_2_2"){ return 2; }
		if(Objecto.name=="cova_2_3"){ return 3; }
		if(Objecto.name=="cova_2_4"){ return 4; }
		if(Objecto.name=="cova_2_5"){ return 5; }
		if(Objecto.name=="cova_2_6"){ return 6; }
		if(Objecto.name=="cova_2_7"){ return 7; }
		if(Objecto.name=="cova_2_8"){ return 8; }
		if(Objecto.name=="cova_2_9"){ return 9; }
		if(Objecto.name=="cova_2_10"){ return 10; }
		if(Objecto.name=="cova_2_11"){ return 11; }
		if(Objecto.name=="cova_2_12"){ return 12; }
		if(Objecto.name=="cova_2_13"){ return 13; }
		if(Objecto.name=="cova_2_14"){ return 14; }
		if(Objecto.name=="cova_2_15"){ return 15; }
		if(Objecto.name=="cova_2_16"){ return 16; }
		if(Objecto.name=="cova_2_17"){ return 17; }
		if(Objecto.name=="cova_2_18"){ return 18; }
		if(Objecto.name=="cova_2_19"){ return 19; }
		if(Objecto.name=="cova_2_20"){ return 20; }
		if(Objecto.name=="cova_2_21"){ return 21; }
		if(Objecto.name=="cova_2_22"){ return 22; }
		if(Objecto.name=="cova_2_23"){ return 23; }
		if(Objecto.name=="cova_2_24"){ return 24; }

		return 0;


	}


	public static void Verificar_Vencedor()
	{
		int Jogador_1 = 0;
		int Jogador_2 = 0;


		//Arumar o Tabuleiro
		for (int i = 1; i <= 24; i++) 
		{

			Jogador_1 = Jogador_1 + Cova_1 [i];
			Jogador_2 = Jogador_2 + Cova_2 [i];

		}


		if(Jogador_1==0){ Suspensor_De_Jogo="on"; Alerta("Opss,"+Nome+" você perdeu. Novo Jogo"); }
		if(Jogador_2==0){ Suspensor_De_Jogo="on"; Alerta("Parabéns "+Nome+" você ganhou. Novo Jogo"); }


	}


	public static int Object_To_Tabuleiro_Num(GameObject Objecto)
	{

		if(Objecto.name=="cova_1_1"){ return 1; }
		if(Objecto.name=="cova_1_2"){ return 1; }
		if(Objecto.name=="cova_1_3"){ return 1; }
		if(Objecto.name=="cova_1_4"){ return 1; }
		if(Objecto.name=="cova_1_5"){ return 1; }
		if(Objecto.name=="cova_1_6"){ return 1; }
		if(Objecto.name=="cova_1_7"){ return 1; }
		if(Objecto.name=="cova_1_8"){ return 1; }
		if(Objecto.name=="cova_1_9"){ return 1; }
		if(Objecto.name=="cova_1_10"){ return 1; }
		if(Objecto.name=="cova_1_11"){ return 1; }
		if(Objecto.name=="cova_1_12"){ return 1; }
		if(Objecto.name=="cova_1_13"){ return 1; }
		if(Objecto.name=="cova_1_14"){ return 1; }
		if(Objecto.name=="cova_1_15"){ return 1; }
		if(Objecto.name=="cova_1_16"){ return 1; }
		if(Objecto.name=="cova_1_17"){ return 1; }
		if(Objecto.name=="cova_1_18"){ return 1; }
		if(Objecto.name=="cova_1_19"){ return 1; }
		if(Objecto.name=="cova_1_20"){ return 1; }
		if(Objecto.name=="cova_1_21"){ return 1; }
		if(Objecto.name=="cova_1_22"){ return 1; }
		if(Objecto.name=="cova_1_23"){ return 1; }
		if(Objecto.name=="cova_1_24"){ return 1; }

		if(Objecto.name=="cova_2_1"){ return 2; }
		if(Objecto.name=="cova_2_2"){ return 2; }
		if(Objecto.name=="cova_2_3"){ return 2; }
		if(Objecto.name=="cova_2_4"){ return 2; }
		if(Objecto.name=="cova_2_5"){ return 2; }
		if(Objecto.name=="cova_2_6"){ return 2; }
		if(Objecto.name=="cova_2_7"){ return 2; }
		if(Objecto.name=="cova_2_8"){ return 2; }
		if(Objecto.name=="cova_2_9"){ return 2; }
		if(Objecto.name=="cova_2_10"){ return 2; }
		if(Objecto.name=="cova_2_11"){ return 2; }
		if(Objecto.name=="cova_2_12"){ return 2; }
		if(Objecto.name=="cova_2_13"){ return 2; }
		if(Objecto.name=="cova_2_14"){ return 2; }
		if(Objecto.name=="cova_2_15"){ return 2; }
		if(Objecto.name=="cova_2_16"){ return 2; }
		if(Objecto.name=="cova_2_17"){ return 2; }
		if(Objecto.name=="cova_2_18"){ return 2; }
		if(Objecto.name=="cova_2_19"){ return 2; }
		if(Objecto.name=="cova_2_20"){ return 2; }
		if(Objecto.name=="cova_2_21"){ return 2; }
		if(Objecto.name=="cova_2_22"){ return 2; }
		if(Objecto.name=="cova_2_23"){ return 2; }
		if(Objecto.name=="cova_2_24"){ return 2; }

		return 0;


	}

	public static int[] Tabuleiro_Covas_Oposta(int Cova)
	{

		if(Cova==1) { return new int[2] { 12, 13 }; };
		if(Cova==2) { return new int[2] { 11, 14 }; };
		if(Cova==3) { return new int[2] { 10, 15 }; };
		if(Cova==4) { return new int[2] { 9, 16 }; };
		if(Cova==5) { return new int[2] { 8, 17 }; };
		if(Cova==6) { return new int[2] { 7, 18 }; };
		if(Cova==7) { return new int[2] { 6, 19 }; };
		if(Cova==8) { return new int[2] { 5, 20 }; };
		if(Cova==9) { return new int[2] { 4, 21 };  };
		if(Cova==10) { return new int[2] { 3, 22 }; };
		if(Cova==11) { return new int[2] { 2, 23 }; };
		if(Cova==12) { return new int[2] { 1, 24 }; };

		return new int[2] { 0, 0 };
	}

	void Update()
	{
		//Press space to change the Sprite of the Image
		if (Input.GetKey(KeyCode.Space))
		{
			//GameObject.Find ("cova_1_12").GetComponent<Image>().color = Color.Lerp(Color.white,new Color(1,1,1,0), (1/1.0f) * Time.deltaTime);
		}


	}


	public static void Alerta(string Texto)
	{
		GameObject.Find ("Alertas").GetComponent<Text> ().text = Texto;
	}








}