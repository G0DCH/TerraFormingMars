using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Logics.Manager
{
    /// <summary>
    /// string을 Environment, Tag, TileType 타입으로 바꿔주는 함수 탑재
    /// </summary>
    public class EnumManager : Utility.Singleton<EnumManager>
    {
        /// <summary>
        /// Source 타입은 제외하고 됨
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public object StringToEnumType(string name)
        {
            PLANETINDICATORS planetIndicators;
            if(System.Enum.TryParse<PLANETINDICATORS>(name, out planetIndicators) == true)
            {
                return planetIndicators;
            }

            Tag tag;
            if(System.Enum.TryParse<Tag>(name, out tag) == true)
            {
                return tag;
            }

            TileType tileType;
            if(System.Enum.TryParse<TileType>(name, out tileType) == true)
            {
                return tileType;
            }

            CardType cardType;
            if (System.Enum.TryParse<CardType>(name, out cardType) == true)
            {
                return cardType;
            }

            Debug.LogError("입력된 이름 " + name + "에 해당하는 Name 값이 없습니다.");
            return null;
        }

        /// <summary>
        /// Source 타입은 제외하고 됨
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public object[] StringsToEnumTypes(string[] names)
        {
            List<object> EnumTypeList = new List<object>();

            foreach(string name in names)
            {
                object convertResult = StringToEnumType(name);

                EnumTypeList.Add(convertResult);
            }

            return EnumTypeList.ToArray();
        }

        public SourceType StringToSourceType(string name)
        {
            SourceType sourceType;
            if (System.Enum.TryParse<SourceType>(name, out sourceType) == true)
            {
                return sourceType;
            }

            Debug.LogError("입력된 이름 " + name + "에 해당하는 Name 값이 없습니다.");
            return SourceType.Empty;
        }

        public SourceType[] StringsToSourceTypes(string[] names)
        {
            List<SourceType> sourceTypeList = new List<SourceType>();

            foreach (string name in names)
            {
                SourceType convertResult = StringToSourceType(name);

                sourceTypeList.Add(convertResult);
            }

            return sourceTypeList.ToArray();
        }
    }
}