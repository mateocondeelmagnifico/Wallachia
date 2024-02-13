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

            myVolume = Camera.main.GetComponent<PostProcessVolume>();
            grading = myVolume.sharedProfile.GetSetting<ColorGrading>();

        }

        private void Update()
        {

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

            Color myFilter = new Color(1, 1 - (howScary/5), 1 - (howScary/5));

            grading.colorFilter.value = myFilter;
        }
    }
}
