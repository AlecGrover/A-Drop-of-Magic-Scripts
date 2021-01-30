using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AmbienceController : MonoBehaviour
    {

        public AudioMixerGroup mixerGroup;

        public AudioClip ambientTrack;
        [Range(0f, 1f)]
        public float volume;

        public bool useParentPosition = false;
        public Vector3 ambientSource = Vector3.zero;
        [Range(0f, float.PositiveInfinity)]
        public float ambientRange = 10f;
        [Range(0f, 1f)]
        public float falloffPercent = 0.8f;
        private AudioSource _audioSource;

        // Start is called before the first frame update
        private void Start()
        {
            // InitializeCollider();
            if (!useParentPosition) gameObject.transform.position = ambientSource;
            InitializeAudioSource();
        }

        private void InitializeCollider()
        {
            var ambientTrigger = gameObject.AddComponent<SphereCollider>();
            ambientTrigger.isTrigger = true;
            ambientTrigger.radius = ambientRange;
        }

        private void InitializeAudioSource()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = ambientTrack;
            _audioSource.volume = volume;
            _audioSource.outputAudioMixerGroup = mixerGroup;
            _audioSource.rolloffMode = AudioRolloffMode.Linear;
            _audioSource.minDistance = ambientRange * falloffPercent;
            _audioSource.maxDistance = ambientRange;
            _audioSource.loop = true;
            _audioSource.spatialBlend = 1f;
            _audioSource.spread = 180;
            _audioSource.Play();
        }

        private void OnDrawGizmosSelected()
        {
            var center = useParentPosition ? transform.position : ambientSource;
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(center, ambientRange);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(center, ambientRange * falloffPercent);
        }

        // Update is called once per frame
        private void Update()
        {
            
        }
    }
}
