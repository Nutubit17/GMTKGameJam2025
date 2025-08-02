using UnityEngine;

public class BashInterfaces
{
    
}

public interface ICoinUseable
{
    void UseCoin();
}
public interface IFoodUseable
{
    void UseFood(int count);
}

public interface IAltInteractiveable
{
    void UseAltInteractive(PlayerArm plarm);
}
