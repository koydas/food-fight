namespace Assets
{
    public static class VolumeManager
    {
        public static float Master = .5f;
        public static float Sfx = .5f;
        public static float Music = .5f;

        public static float GetSfxVolume()
        {
            return Master*Sfx;
        }
    }
}
