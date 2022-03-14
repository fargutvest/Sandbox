using System;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RPA.Report;

namespace Application.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private CredentialsElement _selectedCredentials;
        public CredentialsElement SelectedCredentials
        {
            get { return _selectedCredentials; }
            set
            {
                _selectedCredentials = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCredentials)));
            }
        }

        private IReport _report => new Report(() => ReportOut, (text) => ReportOut = text);


        public event PropertyChangedEventHandler PropertyChanged;

        public CollectionView CredentialsCollection { get; }

        public ObservableCollection<BusinessFlowItemModel> BusinessFlows { get; set; }

        private double _height;
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Height)));
            }
        }

        public MainViewModel()
        {
            var business = new Start(_report);

            var allCredentials = Settings.GetAllCredentials();
            CredentialsCollection = new CollectionView(allCredentials);
            SelectedCredentials = Settings.SelectCredentials();
            var hiddenButtonsDisplayNames = Settings.GetAllHiddenButtons().Cast<HiddenButtonElement>().Select(_=>_.Id).ToList();

            BusinessFlows = new ObservableCollection<BusinessFlowItemModel>();
            var methods = typeof(Start).GetMethods(BindingFlags.Instance | BindingFlags.Public).Where(_ => _.ReturnType == typeof(void)).ToList();
            foreach (var item in methods)
            {
                var displayName = item.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                if (hiddenButtonsDisplayNames.Contains(displayName) == false)
                {
                    var parameters = item.GetParameters();
                    var flowItem = new BusinessFlowItemModel()
                    {
                        Title = displayName ?? item.Name,
                        Invokation = () => item.Invoke(business, parameters.Select(p => new object[] { _report, SelectedCredentials }.FirstOrDefault(_ => p.ParameterType.IsAssignableFrom(_.GetType()))).ToArray())
                    };
                    BusinessFlows.Add(flowItem);
                }
            }

            Height = 19.96 * (BusinessFlows.Count + 1) + 40 + 20;
        }

        public RelayCommand ClickFlowItem
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    var flowItem = obj as BusinessFlowItemModel;
                    OnTask(() =>
                    {
                        flowItem.Invokation.Invoke();
                        _report.WriteLine($"{flowItem.Title} done!");
                    });
                });
            }
        }
        
        private void OnTask(Action toDo)
        {
            Task.Run(() =>
            {
                toDo?.Invoke();
            }).ContinueWith((t) =>
            {
                if (t.Exception?.InnerException != null)
                {
                    _report.WriteLine(t.Exception.InnerException);
                }
            });

        }



        private string _reportOut;
        public string ReportOut
        {
            get
            {
                return _reportOut;
            }
            set
            {
                _reportOut = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReportOut)));
            }
        }
    }
}
