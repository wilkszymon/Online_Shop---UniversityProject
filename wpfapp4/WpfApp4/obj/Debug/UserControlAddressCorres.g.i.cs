﻿#pragma checksum "..\..\UserControlAddressCorres.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "AC775EEF27BE1E49C53CFFD9DC8ED1E2F900A782906B28DA220836805CFEFBE8"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WpfApp4;


namespace WpfApp4 {
    
    
    /// <summary>
    /// UserControlAddressCorres
    /// </summary>
    public partial class UserControlAddressCorres : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CityCoress;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox StreetCoress;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ApartmentNumberCoress;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HouseNumberCoress;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ZIPCodeCoress;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeAddressCorres;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\UserControlAddressCorres.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelRequired;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp4;component/usercontroladdresscorres.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\UserControlAddressCorres.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\UserControlAddressCorres.xaml"
            ((WpfApp4.UserControlAddressCorres)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.UserControl_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.CityCoress = ((System.Windows.Controls.TextBox)(target));
            
            #line 13 "..\..\UserControlAddressCorres.xaml"
            this.CityCoress.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 3:
            this.StreetCoress = ((System.Windows.Controls.TextBox)(target));
            
            #line 16 "..\..\UserControlAddressCorres.xaml"
            this.StreetCoress.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ApartmentNumberCoress = ((System.Windows.Controls.TextBox)(target));
            
            #line 19 "..\..\UserControlAddressCorres.xaml"
            this.ApartmentNumberCoress.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.HouseAndApartmentNumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 5:
            this.HouseNumberCoress = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\UserControlAddressCorres.xaml"
            this.HouseNumberCoress.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.HouseAndApartmentNumberValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ZIPCodeCoress = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\UserControlAddressCorres.xaml"
            this.ZIPCodeCoress.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ZipCodeValidationTextBox);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ChangeAddressCorres = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\UserControlAddressCorres.xaml"
            this.ChangeAddressCorres.Click += new System.Windows.RoutedEventHandler(this.ChangeAddressCorres_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.LabelRequired = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

