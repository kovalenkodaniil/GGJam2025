using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.Tasks;
using _Core.StaticProvider;
using R3;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeModel
    {
        public ReactiveProperty<RewardAttribute> Graphic = new(new RewardAttribute(EnumReward.Graphic, 0));
        public ReactiveProperty<RewardAttribute> Popularity = new(new RewardAttribute(EnumReward.Popularity, 0));
        public ReactiveProperty<RewardAttribute> Quality = new(new RewardAttribute(EnumReward.Quality, 0));
        public ReactiveProperty<RewardAttribute> Gameplay = new(new RewardAttribute(EnumReward.Gameplay, 0));
        public ReactiveProperty<RewardAttribute> Profit = new(new RewardAttribute(EnumReward.Profit, 0));

        public List<ReactiveProperty<RewardAttribute>> _rewardAttributes;

        private List<OfficeConfig> _officeConfigs;
        private HandModel _handModel;
        public int _currentLevel = 1;
        public ReactiveProperty<int> Experience = new ReactiveProperty<int>();

        public event Action LevelUped;

        public OfficeModel(HandModel handModel)
        {
            _rewardAttributes = new List<ReactiveProperty<RewardAttribute>>
                { Graphic, Popularity, Quality, Gameplay, Profit };
            _handModel = handModel;

            _officeConfigs = new List<OfficeConfig>();
            _officeConfigs.AddRange(StaticDataProvider.Get<OfficeDataProvider>().asset.configs);
        }

        public void TakeReward(List<RewardAttribute> taskReward, int experience)
        {
            foreach (RewardAttribute attribute in taskReward)
            {
                ReactiveProperty<RewardAttribute> currentAttribute =
                    _rewardAttributes.FirstOrDefault(a => a.Value.type == attribute.type);

                if (currentAttribute == null)
                {
                    continue;
                }

                currentAttribute.Value =
                    new RewardAttribute(attribute.type, currentAttribute.Value.value + attribute.value);
            }

            Experience.Value += experience;
            CheckExperience();
        }

        public OfficeConfig GetOfficeConfig(int level)
        {
            return _officeConfigs.FirstOrDefault(officeConfig => officeConfig.level == level);
        }

        private void CheckExperience()
        {
            OfficeConfig currentOfficeConfig = GetOfficeConfig(_currentLevel);
            if (Experience.Value >= currentOfficeConfig.experience)
            {
                Experience.Value -= currentOfficeConfig.experience;
                UpdateLevel();
            }
        }

        private void UpdateLevel()
        {
            _handModel.CardsPerTurn++;
            _currentLevel++;

            LevelUped?.Invoke();
        }
    }
}