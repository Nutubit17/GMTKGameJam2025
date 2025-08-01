using System;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyState
{
    Attack,
    Idle,
    Walk,
    Run,
}

public static class AnimationBlend
{

    public static class Attack
    {
        public const int _1 = 0;
        public const int _2 = 1;
        public const int Snake = 2;
        public const int Spider = 3;
    }

    public static class Idle
    {
        public const int _1 = 0;
        public const int _2 = 1;
        public const int _3 = 2;
        public const int _4 = 3;
        public const int _5 = 4;
        public const int _6 = 5;
        public const int _7 = 6;
        public const int Snake = 7;
        public const int Spider = 8;
    }

    public static class Walk
    {
        public const int _1 = 0;
        public const int Snake = 1;
        public const int Spider = 2;
    }
}

public class EnemyBase : Entity
{
    [SerializeField] private EnemyInfoSO _enemyInfo;
    [SerializeField] private PlayerBash _player;
    [Space]

    [Header("Animator")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _visual;
    [SerializeField] private EnemyAnimationTrigger _enemyAnimationTrigger;
    [SerializeField] private EnemyState _previousState;
    [SerializeField] private EnemyState _currentState;

    private readonly int _blendHash = Animator.StringToHash("blend");
    private readonly int _IdleHash = Animator.StringToHash("Idle");
    private readonly int _attackHash = Animator.StringToHash("Attack");
    private readonly int _walkHash = Animator.StringToHash("Walk");
    private readonly int _runHash = Animator.StringToHash("Run");
    private readonly int _helloHash = Animator.StringToHash("Hello");
    private bool _previousEnemyUpdateState = false;
    [SerializeField] private float _freezeTime = 5f;
    private float _startFreezeTime = float.MaxValue;


    [Header("Range")]
    [SerializeField] private float _playerDetectRange = 10f;
    [SerializeField] private float _lightDetectRange = 5f;
    [Space]
    [SerializeField] private float _runRange = 2f;
    [SerializeField] private float _attackRange = 1.5f;
    [Space]

    [Header("Light")]
    [SerializeField] private LayerMask _lightLayer;
    [SerializeField, Range(0.5f, 1f)] private float _rotateTransition = 0.75f;

    private Collider[] _lightColliderCheckArr = new Collider[10];

    protected override void Awake()
    {
        base.Awake();
        _enemyInfo = Instantiate(_enemyInfo);
        _player = FindAnyObjectByType<PlayerBash>();

        _enemyAnimationTrigger.OnAnimationEnd += HandleAnimationEnd;
    }

    public void UpdateState()
    {
        _previousState = _currentState;
    }

    public bool IsFrozen() => _startFreezeTime + _freezeTime > Time.time;

    public void FixedUpdate()
    {
        // root motion problem solving code
        var temp = _visual.transform.position;
        temp.y = 0;
        _visual.transform.position = temp;


        float distanceToPlayer = (_player.transform.position - transform.position).magnitude;
        bool isPlayerInRange = distanceToPlayer < _playerDetectRange * _playerDetectRange;

        int LightCount = Physics.OverlapSphereNonAlloc
            (transform.position, _lightDetectRange, _lightColliderCheckArr, _lightLayer);

        bool isLightExists = false;

        for (int i = 0; i < LightCount; ++i)
        {
            if (_lightColliderCheckArr[i].TryGetComponent<LightObject>(out var light))
            {
                if (light.IsLightOn)
                {
                    isLightExists = true;
                    break;
                }
            }
        }

        if (isPlayerInRange && !isLightExists)
        {
            _previousEnemyUpdateState = true;
            EnemyUpdate(distanceToPlayer);
        }
        else
        {
            if (_previousEnemyUpdateState)
            {
                _previousEnemyUpdateState = false;
                _startFreezeTime = Time.time;
            }

            NormalUpdate();
        }

        UpdateState();
    }

    private void EnemyUpdate(float distance)
    {
        _animator.speed = 1;

        Vector3 position = _player.transform.position;
        Vector3 dir = (position - transform.position).normalized;
        dir.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, dir, _rotateTransition);

        //if (IsFrozen()) return;

        float lastSpeed = 0;

        if (distance < _attackRange)
        {
            _animator.applyRootMotion = true;
            _animator.SetTrigger(_attackHash);

            _currentState = EnemyState.Attack;
            OnAttack();
            return;
        }
        else if (distance < _runRange)
        {
            _animator.applyRootMotion = false;
            _animator.SetTrigger(_runHash);
            _currentState = EnemyState.Run;
            lastSpeed = _enemyInfo.runSpeed;
        }
        else
        {
            _animator.applyRootMotion = false;
            _animator.SetTrigger(_walkHash);
            _currentState = EnemyState.Walk;
            OnWalk();
            lastSpeed = _enemyInfo.speed;
        }

        transform.position += dir * lastSpeed * Time.fixedDeltaTime;
    }

    private void NormalUpdate()
    {
        if (IsFrozen())
        {
            _animator.speed = 0f;
        }
        else
        {
            _animator.speed = 0.75f;
            _animator.SetTrigger(_IdleHash);
            _currentState = EnemyState.Idle;
            OnIdle();
        }
    }

    protected virtual void OnIdle()
    {
        if(_currentState != _previousState) // 지금 막 idle이 된 것이라면
            _animator.SetFloat(_blendHash, Random.Range(AnimationBlend.Idle._1, AnimationBlend.Idle._7));
    }

    public virtual void OnWalk()
    {
        _animator.SetFloat(_blendHash, AnimationBlend.Walk._1);
    }


    private void OnAttack()
    {
        _animator.SetFloat(_blendHash, Random.Range(AnimationBlend.Attack._1, AnimationBlend.Attack._2));
    }


    private void HandleAnimationEnd()
    {
        _animator.SetTrigger(_IdleHash);
        _animator.SetFloat(_blendHash, AnimationBlend.Idle._1);
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _runRange);

        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, _playerDetectRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _lightDetectRange);
    }
#endif
}
