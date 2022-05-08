using UnityEngine;

namespace Rodser.MergeTwins
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource clickSound = null;
        [SerializeField] private AudioSource mergeSound = null;
        [SerializeField] private AudioSource music = null;

        private void Awake()
        {
            Game.AudioManager = this;
        }

        public void OnValueChangedSound(float value)
        {
            mergeSound.volume = value;
            clickSound.volume = value;
            this.clickSound.Play();
        }

        public void OnValueChangedMusic(float value)
        {
            music.volume = value;
        }

        public void OnClickSound(object sender)
        {
            this.clickSound.Play();
        }

        public void OnMergeSound(object sender)
        {
            this.mergeSound.Play();
        }
    }
}
