namespace Messages
{
    public struct SpawnBonusMessage
    {
        public float PlatformXPosition { get; }

        public SpawnBonusMessage(float platformXPosition)
        {
            PlatformXPosition = platformXPosition;
        }
    }
}

