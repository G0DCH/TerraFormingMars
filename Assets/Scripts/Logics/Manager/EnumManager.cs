using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Logics.Manager
{
    /// <summary>
    /// string을 Enum 타입으로 바꿔주는 함수 탑재
    /// </summary>
    public class EnumManager : Utility.Singleton<EnumManager>
    {
        public object StringToEnumType(string name)
        {
            Environment environment;
            if(System.Enum.TryParse<Environment>(name, out environment))
            {
                return environment;
            }

            Tag tag;
            if(System.Enum.TryParse<Tag>(name, out tag))
            {
                return tag;
            }

            TileType tileType;
            if(System.Enum.TryParse<TileType>(name, out tileType))
            {
                return tileType;
            }

            Debug.LogError("입력된 이름 " + name + "에 해당하는 Name 값이 없습니다.");
            return null;
        }

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
    }
}