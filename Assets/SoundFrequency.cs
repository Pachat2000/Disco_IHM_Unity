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
                sound.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);
                for (int i = 0; i < bars.Length; i++){
                    float targetHeight = spectrum[i] * sensitivity;
                    float newY = Mathf.Lerp(bars[i].rectTransform.localScale.y, targetHeight, Time.deltaTime * smoothSpeed);
                    bars[i].rectTransform.localScale = new Vector3(1, newY, 1);
                }
                timeSlider.maxValue = sound.clip.length;
                sound.pitch = Mathf.SmoothDamp(sound.pitch, pitchSlider.value, ref currentPitchVelocity, 0.2f);

                timeSlider.value = sound.time;
                string timeText = string.Format("{0:00}:{1:00}", (int)sound.time / 60, (int)sound.time % 60);
                timeIP.text = timeText;
            }
            
        }

        void StartMusic(){
            if(sound.isPlaying){
                sound.Pause();
            }
            else{
                sound.Play();
            }
            ChangeIcon();
        }

        void ChangeIcon(){
            Image imageBttn = startBttn.GetComponent<Image>();
            if(sound.isPlaying){
                imageBttn.sprite = pauseIcon;
            }
            else{
                imageBttn.sprite = playIcon;
            }
        }

        void ChangeVolume(float value){
            sound.volume = value;
        }

        void ChangeMusic(int index){
            sound.clip = playlist[index];
            timeSlider.maxValue = sound.clip.length;
            sound.Play();
            ChangeIcon();
        }

        void ChangeTime(float value){
            sound.time = timeSlider.value;
        }
    }
