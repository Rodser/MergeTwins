using UnityEngine;

namespace MiniIT.Test.Grounds
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private ParticleSystem mergeEffect = null;

        public ParticleSystem MergeEffect => mergeEffect;

        public void PlayMergeEffect()
        {
            this.MergeEffect.Play();
        }
    }
}