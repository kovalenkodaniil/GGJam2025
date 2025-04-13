using _Core.StaticProvider;

namespace _Core.Scripts
{
    public class SoundDataProvider : IStaticDataProvider
    {
        public SoundConfig asset;

        public SoundDataProvider(SoundConfig asset)
        {
            this.asset = asset;
        }
    }
}