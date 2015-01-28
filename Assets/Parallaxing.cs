using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour
{

		public Transform[] backgrounds; //array des elements qui sur lesquels l'effet sera appliqué
		private float[] parallaxScales; //vitesse des differents elements
		public float smoothing; // /!\ plus grand que zero
	
		private Transform cam ; // reference vers la camera;
		private Vector3 previousCamPos; //sauvegarde la position de la camera de la frame précédente


		//begore start(). permet d'initialiser les variables
		void Awake ()
		{
				this.cam = Camera.main.transform;

		}
		// Use this for initialization
		void Start ()
		{
				//frame précédente
				previousCamPos = cam.position;

				this.parallaxScales = new float[backgrounds.Length];

				for (int i=0; i<this.backgrounds.Length; i++) {
						this.parallaxScales [i] = this.backgrounds [i].position.z * -1;
				
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				for (int i =0; i<this.backgrounds.Length; i++) {

						float parallax = (previousCamPos.x - this.cam.position.x) * parallaxScales [i];

						float backgroundTargetPosX = this.backgrounds [i].position.x + parallax;

						Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, this.backgrounds [i].position.y, this.backgrounds [i].position.z);

						backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
				}
		previousCamPos = cam.position;
		}
}
