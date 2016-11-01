using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{
    public class FireBaseScript : MonoBehaviour
    {
        public AudioSource AudioSource;
        public GameObject ProjectileColliderObject;
        public float Duration = 2.0f;
        public ParticleSystem[] ManualParticleSystems;
        public bool Dragon;      

        private IEnumerator CleanupEverythingCoRoutine()
        {
            if (Duration <= 0.0f)
                yield return 0;
            else
                yield return new WaitForSeconds(2.0f);

            GameObject.Destroy(gameObject);
        }

        private void StartParticleSystems()
        {
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
            {
                if (ManualParticleSystems == null || ManualParticleSystems.Length == 0 ||
                    System.Array.IndexOf(ManualParticleSystems, p) < 0)
                {
                    if (p.startDelay == 0.0f)
                    {
                        p.startDelay = 0.01f;
                    }
                    p.Play();
                }
            }
        }
        
        protected virtual void Start()
        {
            if (AudioSource != null)
                AudioSource.Play();
            
            StartParticleSystems();
            
            ICollisionHandler handler = (this as ICollisionHandler);
            if (handler != null)
            {
                FireCollisionForwardScript collisionForwarder = GetComponentInChildren<FireCollisionForwardScript>();
                if (collisionForwarder != null)
                {
                    collisionForwarder.CollisionHandler = handler;
                }
            }
        }

        protected virtual void Update()
        {
            Duration -= Time.deltaTime;
            if (Duration <= 0.0f && !Stopping && !Dragon)
            {
                ProjectileColliderObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                Stop();
            }
            if (!GameInfo_Mgr.GetInstance.Attack_Dragon && Dragon)
            {
                Stop();
            }
        }
               
        public virtual void Stop()
        {
            if (Stopping)
                return;
            Stopping = true;
            
            foreach (ParticleSystem p in gameObject.GetComponentsInChildren<ParticleSystem>())
                p.Stop();

            StartCoroutine(CleanupEverythingCoRoutine());
        }

        public bool Stopping
        {
            get;
            private set;
        }
    }
}