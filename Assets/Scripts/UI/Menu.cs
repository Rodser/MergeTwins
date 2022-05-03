#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Rodser.MergeTwins.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button resetButton = null;
        [SerializeField] private Button exitButton = null;

        [SerializeField] private AudioSource sound = null;
        [SerializeField] private AudioSource music = null;
        [SerializeField] private Slider soundSlider = null;
        [SerializeField] private Slider musicSlider = null;


        private Button buttonMenu = null;

        private void Start()
        {
            this.playButton.onClick.AddListener(LoadScene);
            this.resetButton.onClick.AddListener(Reset);
            this.exitButton.onClick.AddListener(Quit);

            this.soundSlider.onValueChanged.AddListener(OnValueChangedSound);
            this.musicSlider.onValueChanged.AddListener(OnValueChangedMusic);
            this.soundSlider.value = 0.7f;
            this.musicSlider.value = 0.7f;
        }

        public void OnValueChangedSound(float value)
        {
            sound.volume = soundSlider.value;
        }

        public void OnValueChangedMusic(float value)
        {
            music.volume = musicSlider.value;
        }

        public void SetButtonMenu(Button button)
        {
            this.buttonMenu = button;
        }

        private void LoadScene()
        {
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            Game.StartGame();
            SceneManager.LoadSceneAsync(Game.Level, LoadSceneMode.Additive);
        }

        private void Reset()
        {
            Time.timeScale = 1;
            this.buttonMenu.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            Game.OnClickButton(this);
        }

        private void Quit()
        {
            Debug.Log("Quit application!");
            Game.OnClickButton(this);
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}