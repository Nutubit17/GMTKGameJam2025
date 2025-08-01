using UnityEngine;

public enum EnemyState
{
    None,

    Wait,

    Chase,
    Chase_Walk,
    Chase_Run,
    Chase_Attack,
    
    Freeze,
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

    public static class Hit
    {
        public const int _1 = 0;
        public const int Snake = 1;
        public const int Spider = 2;
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


public struct StateInfo<T>
    where T : System.Enum
{
    public T PreviousState { get; private set; }
    public T CurrentState { get; private set; }

    public StateInfo(T defaultState)
    {
        CurrentState = defaultState;
        PreviousState = default;
    }

    public void ChangeState(T state)
    {
        PreviousState = CurrentState;
        CurrentState = state;
    }

    public void Welldone()
    {
        PreviousState = CurrentState;
    }

    public bool IsEnter() => !PreviousState.Equals(CurrentState);
}

    [System.Serializable]
    public struct Timer
    {
        [SerializeField] private float waitTime;
        private float startTime;

        public void Start()
        {
            startTime = Time.time;
        }

        public bool Check() => startTime + waitTime < Time.time;
    }

