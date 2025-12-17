using WMPLib;

namespace PMP_LAB09
{
    internal class SFXHandler
    {
        public static int MAXSFX = 15;

        static int current = 0;
        static WindowsMediaPlayer[] players = new WindowsMediaPlayer[MAXSFX];

        static SFXHandler()
        {
            for (int i = 0; i < MAXSFX; i++)
            {
                players[i] = new();
            }
        }

        public static void PlaySound(string url)
        {
            //Console.WriteLine($"Request to play {url}, player id {current}");
            players[current].URL = url;
            players[current].controls.play();

            current = (current + 1)%MAXSFX;
        }
    }
}
