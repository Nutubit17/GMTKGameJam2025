using UnityEngine;

public class LightLantern : LightObject, IAltInteractiveable
{
    [SerializeField] private float _fuelPerClick = 0.01f;

    public void UseAltInteractive(PlayerArm plarm)
    {
        AddFuel(_fuelPerClick);
    }
}
