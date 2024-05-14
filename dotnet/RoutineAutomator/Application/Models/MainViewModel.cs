using System;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using RPA.Report;
using System.IO;
using System.Collections.Generic;
using RPA;

namespace Application.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IReport _report => new Report(() => ReportOut, (text) => ReportOut = text);

        public event PropertyChangedEventHandler PropertyChanged;

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
            BusinessFlows = new ObservableCollection<BusinessFlowItemModel>(LoadPlugins());
            Height = 19.96 * (BusinessFlows.Count + 1) + 40 + 20;
        }

        private List<BusinessFlowItemModel> LoadPlugins()
        {
            string appDirectory = Directory.GetCurrentDirectory();
            IEnumerable<TypeInfo> pluginsTypes = Directory.EnumerateFiles(appDirectory)
                .Where(_ => Path.GetExtension(_) == ".dll")
                .Select(_ => Assembly.LoadFile(_))
                .SelectMany(_ => _.DefinedTypes)
                .Where(_ => _.IsClass)
                .Where(_ => typeof(IPlugin).IsAssignableFrom(_));
                

            List<BusinessFlowItemModel> businessFlows = new List<BusinessFlowItemModel>();

            foreach (TypeInfo pluginType in pluginsTypes)
            {
                object plugin = Activator.CreateInstance(pluginType, new object[] { _report });
                var methods = pluginType.GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(_ => _.ReturnType == typeof(void)).ToList();

                foreach (var method in methods)
                {
                    var displayName = method.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
                    var parameters = method.GetParameters();
                    var flowItem = new BusinessFlowItemModel()
                    {
                        Title = displayName ?? method.Name,
                        Invocation = () => method.Invoke(plugin,
                        parameters.Select(p => new object[]
                        {
                                _report
                        }
                        .FirstOrDefault(_ => p.ParameterType.IsAssignableFrom(_.GetType()))).ToArray())
                    };
                    businessFlows.Add(flowItem);
                }
            }

            return businessFlows;
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
                        flowItem.Invocation.Invoke();
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
