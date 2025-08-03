using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : HealthCompo
{
    [SerializeField] private EnemyInfoSO _enemyInfo;
    [SerializeField] private PlayerBash _player;

    [Space]

    [Header("Animator")]
    [SerializeField] protected Animator _animator;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Transform _visual;
    [SerializeField] protected Rigidbody _ragdoll;
    [SerializeField] private EnemyAnimationTrigger _enemyAnimationTrigger;
    [Space]
    [SerializeField] private Timer _freezeTimer;
    [SerializeField] private Timer _attackTimer;
    [SerializeField] private Timer _hitCheckTimer;
    [SerializeField] private bool _useRunAnim;

    protected readonly int _blendHash = Animator.StringToHash("blend");
    protected readonly int _NoneHash = Animator.StringToHash("None");
    protected readonly int _IdleHash = Animator.StringToHash("Idle");
    protected readonly int _attackHash = Animator.StringToHash("Attack");
    protected readonly int _walkHash = Animator.StringToHash("Walk");
    protected readonly int _runHash = Animator.StringToHash("Run");
    protected readonly int _hitHash = Animator.StringToHash("Hit");
    private int _previousHash = 0;
    
    [Header("Light")]
    [SerializeField] private LayerMask _lightLayer;
    [SerializeField, Range(0.5f, 1f)] private float _rotateTransition = 0.75f;
    private Collider[] _lightColliderCheckArr = new Collider[10];

    protected virtual int _defaultIdleBlend => AnimationBlend.Idle._1;
    protected virtual int _idleBlendStart => AnimationBlend.Idle._1;
    protected virtual int _idleBlendEnd => AnimationBlend.Idle._7;

    protected virtual int _attackBlendStart => AnimationBlend.Attack._1;
    protected virtual int _attackBlendEnd => AnimationBlend.Attack._2;

    protected virtual int _walkBlend => AnimationBlend.Walk._1;
    protected virtual int _hitBlend => AnimationBlend.Hit._1;

    private bool _isDie = false;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    _enemyInfo = Instantiate(_enemyInfo);
    //    _player = FindAnyObjectByType<PlayerBash>();

    //    _enemyAnimationTrigger.OnAnimationEnd += HandleAnimationEnd;
    //    StartCoroutine(StateOuterRoutine());
    //}
    public override void Awake()
    {
        base.Awake();
        _enemyInfo = Instantiate(_enemyInfo);
        _player = FindAnyObjectByType<PlayerBash>();

        _enemyAnimationTrigger.OnAnimationEnd += HandleAnimationEnd;
        StartCoroutine(StateOuterRoutine());
    }

    private void HandleAnimationEnd()
    {
        PlayAnimation(_IdleHash, _defaultIdleBlend);
    }

    public override void Damage(float damage)
    {
        OnHit(damage);
    }
    public void OnHit(float damage)
    {
        _enemyInfo.hp -= damage;

        if (_enemyInfo.hp <= 0)
        {
            Die();
        }
        

        // hit 동안 statemachine 정지
        _hitCheckTimer.Start();
    }

    private void Die()
    {
        _isDie = true;
        //_visual.gameObject.SetActive(false);
        //_ragdoll.gameObject.SetActive(true);
        _animator.enabled = false;
        _ragdoll.AddForce(transform.forward * -20);

        //Destroy(gameObject, 100f);
    }

    public IEnumerator StateOuterRoutine()
    {
        bool isActive = true;
        var routine = StateRoutine();
        StartCoroutine(routine);

        while (true)
        {
            if (!_hitCheckTimer.Check() && isActive) // 지금 막 hit 이라면
            {
                StopCoroutine(routine);
                isActive = false;

                PlayAnimation(_hitHash, _hitBlend);
                _rigidbody.AddForce(-transform.forward * 50);
            }
            else if (_hitCheckTimer.Check() && !isActive)
            {
                StartCoroutine(routine);
                isActive = true;
            }

            if (_isDie)
            {
                StopCoroutine(routine);
                break;
            }

            yield return new WaitForFixedUpdate();
        }
        
    }

    public IEnumerator ChaseStateRoutine()
    {
        var state = new StateInfo<EnemyState>(EnemyState.Chase_Walk);

        while (true)
        {
            float speed = 0;

            Vector3 dir = _player.transform.position - transform.position;
            float distance = dir.magnitude;
            dir.Normalize();
            dir.y = 0;


            switch (state.CurrentState)
            {

                case EnemyState.Chase_Walk:

                    {
                        _animator.speed = 1;
                        if (distance <= _enemyInfo.RunRange)
                        {
                            state.ChangeState(EnemyState.Chase_Run);
                            break;
                        }

                        if (state.IsEnter())
                        {
                            PlayAnimation(_walkHash, _walkBlend);
                        }
                        speed = _enemyInfo.speed;
                    }
                    state.Welldone();
                    break;


                case EnemyState.Chase_Run:

                    {
                        if (distance <= _enemyInfo.AttackRange)
                        {
                            state.ChangeState(EnemyState.Chase_Attack);
                            break;
                        }

                        if (distance >= _enemyInfo.RunRange)
                        {
                            state.ChangeState(EnemyState.Chase_Walk);
                            break;
                        }

                        if (state.IsEnter())
                        {
                            if (_useRunAnim)
                            {
                                PlayAnimation(_runHash);
                                _animator.speed = 1;
                            }
                            else
                            {
                                PlayAnimation(_walkHash, _walkBlend);
                                _animator.speed = 1.5f;
                            }
                        }

                        speed = _enemyInfo.runSpeed;
                    }

                    state.Welldone();
                    break;


                case EnemyState.Chase_Attack:

                    {
                        _animator.speed = 1;
                        if (distance >= _enemyInfo.AttackRange)
                        {
                            state.ChangeState(EnemyState.Chase_Run);
                            break;
                        }

                        if (state.IsEnter() || _attackTimer.Check())
                        {
                            _attackTimer.Start();
                            PlayAnimation(_attackHash, Random.Range(_attackBlendStart, _attackBlendEnd));
                        }


                    }

                    state.Welldone();
                    break;

            }

            _rigidbody.linearVelocity = dir * speed;
            _visual.transform.forward = dir;

            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator StateRoutine()
    {
        yield return null;
        yield return null;
        yield return null;
        
        var state = new StateInfo<EnemyState>(EnemyState.Wait);
        IEnumerator chaseStateMachine = ChaseStateRoutine();

        while (true)
        {
            switch (state.CurrentState)
            {
                case EnemyState.Wait:

                    if (TryFindTarget())
                    {
                        state.ChangeState(EnemyState.Chase);
                        break;
                    }
                    else
                    {
                        if (state.IsEnter())
                        {
                            _animator.speed = 1;
                            PlayAnimation(_IdleHash, Random.Range(_idleBlendStart, _idleBlendEnd));
                        }
                        
                    }

                    state.Welldone();
                    break;


                case EnemyState.Chase:

                    if (state.IsEnter())
                    {
                        StartCoroutine(chaseStateMachine);
                        _animator.speed = 1;
                    }

                    if (TryFindTarget() == false)
                    {
                        StopCoroutine(chaseStateMachine);
                        state.ChangeState(EnemyState.Freeze);
                        break;
                    }

                    if (Vector3.Distance(transform.position, _player.transform.position) > _enemyInfo.PlayerDetectRange)
                    {
                        StopCoroutine(chaseStateMachine);
                        state.ChangeState(EnemyState.Wait);
                        break;
                    }

                    state.Welldone();
                    break;


                case EnemyState.Freeze:

                    if (state.IsEnter())
                    {
                        _freezeTimer.Start();
                        _animator.speed = 0;
                    }

                    if (TryFindTarget())
                    {
                        state.ChangeState(EnemyState.Chase);
                        break;
                    }
                    else if (_freezeTimer.Check())
                    {
                        state.ChangeState(EnemyState.Wait);
                        break;
                    }
                    state.Welldone();
                    break;

            }

            yield return new WaitForFixedUpdate();
        }

    }

    public void PlayAnimation(int hash, int blend)
    {
        _animator.SetInteger(_blendHash, blend);
        PlayAnimation(hash);
    }

    public void PlayAnimation(int hash)
    {
        _animator.SetBool(_previousHash, false);
        _animator.SetBool(hash, true);
        
        _previousHash = hash;
    }

    public bool TryFindTarget()
    {
        var temp = _visual.transform.position;
        temp.y = 0;
        _visual.transform.position = temp;


        float distanceToPlayer = (_player.transform.position - transform.position).sqrMagnitude;
        bool isPlayerInRange = distanceToPlayer < _enemyInfo.PlayerDetectRange * _enemyInfo.PlayerDetectRange;

        int LightCount = Physics.OverlapSphereNonAlloc
            (transform.position, _enemyInfo.LightDetectRange, _lightColliderCheckArr, _lightLayer);

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

        return isPlayerInRange && !isLightExists;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_enemyInfo == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _enemyInfo.AttackRange);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemyInfo.RunRange);

        Gizmos.color = Color.pink;
        Gizmos.DrawWireSphere(transform.position, _enemyInfo.PlayerDetectRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _enemyInfo.LightDetectRange);
    }
#endif
}
