﻿using System;
using System.Windows;
using System.Windows.Interactivity;

namespace LivetMessageSample.Views.Behaviors
{
    class DropWindowActiveBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewDragEnter += ActivateParentWindow;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.PreviewDragEnter -= ActivateParentWindow;
        }

        // WindowがActiveでないとLivetのMessengerが表示されないので有効化する
        private static void ActivateParentWindow(object sender, EventArgs e)
        {
            if (sender is DependencyObject d)
            {
                var window = UIHelper.TryFindParent<Window>(d);
                window?.Activate();
            }
        }

    }
}
