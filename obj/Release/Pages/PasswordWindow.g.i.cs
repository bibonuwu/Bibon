﻿#pragma checksum "..\..\..\Pages\PasswordWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "87CD9D409BCC8E482E9FC32DCA8A95504289E174911B6DAA5ECFED949BD2F6D6"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Bibon.Pages;
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


namespace Bibon.Pages {
    
    
    /// <summary>
    /// PasswordWindow
    /// </summary>
    public partial class PasswordWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 49 "..\..\..\Pages\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Pages\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxPassword;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\..\Pages\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image ToggleImage;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\Pages\PasswordWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
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
            System.Uri resourceLocater = new System.Uri("/BIBON;component/pages/passwordwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\PasswordWindow.xaml"
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
            
            #line 19 "..\..\..\Pages\PasswordWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnClose_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 55 "..\..\..\Pages\PasswordWindow.xaml"
            this.PasswordBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.PasswordBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.TextBoxPassword = ((System.Windows.Controls.TextBox)(target));
            
            #line 65 "..\..\..\Pages\PasswordWindow.xaml"
            this.TextBoxPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBoxPassword_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 71 "..\..\..\Pages\PasswordWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.TogglePasswordVisibility_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ToggleImage = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\Pages\PasswordWindow.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 106 "..\..\..\Pages\PasswordWindow.xaml"
            ((System.Windows.Controls.TextBlock)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.OpenLink);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
