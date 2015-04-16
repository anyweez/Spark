﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

using Spark.Dialogs;
using Spark.Input;

namespace Spark.ViewModels
{
    public enum DialogButtons
    {
        OK = 0,
        OKCancel = 1,
        YesNo = 2
    }

    public class DialogViewModel : WorkspaceViewModel
    {
        string title;
        string message;
        string messageHint;
        string positiveButtonTitle;
        string negativeButtonTitle;
        bool isPositiveButtonVisible;
        bool isNegativeButtonVisible;

        ICommand yesCommand;
        ICommand noCommand;

        public event EventHandler PositiveButtonClicked;
        public event EventHandler NegativeButtonClicked;

        #region Properties
        public virtual string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        public virtual string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public virtual string MessageHint
        {
            get { return messageHint; }
            set { SetProperty(ref messageHint, value); }
        }

        public virtual string PositiveButtonTitle
        {
            get { return positiveButtonTitle; }
            set { SetProperty(ref positiveButtonTitle, value); }
        }

        public virtual string NegativeButtonTitle
        {
            get { return negativeButtonTitle; }
            set { SetProperty(ref negativeButtonTitle, value); }
        }

        public virtual bool IsYesButtonVisible
        {
            get { return isPositiveButtonVisible; }
            set { SetProperty(ref isPositiveButtonVisible, value); }
        }

        public virtual bool IsNoButtonVisible
        {
            get { return isNegativeButtonVisible; }
            set { SetProperty(ref isNegativeButtonVisible, value); }
        }

        public ICommand YesCommand
        {
            get
            {
                // Lazy-initialized
                if (yesCommand == null)
                    yesCommand = new DelegateCommand(x => OnYesClicked());

                return yesCommand;
            }
        }

        public ICommand NoCommand
        {
            get
            {
                // Lazy-initialized
                if (noCommand == null)
                    noCommand = new DelegateCommand(x => OnNoClicked());

                return noCommand;
            }
        }
        #endregion

        public DialogViewModel(string title, string message, string messageHint = null, DialogButtons buttons = DialogButtons.OK, IDialogService dialogService = null)
            : base(title, dialogService)
        {
            this.Title = title;
            this.Message = message;
            this.MessageHint = messageHint;

            if (buttons == DialogButtons.OK || buttons == DialogButtons.OKCancel)
            {
                this.PositiveButtonTitle = "_OK";
                this.NegativeButtonTitle = "_Cancel";
                this.IsYesButtonVisible = true;
                this.IsNoButtonVisible = (buttons == DialogButtons.OKCancel);
            }
            else if (buttons == DialogButtons.YesNo)
            {
                this.PositiveButtonTitle = "_Yes";
                this.NegativeButtonTitle = "_No";
                this.IsYesButtonVisible = true;
                this.IsNoButtonVisible = true;
            }
        }

        protected virtual void OnYesClicked()
        {
            var handler = this.PositiveButtonClicked;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnNoClicked()
        {
            var handler = this.NegativeButtonClicked;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}