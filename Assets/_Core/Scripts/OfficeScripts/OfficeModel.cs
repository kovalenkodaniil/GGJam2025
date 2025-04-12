using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.Tasks;
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
        
        private List<ReactiveProperty<RewardAttribute>> _rewardAttributes;

        public OfficeModel()
        {
            _rewardAttributes = new List<ReactiveProperty<RewardAttribute>> {Graphic, Popularity, Quality, Gameplay, Profit};
        }
        
        public void TakeReward(TaskRewardConfig taskReward)
        {
            foreach (RewardAttribute attribute in taskReward.rewardAttributes)
            {
                ReactiveProperty<RewardAttribute> currentAttribute = _rewardAttributes.FirstOrDefault(a => a.Value.type == attribute.type);

                if (currentAttribute == null)
                {
                    continue;
                }

                currentAttribute.Value.value += attribute.value;
            }
        }
    }
}