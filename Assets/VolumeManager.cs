namespace Assets
{
    public static class VolumeManager
    {
        public static float Master;
        public static float Sfx;
        public static float Music;

        public static float GetSfxVolume()
        {
            return Master*Sfx;
        }
    }
}
