using UnityEngine;

namespace MiniIT.Test.Grounds
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private ParticleSystem mergeEffect = null;
        [SerializeField] private AudioSource mergeSound = null;
       
        public void PlayMergeEffect()
        {
            this.mergeSound.Play();
            this.mergeEffect.Play();
        }
    }
}