using UnityEngine;

public interface IMovementStrategy
{
    // enemyController ����� ������������ ��� ������� � ����������� ����� �����
    // (��������, finalDestination ��� �����) ��� ��� ��������� ��������� �����.
    void Move(Transform enemyTransform, GameObject player, float speed, Enemy enemyController);
}