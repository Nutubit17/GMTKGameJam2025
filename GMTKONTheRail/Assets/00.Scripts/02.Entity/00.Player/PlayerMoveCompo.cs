using UnityEngine;
using UnityEngine.Events;
public class PlayerMoveCompo : MoveCompo,IGetCompoable
{
    public Transform cameraRoot;



    [SerializeField]
    float _maxSpeed = 10, _accelation = 25, _jumpPower = 5, _damp=3,_gravity = -11,_plHeight=2,_plRaius=0.26f; //PlayerState�� �� ������
    [SerializeField]
    float _maxSpeedModify=1,_accelationModify=1;
    [SerializeField]
    float _onAirSpeed,_onAirAccel,_onSlidingSpeed, _onSlidingAccel, _onSlideDamping=0,_deafaultDamping=2;
    [SerializeField]
    float _mouseSpeed = 5f;//PlayerSettingMing ���� ���ߵ�


    public bool _isCanJump, _isGround;

    [SerializeField]
    private LayerMask _whatIsGround;

    [SerializeField]
    private CapsuleCollider _capsuleCollider;

    private Vector2 _mouseSum;
    private Vector3 _movDir;

    private RaycastHit _groundCheck;

    Collider[] _groundCheckCols = new Collider[1];

    protected PlayerBash _player;

    protected PlayerSatus _playerStatus;


    private int _jumptmp = 0;

    public override void Init(Entity agent)
    {
        base.Init(agent);
        _player = agent as PlayerBash;
    }

    void Start()
    {
        _player.PlayerInput.jumpInputAction += Jump;
        _playerStatus = _player.GetCompo<PlayerSatus>();

 
        _plRaius = _capsuleCollider.radius* _capsuleCollider.transform.lossyScale.z - 0.01f;
        _plHeight = _capsuleCollider.height * _capsuleCollider.transform.lossyScale.y - _plRaius;
    }

    // Update is called once per frame
    private void Update()
    {
        RotateCamera();
    }
    void FixedUpdate()
    {
        _isCanJump = false;
        _isGround = false;

        float accelModify = _accelationModify, maxSpeedModify = _maxSpeedModify;

        Vector3 input = BashUtils.V2toV3(_player.PlayerInput.movement);
        //_movDir = BashUtils.V3X0Z(cameraRoot.TransformVector(input)).normalized;
        input = (Quaternion.Euler(0, _mouseSum.x, 0) * input);

        //���� ��� ���� ������ �����ϱ� ���� ��� ����(Overlap)�� ������ ���� ����(SphereCast)�� ��������.
        if (Physics.OverlapSphereNonAlloc(transform.position-Vector3.up*_plHeight/2,_plRaius+0.1f,_groundCheckCols,_whatIsGround) > 0) //������ üũ(�γ��� ����)
        {
            //_isCanJump = true;

            if (Physics.SphereCast(transform.position, _plRaius, -transform.up, out _groundCheck, _plHeight / 2, _whatIsGround))//������ üũ(������ ����)
            {
                _isGround = true;
                _isCanJump = true;
                //OnGorund(
                //����O ����O
                MoveOnGorund(ref input);
            }
            else
            {
                //OnAirAndGround?
                //����x ����O
                accelModify = _onAirAccel;
                maxSpeedModify = _onAirSpeed;

                rigidCompo.AddForce(Vector3.up*_gravity,ForceMode.Impulse);
            }
        }
        else
        {
            //OnAir

            accelModify = _onAirAccel;
            maxSpeedModify = _onAirSpeed;
            //����X ����X
        }

        //rigidCompo.linearDamping = _deafaultDamping;
        if (_player.PlayerInput.IsSliding && _playerStatus.CurrentStemina >0)
        {
            accelModify *= _onSlidingAccel;
            maxSpeedModify *= _onSlidingSpeed;
            _playerStatus.AddStemina(-Time.deltaTime);
            _playerStatus.SetSteminaRegen(false);
            //rigidCompo.linearDamping = _onSlideDamping;
        }
        else
        {
            _playerStatus.SetSteminaRegen(true);
        }

        //    _movDir = input * (Mathf.Lerp(1, 0, (Vector3.Project(input, rigidbody.velocity) + rigidbody.velocity).magnitude / _maxSpeed)
        //+ Vector3.Project(input, -rigidbody.velocity.normalized).magnitude);

      
        _movDir = input * Mathf.Lerp(1, 0, (Vector3.Project(input, rigidCompo.linearVelocity)+rigidCompo.linearVelocity).magnitude / _maxSpeed / maxSpeedModify) * accelModify * _accelation;
 
       // _movDir = input * Mathf.Lerp(1, 0, (Vector3.Project(rigidCompo.linearVelocity, input) + input).magnitude / _maxSpeed / maxSpeedModify) * accelModify * _accelation;


        //_movDir += Vector3.Project(input, Quaternion.Euler(0, 90, 0) * rigidbody.velocity.normalized);
        //_movDir += Vector3.Project(input, Quaternion.Euler(0, 270, 0) * rigidbody.velocity.normalized);

        //rigidCompo.linearVelocity += _movDir;
        rigidCompo.AddForce(_movDir,ForceMode.Impulse);



    }

    private void Jump()
    {
        //if(_isCanJump) 
        //rigidCompo.AddForce(transform.up*_jumpPower,ForceMode.Impulse);
        if (_isCanJump)
        {
            transform.position = transform.position + Vector3.up * 0.01f;
            rigidCompo.AddForce(transform.up * (_jumpPower + _gravity), ForceMode.Impulse);
        }

    }

    private void MoveOnGorund(ref Vector3 input)
    {
        Vector3 horizontalSpeed = BashUtils.V3X0Z(rigidCompo.linearVelocity);
        //
        rigidCompo.linearVelocity = Vector3.up * rigidCompo.linearVelocity.y
           + Vector3.Lerp(horizontalSpeed, Vector3.zero, _damp * Time.fixedDeltaTime);
        //����
        //rigidCompo.AddForce(Vector3.Lerp(Vector3.zero,-horizontalSpeed, _damp * Time.fixedDeltaTime),ForceMode.Impulse);


        input = Vector3.ProjectOnPlane(input,_groundCheck.normal);
        //�������� ��� ���� �� �ְ��ϴ� �ڵ�;
    }

    private void MoveOnAir()
    {

    }

    private void RotateCamera()
    {
        //_mouseTmp += RPlayerMana.Instance.playerInput.mouseMov*_mouseSpeed;
        _mouseSum.x += Input.GetAxisRaw("Mouse X") * _mouseSpeed ;
        _mouseSum.y -= Input.GetAxisRaw("Mouse Y")  *_mouseSpeed;
        _mouseSum.y = Mathf.Clamp(_mouseSum.y, -89, 89);

        cameraRoot.rotation = Quaternion.Euler(_mouseSum.y, _mouseSum.x, 0);
    }
}
