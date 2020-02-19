using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TerraFormmingMars.Logics;

namespace TerraFormmingMars.Entity.Corporation
{
    [CreateAssetMenu(fileName = "Corporation Data", menuName = "Scriptable Object/Corporation Data", order = int.MaxValue)]
    public class CorporationData : ScriptableObject
    {
        [SerializeField]
        private string corporationImageName;
        public string CorporationImageName { get { return corporationImageName; } }

        private Image corporationImage;
        public Image CorporationImage { set { corporationImage = value; } get { return corporationImage; } }

        [SerializeField]
        private List<Tag> tags;
        public List<Tag> Tags { get { return tags; } }

        [SerializeField]
        private List<Source> startSource;
        public List<Source> StartSource { get { return startSource; } }

        [SerializeField]
        private List<string> functionList;
        public List<string> FunctionList { get { return functionList; } }

        [SerializeField]
        private List<string> functionArguments;
        public List<string> FunctionArguments { get { return functionArguments; } }
    }
}