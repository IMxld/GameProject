using UnityEngine;

//词条方法库
public class EntryVoid : MonoBehaviour
{
    protected void ChangeDecreaseStaminaSpeed(float _changeNum, Character _character)//修改演员体力损耗速度
    {
        _character.StaminaDecreaseSpeed *= 1 + _changeNum;
    }

    protected void ChangeRecoverStaminaSpeed(float _changeNum, Character _character)//修改演员体力恢复速度
    {
        _character.StaminaRecoverSpeed *= 1 + _changeNum;
    }

    protected void ChangeSalary(float _changeNum,Character _character)//修改演员周薪值
    {
        _character.Salary *= 1 + _changeNum;
    }

    protected void ChangePecfectChance(float _changeNum, MainDrama _mainDrama)//修改完美率
    {
        _mainDrama.PerfectChance += _changeNum;
    }

    protected void ChangePerfectValue(float _changeNum, Character _character)//修改完美值
    {
        _character.PerfectValue *= 1 + _changeNum;

    }

    protected void ChangePecfectIncomesRate(float _changeNum, MainDrama _mainDrama)//修改完美收入倍率
    {
        _mainDrama.PerfectIncomesRate += _changeNum;
    }
    protected void ChangePecfectFansRate(float _changeNum, MainDrama _mainDrama)//修改完美粉丝倍率
    {
        _mainDrama.PerfectFansRate += _changeNum;
    }

    protected void ChangeProperty(float _changeNum, PlayerController _playerController)//修改剧院财产
    {
        float mutiply = _playerController.Property * (1 + _changeNum);
        _playerController.Property = (int)mutiply;
    }

    protected void ChangeRoutineExpenses(float _changeNum, PlayerController _playerController)//修改剧院杂费
    {
        float mutiply = _playerController.RoutinePayment * (1 + _changeNum);
        _playerController.RoutinePayment = (int)mutiply;
    }

    protected void ChangeIncome(float _changeNum, MainDrama _mainDrama)//修改演出收入
    {
        _mainDrama.DramaIncome *= 1 + _changeNum;
    }

    protected void ChangeFans(float _changeNum, MainDrama _mainDrama)//修改演出粉丝增量
    {
        _mainDrama.FansIncrease *= 1 + _changeNum;
    }

}