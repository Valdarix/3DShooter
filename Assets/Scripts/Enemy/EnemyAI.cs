using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public enum EnemyState
    {
        Idle,
        Chase,
        Attack
    }
    private CharacterController _enemyController;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float gravityValue = -9.81f;
    private Vector3 _velocity;
    [SerializeField] private float attackDelay = 1.5f;
    [SerializeField] private float nextAttack = 1;

    [SerializeField] private EnemyState currentState = EnemyState.Chase;

    private Transform _player;
    // Start is called before the first frame update
    void Start()
    {
        _enemyController = gameObject.GetComponent<CharacterController>();
        if (_enemyController == null)
        {
            Debug.Log("Character Controller is Null");
        }
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (_player == null)
        {
            Debug.Log("Player is not present");
        }

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Chase:
                Move();
                break;
            case EnemyState.Idle:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Attack()
    {
        if (Time.time > nextAttack)
        {
            if (_player == null) return;
            var health = _player.GetComponent<UniversalHealth>();
            if (health != null)
            {
                health.Damage(10);
                nextAttack = Time.time + attackDelay;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentState = EnemyState.Attack;

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        currentState = EnemyState.Chase;
    }


    private void Move()
    {
        if (_enemyController.isGrounded)
        {
            var direction = _player.position - transform.position;
            direction.Normalize();
            direction.y = 0;
            _velocity = direction * speed;
            transform.localRotation = Quaternion.LookRotation(_velocity);
        }
        
        _velocity.y += gravityValue * Time.deltaTime;
        _enemyController.Move(_velocity * Time.deltaTime);
    }
}
