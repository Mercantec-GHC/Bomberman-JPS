
namespace BombermanGame.Source.Engine.Content
{
    public class FrameAnimator
    {
        private int frame;
        private int counter;
        private readonly int maxFrames;
        private readonly int speed;

        public FrameAnimator(int maxFrames, int speed = 25)
        {
            this.maxFrames = maxFrames;
            this.speed = speed;
            frame = 0;
            counter = 0;
        }

        public int UpdateAndGetFrame()
        {
            counter++;
            if (counter > speed)
            {
                counter = 0;
                frame++;
                if (frame >= maxFrames)
                {
                    frame = 0;
                }
            }

            return frame;
        }
    }
}
