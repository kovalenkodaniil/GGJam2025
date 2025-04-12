using _Core.StaticProvider;

namespace _Core.Scripts.TurnManagerScripts
{
    public class TurnDataProvider : IStaticDataProvider
    {
        public TurnAsset asset;
        
        public TurnDataProvider(TurnAsset asset)
        {
            this.asset = asset;
        }
    }
}