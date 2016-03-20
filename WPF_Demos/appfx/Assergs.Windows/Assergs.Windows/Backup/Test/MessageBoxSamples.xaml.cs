#region Using

using System;
using Assergs.Windows;
using System.Windows;
using System.Windows.Controls; 

#endregion

namespace Assergs.Windows.Tests
{
	public partial class MessageBoxSamples : ToolWindow
	{
        #region Constructor

        public MessageBoxSamples()
        {
            InitializeComponent();
                        
            #region Fill ComboBoxes

            this.MessageBoxButtonComboBox.Items.Add(MessageBoxButton.OK.ToString());
            this.MessageBoxButtonComboBox.Items.Add(MessageBoxButton.OKCancel.ToString());
            this.MessageBoxButtonComboBox.Items.Add(MessageBoxButton.YesNo.ToString());
            this.MessageBoxButtonComboBox.Items.Add(MessageBoxButton.YesNoCancel.ToString());
            this.MessageBoxButtonComboBox.SelectedIndex = 0;

            ComboBoxItem[] items = new ComboBoxItem[9];
            items[0] = new ComboBoxItem();
            items[0].Content = "Asterisk";
            items[0].Tag = MessageBoxImage.Asterisk;

            items[1] = new ComboBoxItem();
            items[1].Content = "Error";
            items[1].Tag = MessageBoxImage.Error;

            items[2] = new ComboBoxItem();
            items[2].Content = "Exclamation";
            items[2].Tag = MessageBoxImage.Exclamation;

            items[3] = new ComboBoxItem();
            items[3].Content = "Hand";
            items[3].Tag = MessageBoxImage.Hand;

            items[4] = new ComboBoxItem();
            items[4].Content = "Information";
            items[4].Tag = MessageBoxImage.Information;

            items[5] = new ComboBoxItem();
            items[5].Content = "None";
            items[5].Tag = MessageBoxImage.None;

            items[6] = new ComboBoxItem();
            items[6].Content = "Question";
            items[6].Tag = MessageBoxImage.Question;

            items[7] = new ComboBoxItem();
            items[7].Content = "Stop";
            items[7].Tag = MessageBoxImage.Stop;

            items[8] = new ComboBoxItem();
            items[8].Content = "Warning";
            items[8].Tag = MessageBoxImage.Warning;

            foreach (ComboBoxItem item in items)
                this.MessageBoxImageComboBox.Items.Add(item);

            this.MessageBoxImageComboBox.SelectedIndex = 0;
            
            this.MessageBoxResultComboBox.Items.Add(MessageBoxResult.Cancel.ToString());
            this.MessageBoxResultComboBox.Items.Add(MessageBoxResult.No.ToString());
            this.MessageBoxResultComboBox.Items.Add(MessageBoxResult.None.ToString());
            this.MessageBoxResultComboBox.Items.Add(MessageBoxResult.OK.ToString());
            this.MessageBoxResultComboBox.Items.Add(MessageBoxResult.Yes.ToString());
            this.MessageBoxResultComboBox.SelectedIndex = 0;

            #endregion

            CaptionTextBox.Text = "Caption";
            MessageBoxText.Text = "Message Text goes here!";

            this.Loaded += new RoutedEventHandler(MessageBoxSamples_Loaded);
        }

        #endregion

        #region MessageBoxSamples_Loaded

        void MessageBoxSamples_Loaded(object sender, RoutedEventArgs e)
        {
            CaptionTextBox.SelectionLength = CaptionTextBox.Text.Length;
            CaptionTextBox.Focus();
        }

        #endregion

        #region ShowButton_Click

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            #region Define messageBoxButton

            MessageBoxButton messageBoxButton;
            if(MessageBoxButtonComboBox.Text == MessageBoxButton.OK.ToString())
            {
                messageBoxButton = MessageBoxButton.OK;
            }
            else if(MessageBoxButtonComboBox.Text == MessageBoxButton.OKCancel.ToString())
            {
                messageBoxButton = MessageBoxButton.OKCancel;
            }
            else if(MessageBoxButtonComboBox.Text == MessageBoxButton.YesNo.ToString())
            {
                messageBoxButton = MessageBoxButton.YesNo;
            }
            else if(MessageBoxButtonComboBox.Text == MessageBoxButton.YesNoCancel.ToString())
            {
                messageBoxButton = MessageBoxButton.YesNoCancel;
            }
            else
            {
                messageBoxButton = MessageBoxButton.OK;
            }
                
            #endregion

            #region Define messageBoxImage

            MessageBoxImage messageBoxImage = (MessageBoxImage)((ComboBoxItem)MessageBoxImageComboBox.SelectedItem).Tag;
            //if (MessageBoxImageComboBox.Text == MessageBoxImage.Asterisk.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Asterisk;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Error.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Error;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Exclamation.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Exclamation;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Hand.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Hand;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Information.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Information;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.None.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.None;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Question.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Question;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Stop.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Stop;
            //}
            //else if (MessageBoxImageComboBox.Text == MessageBoxImage.Warning.ToString())
            //{
            //    messageBoxImage = MessageBoxImage.Warning;
            //}
            //else
            //{
            //    messageBoxImage = MessageBoxImage.None;
            //}

            #endregion

            #region Define messageBoxResult

            MessageBoxResult messageBoxResult;
            if (MessageBoxResultComboBox.Text == MessageBoxResult.Cancel.ToString())
            {
                messageBoxResult = MessageBoxResult.Cancel;
            }
            else if (MessageBoxResultComboBox.Text == MessageBoxResult.No.ToString())
            { 
                messageBoxResult = MessageBoxResult.No;
            }
            else if (MessageBoxResultComboBox.Text == MessageBoxResult.None.ToString())
            { 
                messageBoxResult = MessageBoxResult.None;
            }
            else if (MessageBoxResultComboBox.Text == MessageBoxResult.OK.ToString())
            { 
                messageBoxResult = MessageBoxResult.OK;
            }
            else if (MessageBoxResultComboBox.Text == MessageBoxResult.Yes.ToString())
            { 
                messageBoxResult = MessageBoxResult.Yes;
            }
            else
            { 
                messageBoxResult = MessageBoxResult.None;
            }

            #endregion

            MessageBox.Show(MessageBoxText.Text, CaptionTextBox.Text,
                messageBoxButton,
                messageBoxImage,
                messageBoxResult,
                new MessageBox.DialogResultEventHandler(this.DialogResult));
        }

        #endregion

        #region DialogResult

        private void DialogResult(MessageBox.MessageBoxResultEventArgs e)
        {
            this.LeftStatusContent = String.Format("Result of message: {0}", e.MessageResult.ToString());
        }

        #endregion
	}
}