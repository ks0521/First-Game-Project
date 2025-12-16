using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Kamikakushi.Utills.Structs;

namespace _Kamikakushi.Utills.Enums
{
    public enum PromptKey
    {
        None = 0,

        //������
        OpenDoor, //Ư���������ʿ����� ������ (E : ������), (����ȭ��ǥ���� ũ�ν�����)
        LockDoor, //Ư�����������ʿ��� ������ (E : ��������),  (�������� ũ�ν�����)
        CloseDoor, //���ݱ� (E : ���ݱ�), (�Ʒ���ȭ��ǥ���� ũ�ν�����)

        //�����۰���
        PickupItem, //������ �ݱ� (F : ����), (���� ���� ũ�ν�����)
        UseItem, //������ �����ϱ�

        //ȯ�� ��ȣ�ۿ�
        AcivateSwitch, //����ġ(�ʿ��Ҽ��������Ͱ��Ƽ�)
        Inspect, //���� (F : ����), (���� ���� ũ�ν�����)

        //����
        Hide, //���� (Ctrl : ����), (������ ���� ���� ũ�ν�����)
        HideTransform //������ ī�޶� �����̵� (����ƴ���� �ü��̵��ϴ� ����)
    }
}