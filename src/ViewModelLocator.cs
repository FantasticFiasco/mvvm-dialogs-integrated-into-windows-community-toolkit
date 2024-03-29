﻿using CommunityToolkit.Mvvm.DependencyInjection;

namespace Todos
{
    /// <summary>
    /// This class contains references to all the view models in the application and provides an
    /// entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindow => Ioc.Default.GetRequiredService<MainWindowViewModel>();
    }
}
