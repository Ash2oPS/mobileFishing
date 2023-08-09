namespace EtienneSibeaux.UI
{
    public class C_UI_FishCounter : CA_UI_Counter
    {
        private int _maxFish;

        public int MaxFish { get => _maxFish; }

        public void SetMaxFish(int maxFish)
        {
            _maxFish = maxFish;
            UpdateText();
        }

        protected override void UpdateText()
        {
            _counterText.text = _currentValue.ToString() + "/" + _maxFish;
        }
    }
}