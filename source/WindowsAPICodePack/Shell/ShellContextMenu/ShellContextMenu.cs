using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.Interop.ShellContextMenu;

namespace Microsoft.WindowsAPICodePack.Controls.WindowsPresentationFoundation
{
    public class ShellContextMenu
    {
        #region Attch Porperties

        private static readonly DependencyProperty AsShellContextMenuOwnerProperty = DependencyProperty.RegisterAttached(
            "AsShellContextMenuOwner", typeof(bool), typeof(ShellContextMenu), new PropertyMetadata(false, AsShellContextMenuOwnerChanged));

        private static void SetAsShellContextMenuOwner(DependencyObject element, bool value)
        {
            element.SetValue(AsShellContextMenuOwnerProperty, value);
        }

        private static bool GetAsShellContextMenuOwner(DependencyObject element)
        {
            return (bool)element.GetValue(AsShellContextMenuOwnerProperty);
        }

        public static readonly DependencyProperty ShellItemsProperty = DependencyProperty.RegisterAttached(
            "ShellItems", typeof(object), typeof(ShellContextMenu), new PropertyMetadata(null, ShellItemsChanged));

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static void SetShellItems(DependencyObject element, object value)
        {
            element.SetValue(ShellItemsProperty, value);
        }

        [AttachedPropertyBrowsableForType(typeof(UIElement))]
        public static object GetShellItems(DependencyObject element)
        {
            return (object)element.GetValue(ShellItemsProperty);
        }

        private static void ShellItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!GetAsShellContextMenuOwner(d))
            {
                SetAsShellContextMenuOwner(d, true);
            }
        }

        private static void AsShellContextMenuOwnerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = (UIElement)d;

            if ((bool)e.NewValue)
                element.MouseRightButtonUp += OnMouseRightButtonUp;
            else
                element.MouseRightButtonUp -= OnMouseRightButtonUp;
        }

        private static void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!(sender is UIElement element)) return;

            var shellItem = GetShellItems(element);
            if (shellItem == null) return;
            new ShellContextMenu(ConvertToShellItemList(shellItem)).ShowIn(element);
        }

        private static IReadOnlyList<ShellObject> ConvertToShellItemList(object obj)
        {
            switch (obj)
            {
                case ShellObject item:
                    return new[] { item };
                case IEnumerable enumerable:
                    return enumerable.Cast<ShellObject>().ToArray();
                default:
                    throw new InvalidCastException($"The type of the {nameof(ShellObject)} property is invalid. ");
            }
        }

        #endregion

        public const int CmdFirst = 0x8000;
        public const int CmdLast = int.MaxValue;

        private IContextMenu _contextMenu;
        private ShellContextMenuMessageHandler _messageHandler;

        /// <summary>
        /// Initialises a new instance of the <see cref="ShellContextMenu"/> 
        /// class.
        /// </summary>
        /// 
        /// <param name="items">
        /// The items to which the context menu should refer.
        /// </param>
        public ShellContextMenu(IReadOnlyList<ShellObject> items)
        {
            Initialize(items);
        }

        public void ShowIn(UIElement element)
        {
            var point = element.PointToScreen(Mouse.GetPosition(element));

            var popupMenu = ShellContextMenuNativeMethods.CreatePopupMenu();
            _contextMenu.QueryContextMenu(popupMenu, 0, CmdFirst, CmdLast, CMF.EXPLORE);

            int command = ShellContextMenuNativeMethods.TrackPopupMenuEx(popupMenu, TPM.TPM_RETURNCMD, (int)point.X, (int)point.Y, _messageHandler.Handle, IntPtr.Zero);
            if (command > 0)
            {
                _messageHandler.InvokeCommand(command - CmdFirst);
            }
        }

        private void Initialize(IReadOnlyList<ShellObject> items)
        {
            var pidls = items.Select(item=> ShellContextMenuNativeMethods.ILFindLastID(item.PIDL)).ToArray();
            var parent = (ShellContainer)items.Select(item => item.Parent).DistinctByIEquatable().Single();

            parent.NativeShellFolder.GetUIObjectOf(IntPtr.Zero, (uint)pidls.Length, pidls, typeof(IContextMenu).GUID, 0, out var result);
            _contextMenu = (IContextMenu)Marshal.GetTypedObjectForIUnknown(result, typeof(IContextMenu));
            _messageHandler = new ShellContextMenuMessageHandler(_contextMenu);
        }
    }

    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> DistinctByIEquatable<T>(this IEnumerable<T> @this)
        {
            var list = new List<T>();
            foreach (var item in @this)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                    yield return item;
                }
            }
        }
    }
}
