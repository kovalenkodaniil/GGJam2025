using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Employees
{
    [CreateAssetMenu(fileName = "New Employee", menuName = "Create new Employee")]
    public class EmployeeConfig : ScriptableObject
    {
        public string id;
        public string name;
        public Sprite icon;
        public EnumGender gender;
        public List<CharacterAttribute> characterictics;
    }

    public enum EnumGender
    {
        Man,
        Female,
        Ilya
    }

    public enum EnumСharacteristic
    {
        Technology,
        Creativity,
        SoftSkill
    }

    [Serializable]
    public class CharacterAttribute
    {
        public EnumСharacteristic type;
        public int value;
    }
}