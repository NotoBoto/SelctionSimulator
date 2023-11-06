using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityModel
{
    private float _speed;
    public float Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    private float _size;
    public float Size
    {
        get { return _size; }
        set { _size = value; }
    }

    private float _maxHP;
    public float MaxHP
    {
        get { return _maxHP; }
        set { _maxHP = value; }
    }

    private float _HP;
    public float HP
    {
        get { return _HP; }
        set { _HP = value; }
    }

    private float _damage;
    public float Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }

    private Color _color;
    public Color Color
    {
        get { return _color; }
        set { _color = value; }
    }

    private int _foodCount;
    public int FoodCount
    {
        get { return _foodCount; }
        set { _foodCount = value; }
    }

    private Vector3 _homePosition;
    public Vector3 HomePosition
    {
        get { return _homePosition; }
        set { _homePosition = value; }
    }
}
