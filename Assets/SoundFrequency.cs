    using UnityEngine;
    using UnityEngine.UI;
    using Unity.Collections;
    using System.Collections.Generic;
    using System.Collections;

    public class SoundFrequency : MonoBehaviour
    {
        private AudioSource sound;
        
        public Button startBttn;
        public Sprite playIcon;
        public Sprite pauseIcon;


        public Slider volumeSlider;

        public RawImage[] bars;

        private float sensitivity = 10f;
        private float smoothSpeed = 10f;

        public AudioClip[] playlist;
        public Dropdown musicDropdown;

        public Slider pitchSlider;
        private float currentPitchVelocity;

        public Slider timeSlider;

        public InputField timeIP;

        void Start()
        {
            startBttn.onClick.AddListener(StartMusic);
            


            sound = GetComponent<AudioSource>();
            volumeSlider.onValueChanged.AddListener(ChangeVolume);

            List<string> options = new List<string>();
            foreach (var clip in playlist)
            {
                options.Add(clip.name);
            }
            musicDropdown.AddOptions(options);
            musicDropdown.onValueChanged.AddListener(ChangeMusic);
            timeSlider.maxValue = sound.clip.length;
            timeSlider.onValueChanged.AddListener(ChangeTime);
            ChangeIcon();
        }

        // Update is called once per frame
        void Update()
        {   
            if(sound.isPlaying){
                float[] spectrum = new float[256];
                // on récupère les Données de spectre du son dans un tableau de float
                sound.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

                // Selon le nombre de bars de visualisation
                for (int i = 0; i < bars.Length; i++){
                    // On récupère lav taille du spectre de donné qu'on scale, 
                    float targetHeight = spectrum[i] * sensitivity;
                    // on fait une interpolation linéaire pour déplacer progressivement la hauteur de la bar
                    float newY = Mathf.Lerp(bars[i].rectTransform.localScale.y, targetHeight, Time.deltaTime * smoothSpeed);
                    bars[i].rectTransform.localScale = new Vector3(1, newY, 1);
                }

                // On définie la valeur max du slider avec celle de la musique
                timeSlider.maxValue = sound.clip.length;
                
                //pour lisser progressivement la valeur de la hauteur sonore
                sound.pitch = Mathf.SmoothDamp(sound.pitch, pitchSlider.value, ref currentPitchVelocity, 0.2f);

                // Met à jour la bar du slider selon la musique
                timeSlider.value = sound.time;
                
                string timeText = string.Format("{0:00}:{1:00}", (int)sound.time / 60, (int)sound.time % 60);
                timeIP.text = timeText;
            }
            
        }

        // Permet de mettre en Pause/Jouer la son 
        void StartMusic(){
            if(sound.isPlaying){
                sound.Pause();
            }
            else{
                sound.Play();
            }
            ChangeIcon();
        }

        // Permet de changer l'icone du bouton selon si le son est joué 
        void ChangeIcon(){
            Image imageBttn = startBttn.GetComponent<Image>();
            if(sound.isPlaying){
                imageBttn.sprite = pauseIcon;
            }
            else{
                imageBttn.sprite = playIcon;
            }
        }

        // Met à jour le volume de la music
        void ChangeVolume(float value){
            sound.volume = value;
        }

        // Permet de changer la musique joué actuellement 
        void ChangeMusic(int index){
            sound.clip = playlist[index];
            timeSlider.maxValue = sound.clip.length;
            sound.Play();
            ChangeIcon();
        }

        // Permet de remettre à jour le son à jour selon la valeur du slider
        void ChangeTime(float value){
            sound.time = timeSlider.value;
        }
    }
