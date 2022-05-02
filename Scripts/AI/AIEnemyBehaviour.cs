using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBehaviour : AIBehaviour
{

    public AIEnemyController controller;

    public List<Transform> onAims = new List<Transform>();

    [SerializeField] private ParticleSystem shoters;
    [SerializeField] private Rigidbody _rigidbody;

    private void Update()
    {
        if (onAims.Count > 0)
        {
            Attack();
        }
        else
        {
            Search();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(controller.target))
        {
            Transform targetTransform = collision.transform;
            if (!onAims.Contains(targetTransform))
            {
                onAims.Add(targetTransform);
            }
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(controller.target))
        {
            Transform targetTransform = collision.transform;
            if (onAims.Contains(targetTransform))
            {
                onAims.Remove(targetTransform);
            }
        }
    }
    private void Search()
    {
        GameObject finded = GameObject.FindGameObjectWithTag(controller.target);
        if (finded)
        {
            Move((finded.transform.position - transform.position).normalized);
        }
        else
        {

        }
    }
    private void Attack()
    {

    }
    public override void Move(Vector3 direction)
    {
        _rigidbody.AddForce(controller.attributes.speed * direction);
    }
}
