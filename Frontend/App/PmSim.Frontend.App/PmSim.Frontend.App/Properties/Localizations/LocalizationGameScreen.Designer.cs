﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PmSim.Frontend.App.Properties.Localizations {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class LocalizationGameScreen {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizationGameScreen() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PmSim.Frontend.App.Properties.Localizations.LocalizationGameScreen", typeof(LocalizationGameScreen).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Сдаться.
        /// </summary>
        public static string ButtonGiveUp {
            get {
                return ResourceManager.GetString("ButtonGiveUp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Пропустить.
        /// </summary>
        public static string ButtonSkip {
            get {
                return ResourceManager.GetString("ButtonSkip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Недостаточно денег!.
        /// </summary>
        public static string NotEnoughMoney {
            get {
                return ResourceManager.GetString("NotEnoughMoney", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Офис занят другим игроком!.
        /// </summary>
        public static string OfficeIsOccupiedByAnother {
            get {
                return ResourceManager.GetString("OfficeIsOccupiedByAnother", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Этот офис незанят.
        ///            Стоимость аренды: {0}
        ///            Вместимость: {1}
        ///            
        ///            Арендовать?
        ///        .
        /// </summary>
        public static string OfficeRentInfo {
            get {
                return ResourceManager.GetString("OfficeRentInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Игроки.
        /// </summary>
        public static string Players {
            get {
                return ResourceManager.GetString("Players", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Выберите предысторию.
        /// </summary>
        public static string TextBlockChooseBackground {
            get {
                return ResourceManager.GetString("TextBlockChooseBackground", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Аренда офиса.
        /// </summary>
        public static string TextBlockOfficeRent {
            get {
                return ResourceManager.GetString("TextBlockOfficeRent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Подождите, пока остальные игроки подключатся..
        /// </summary>
        public static string TextBlockWaitConnection {
            get {
                return ResourceManager.GetString("TextBlockWaitConnection", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Осталось действий.
        /// </summary>
        public static string ToolTipActionsNumber {
            get {
                return ResourceManager.GetString("ToolTipActionsNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кол-во ваших сотрудников.
        /// </summary>
        public static string ToolTipEmployeesNumber {
            get {
                return ResourceManager.GetString("ToolTipEmployeesNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Стадия игры.
        /// </summary>
        public static string ToolTipGameStage {
            get {
                return ResourceManager.GetString("ToolTipGameStage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Деньги.
        /// </summary>
        public static string ToolTipMoney {
            get {
                return ResourceManager.GetString("ToolTipMoney", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кол-во ваших офисов.
        /// </summary>
        public static string ToolTipOfficesNumber {
            get {
                return ResourceManager.GetString("ToolTipOfficesNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Кол-во ваших проектов.
        /// </summary>
        public static string ToolTipProjectsNumber {
            get {
                return ResourceManager.GetString("ToolTipProjectsNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Время до конца стадии.
        /// </summary>
        public static string ToolTipTime {
            get {
                return ResourceManager.GetString("ToolTipTime", resourceCulture);
            }
        }
    }
}
