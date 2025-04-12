using _Core.Scripts.TurnManagerScripts;
using _Core.StaticProvider;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeDataProvider : IStaticDataProvider
    {
        public OfficeAsset asset;
    
        public OfficeDataProvider(OfficeAsset asset)
        {
            this.asset = asset;
        }
    }
}