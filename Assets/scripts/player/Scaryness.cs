using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace PlayerMechanics
{
    public class Scaryness : MonoBehaviour
    {
        //Esto controla el miedo que le tienen los enemigos al jugador
        public static Scaryness Instance { get; private set; }

        private float howScary, timer, secondaryTimer;

        private PostProcessVolume myVolume;
        private ColorGrading grading;
        public PostProcessProfile camProfile;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            //myVolume = Camera.main.GetComponent<PostProcessVolume>();

            grading = ScriptableObject.CreateInstance<ColorGrading>();
            grading.enabled.Override(true);

            Color myFilter = new Color(1000, 0, 0);
            grading.colorFilter.value = myFilter;

            myVolume = PostProcessManager.instance.QuickVolume(0, 10, grading);
            //myVolume.isGlobal = true;
            //myVolume.profile = camProfile;
        }

        private void Update()
        {
            Debug.Log(grading.colorFilter.value);

            IncreaseScaryness(0.1f);

            //Mantines la furia mientras que estes en combate basicamente
            if (secondaryTimer > 0)
            {
                secondaryTimer -= Time.deltaTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

        public void IncreaseScaryness(float howMuch)
        {
            howScary += howMuch;
            if (howScary > 5) howScary = 5;

            secondaryTimer = 3;

            timer += howMuch * 2;

            if (timer > 15) timer = 15;

            //Color myFilter = new Color(255, 255 - (howScary * 51), 255 - (howScary * 51));

            Color myFilter = new Color(1000, 0,0);

            /*
            if (myVolume.TryGetComponent(out ColorGrading grading))
            {
                grading.colorFilter.value = myFilter;
            }
            */

            grading.colorFilter.value = myFilter;
        }
    }
}
