using UnityEngine;

//����������
public class EntryVoid : MonoBehaviour
{
    protected void ChangeDecreaseStaminaSpeed(float _changeNum, Character _character)//�޸���Ա��������ٶ�
    {
        _character.StaminaDecreaseSpeed *= 1 + _changeNum;
    }

    protected void ChangeRecoverStaminaSpeed(float _changeNum, Character _character)//�޸���Ա�����ָ��ٶ�
    {
        _character.StaminaRecoverSpeed *= 1 + _changeNum;
    }

    protected void ChangeSalary(float _changeNum,Character _character)//�޸���Ա��нֵ
    {
        _character.Salary *= 1 + _changeNum;
    }

    protected void ChangePecfectChance(float _changeNum, MainDrama _mainDrama)//�޸�������
    {
        _mainDrama.PerfectChance += _changeNum;
    }

    protected void ChangePerfectValue(float _changeNum, Character _character)//�޸�����ֵ
    {
        _character.PerfectValue *= 1 + _changeNum;

    }

    protected void ChangePecfectIncomesRate(float _changeNum, MainDrama _mainDrama)//�޸��������뱶��
    {
        _mainDrama.PerfectIncomesRate += _changeNum;
    }
    protected void ChangePecfectFansRate(float _changeNum, MainDrama _mainDrama)//�޸�������˿����
    {
        _mainDrama.PerfectFansRate += _changeNum;
    }

    protected void ChangeProperty(float _changeNum, PlayerController _playerController)//�޸ľ�Ժ�Ʋ�
    {
        float mutiply = _playerController.Property * (1 + _changeNum);
        _playerController.Property = (int)mutiply;
    }

    protected void ChangeRoutineExpenses(float _changeNum, PlayerController _playerController)//�޸ľ�Ժ�ӷ�
    {
        float mutiply = _playerController.RoutinePayment * (1 + _changeNum);
        _playerController.RoutinePayment = (int)mutiply;
    }

    protected void ChangeIncome(float _changeNum, MainDrama _mainDrama)//�޸��ݳ�����
    {
        _mainDrama.DramaIncome *= 1 + _changeNum;
    }

    protected void ChangeFans(float _changeNum, MainDrama _mainDrama)//�޸��ݳ���˿����
    {
        _mainDrama.FansIncrease *= 1 + _changeNum;
    }

}