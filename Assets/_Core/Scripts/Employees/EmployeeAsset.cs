using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Employees
{
    [CreateAssetMenu(fileName = "EmployeeAsset", menuName = "Create new EmployeeAsset")]
    public class EmployeeAsset : ScriptableObject
    {
        public List<EmployeeConfig> characters;

        [Header("Employee Icon")]
        public Sprite technologyIcon;
        public Sprite creativityIcon;
        public Sprite softSkillIcon;
    }
}