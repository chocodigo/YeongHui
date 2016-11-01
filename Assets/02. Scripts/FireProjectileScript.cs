using UnityEngine;
using System.Collections;

namespace DigitalRuby.PyroParticles
{    
    public delegate void FireProjectileCollisionDelegate(FireProjectileScript script, Vector3 pos);
    
    public class FireProjectileScript : FireBaseScript, ICollisionHandler
    {                      
        public AudioSource ProjectileCollisionSound;        
        public ParticleSystem ProjectileExplosionParticleSystem;   
        public float ProjectileColliderSpeed;        
        public Vector3 ProjectileDirection = Vector3.forward;             
        public ParticleSystem[] ProjectileDestroyParticleSystemsOnCollision;

        [HideInInspector]
        public FireProjectileCollisionDelegate CollisionDelegate;

        private bool collided;

        private void SendCollision()
        {
            Vector3 dir = ProjectileDirection * ProjectileColliderSpeed;
            dir = ProjectileColliderObject.transform.rotation * dir;
            ProjectileColliderObject.GetComponent<Rigidbody>().velocity = dir;
        }

        protected override void Start()
        {
            base.Start();

            SendCollision();
        }

        public void HandleCollision(GameObject obj, Collision c)
        {
            if (collided)
                return;

            collided = true;
            Stop();

            if (ProjectileDestroyParticleSystemsOnCollision != null)
                foreach (ParticleSystem p in ProjectileDestroyParticleSystemsOnCollision)
                    GameObject.Destroy(p, 0.1f);

            if (ProjectileCollisionSound != null)
                ProjectileCollisionSound.Play();
            
            if (c.contacts.Length != 0)
            {
                ProjectileExplosionParticleSystem.transform.position = c.contacts[0].point;
                ProjectileExplosionParticleSystem.Play();
                if (CollisionDelegate != null)
                    CollisionDelegate(this, c.contacts[0].point);
            }
        }
    }
}