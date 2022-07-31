using _DunkBall.Scripts.EventLayer;

namespace _DunkBall.Scripts.Data
{
    public static class DataSaver
    {
        private static GlobalData _globalData;

        public static GlobalData GlobalData
        {
            // Can bee updated to save to Json
            get => _globalData ??= new GlobalData();
            set => _globalData = value;
        }
    }

    public class GlobalData
    {
        private int _totalScore;
        private int _totalStars;
        public int TotalScore => _totalScore;
        public int TotalStars => _totalStars;

        public void AddScore(int score)
        {
            _totalScore += score;
            EventBus.OnTotalScoreChange?.Invoke();
        }

        public void AddStars(int stars)
        {
            _totalStars += stars;
            EventBus.OnStarsChange?.Invoke();
        }
    }
}