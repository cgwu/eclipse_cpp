#region Using

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace Assergs.Windows.Controls
{
    public class ToolWindowTabItemContainer: TabItem
    {
        #region Declare

        public static RoutedCommand TabCloseCommand;
        public static RoutedCommand TabRestoreCommand;

        public static readonly RoutedEvent TabRestoredEvent;
        public static readonly RoutedEvent TabClosedEvent; 

        #endregion
        
        #region Constructor

        #region Static

        static ToolWindowTabItemContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ToolWindowTabItemContainer),
                new FrameworkPropertyMetadata(typeof(ToolWindowTabItemContainer)));

            ToolWindowTabItemContainer.TabCloseCommand = new RoutedCommand("TabCloseCommand", typeof(ToolWindowTabItemContainer));
            ToolWindowTabItemContainer.TabRestoreCommand = new RoutedCommand("TabRestoreCommand", typeof(ToolWindowTabItemContainer));

            TabRestoredEvent = EventManager.RegisterRoutedEvent("TabRestored", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(ToolWindowTabItemContainer));

            TabClosedEvent = EventManager.RegisterRoutedEvent("TabClosed", RoutingStrategy.Bubble,
                typeof(RoutedEventHandler), typeof(ToolWindowTabItemContainer));
        } 

        #endregion

        #region Instance

        public ToolWindowTabItemContainer()
        {


            CommandBinding bindTabRestoreCommand = new CommandBinding(
                                                     ToolWindowTabItemContainer.TabRestoreCommand,
                                                     this.TabRestoreCommand_Executed);
            CommandBinding bindTabCloseCommand = new CommandBinding(
                                                     ToolWindowTabItemContainer.TabCloseCommand,
                                                     this.TabCloseCommand_Executed);

            this.CommandBindings.Add(bindTabRestoreCommand);
            this.CommandBindings.Add(bindTabCloseCommand);
        } 

        #endregion

        #endregion

        #region TabRestoredEvent

        public event RoutedEventHandler TabRestored
        {
            add { AddHandler(TabRestoredEvent, value); }
            remove { RemoveHandler(TabRestoredEvent, value); }
        }

        private void RaiseTabRestoredEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TabRestoredEvent, this);
            this.RaiseEvent(newEventArgs);
        } 

        #endregion

        #region TabClosedEvent

        public event RoutedEventHandler TabClosed
        {
            add { AddHandler(TabClosedEvent, value); }
            remove { RemoveHandler(TabClosedEvent, value); }
        }

        private void RaiseTabClosedEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(TabClosedEvent, this);
            this.RaiseEvent(newEventArgs);
        } 

        #endregion

        #region TabRestoreCommand_Executed

        private void TabRestoreCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RaiseTabRestoredEvent();
        } 

        #endregion

        #region TabCloseCommand_Executed

        private void TabCloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            this.RaiseTabClosedEvent();
        } 

        #endregion

        #region ImageSource

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ToolWindowTabItemContainer),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsArrange)); 

        #endregion
    }
}
