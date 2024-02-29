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

            IncreaseScaryness(0);
        }

        private void Update()
        {
            //Mantines la furia mientras que estes en combate basicamente
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (howScary > 0) 
            {
                secondaryTimer -= Time.deltaTime;
                if(secondaryTimer <= 0)
                {
                    IncreaseScaryness(-0.1f);
                    secondaryTimer = 0.5f;
                }
            }
        }

        public void IncreaseScaryness(float howMuch)
        {
            //Big hits are more scary
            if (howMuch < 0.8f) howMuch = howMuch/2;
            howScary += howMuch;

            if (howScary < 0) howScary = 0;
            if (howScary > 5) howScary = 5;

            secondaryTimer = 3;

            timer += howMuch * 3;

            if (timer < 0) timer = 0;
            if (timer > 15) timer = 15;

            Color myFilter = new Color(1 + howScary, 1 - (howScary/5), 1 - (howScary/5));

            grading.colorFilter.value = myFilter;
        }
    }
}
