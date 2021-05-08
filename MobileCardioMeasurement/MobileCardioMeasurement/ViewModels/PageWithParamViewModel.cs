namespace MobileCardioMeasurement.ViewModels
{
    public class PageWithParamViewModel : BaseViewModel<string>
    {
        private string _param;

        public string Param
        {
            get => _param;
            private set => SetProperty(ref _param, value);
        }
        public override void Prepare(string param)
        {
            Param = param;
        }
    }
}