using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleDamageBehaviour : MonoBehaviour, IShoot
    {
        [SerializeField] private GameObject sparks;
        [SerializeField] private ParticleSystem bullets;

        private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

        private IMakeDamage damage;

        public IMakeDamage Damage { set => damage = value; }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log($"[Shoot] Particle collision detected {other.name} making damage");
            if (damage != null)
            {
                ITakeDamage take = other.GetComponentInChildren<ITakeDamage>();
                if (take != null)
                {
                    int events = bullets.GetCollisionEvents(other, collisionEvents);
                    for (int i = 0; i < events; i++)
                    {
                        Debug.Log($"[Shoot] Particle collision detected {other.name} making damage");
                        //Instantiate(sparks, collisionEvents[i].intersection, Quaternion.LookRotation(collisionEvents[i].normal));
                        take.Take(damage.MakeDamage(default));
                    }
                }
                else
                {
                    Debug.Log($"[Shoot] no Taker datected");
                }
            }
            else
            {

                Debug.Log($"[Shoot] No damage detected");
            }
        }
        public void Shoot(IMakeDamage damage)
        {
            Debug.Log("[Shoot] shooting");
            bullets.Emit((int)damage.MakeDamage(default).attack);
        }
    }
}
