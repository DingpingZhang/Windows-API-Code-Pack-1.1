using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Microsoft.WindowsAPICodePack.Shell.Interop.ShellContextMenu;
using MS.WindowsAPICodePack.Internal;
using MSG = Microsoft.WindowsAPICodePack.Shell.Interop.ShellContextMenu.MSG;

namespace Microsoft.WindowsAPICodePack.Controls.WindowsPresentationFoundation
{
    internal sealed class ShellContextMenuMessageHandler : IDisposable
    {
        private HwndSource _hwndSource;
        private IContextMenu _contextMenu;
        private IContextMenu2 _contextMenu2;
        private IContextMenu3 _contextMenu3;

        public IntPtr Handle => _hwndSource.Handle;

        public ShellContextMenuMessageHandler(IContextMenu contextMenu)
        {
            _contextMenu = contextMenu;
            _contextMenu2 = contextMenu as IContextMenu2;
            _contextMenu3 = contextMenu as IContextMenu3;

            _hwndSource = new HwndSource(0, 0, 0, 0, 0, 0, 0, null, IntPtr.Zero);
            _hwndSource.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam, ref bool handled)
        {
            if (msg == (int)MSG.WM_COMMAND && (int)wparam >= ShellContextMenu.CmdFirst)
            {
                InvokeCommand((int)wparam - ShellContextMenu.CmdFirst);
                return IntPtr.Zero;
            }

            if (_contextMenu3 != null)
            {
                if (_contextMenu3.HandleMenuMsg2(msg, wparam, lparam, out var result) == HResult.Ok)
                {
                    return result;
                }
            }
            else if (_contextMenu2 != null)
            {
                if (_contextMenu2.HandleMenuMsg(msg, wparam, lparam) == HResult.Ok)
                {
                    return IntPtr.Zero;
                }
            }
            return IntPtr.Zero;
        }

        public void InvokeCommand(int index)
        {
            const int SW_SHOWNORMAL = 1;
            var invoke = new CMINVOKECOMMANDINFO_ByIndex
            {
                iVerb = index,
                nShow = SW_SHOWNORMAL
            };
            invoke.cbSize = Marshal.SizeOf(invoke);
            _contextMenu2.InvokeCommand(ref invoke);
        }

        /// <summary>
        /// Invokes the Rename command on the shell item.
        /// </summary>
        public void InvokeRename()
        {
            CMINVOKECOMMANDINFO invoke = new CMINVOKECOMMANDINFO();
            invoke.cbSize = Marshal.SizeOf(invoke);
            invoke.lpVerb = "rename";
            _contextMenu.InvokeCommand(ref invoke);
        }


        public void Dispose()
        {
            _hwndSource.RemoveHook(WndProc);
            _hwndSource?.Dispose();
            _hwndSource = null;

            _contextMenu = null;
            _contextMenu2 = null;
            _contextMenu3 = null;
        }
    }
}
