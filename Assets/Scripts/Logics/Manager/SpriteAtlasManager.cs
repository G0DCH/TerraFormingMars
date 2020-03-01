using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;

namespace TerraFormmingMars.Logics.Manager
{
    public class SpriteAtlasManager : Utility.Singleton<SpriteAtlasManager>
    {
        public SpriteAtlas ActiveCards;
        public SpriteAtlas AutomatedCards;
        public SpriteAtlas EventCards;
        public SpriteAtlas Corporations;
        public SpriteAtlas Icons;

        public Image image;

        private void Start()
        {
            image.sprite = GetCardSprite("Card_Event_001");
        }

        public Sprite GetCardSprite(string imageName)
        {
            Sprite resultSprite = null;
            if(imageName.Contains("Active") == true)
            {
                resultSprite = ActiveCards.GetSprite(imageName);
            }
            else if(imageName.Contains("Automated") == true)
            {
                resultSprite = AutomatedCards.GetSprite(imageName);
            }
            else if(imageName.Contains("Event") == true)
            {
                resultSprite = EventCards.GetSprite(imageName);
            }
            else
            {
                Debug.LogError(imageName + "은 카드 이름이 아닙니다.");
                return null;
            }

            if (resultSprite == null)
            {
                Debug.LogError(imageName + "에 해당하는 스프라이트가 없습니다.");
            }

            return resultSprite;
        }
    }
}