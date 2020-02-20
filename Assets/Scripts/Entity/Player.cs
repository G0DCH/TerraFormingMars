using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerraFormmingMars.Entity
{
    public class Player
    {
        private Source MC;
        private Source Iron;
        private Source Titanium;
        private Source Plant;
        private Source Energy;
        private Source Heat;

        private Corporation.Corporation corporation;

        private List<Card.Card> hands;
        private List<Card.Card> selectHands;

        //사용한 카드들
        private List<Card.Card> activeCards;
        private List<Card.Card> automatedCards;
        private List<Card.Card> eventCards;

        private int terraformmingLevel;
    }
}