using System;
using System.Runtime.InteropServices;
using System.Text;
using MS.WindowsAPICodePack.Internal;

#pragma warning disable 1591

namespace Microsoft.WindowsAPICodePack.Shell.Interop.ShellContextMenu
{
    internal static class ShellContextMenuNativeMethods
    {
        #region User32

        // Creates a popup-menu. The menu is initially empty, but it can be filled with 
        // menu items by using the InsertMenuItem, AppendMenu, and InsertMenu functions
        [DllImport("user32",
            SetLastError = true,
            CharSet = CharSet.Auto)]
        public static extern IntPtr CreatePopupMenu();

        [DllImport("user32.dll")]
        public static extern bool DeleteMenu(IntPtr hMenu, int uPosition,
            MF uFlags);

        [DllImport("user32.dll")]
        public static extern bool DestroyWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        public static extern IntPtr EnumChildWindows(IntPtr parentHandle,
            Win32Callback callback, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool GetMenuInfo(IntPtr hmenu,
            ref MENUINFO lpcmi);

        [DllImport("user32.dll")]
        public static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll")]
        public static extern bool GetMenuItemInfo(IntPtr hMenu, int uItem,
            bool fByPosition, ref MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        public static extern uint RegisterClipboardFormat(string lpszFormat);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, MSG Msg,
            int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, MSG Msg,
            int wParam, ref LVITEMA lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, MSG Msg,
            int wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, MSG Msg,
            int wParam, ref TVITEMW lParam);

        [DllImport("user32.dll")]
        public static extern bool SetMenuInfo(IntPtr hmenu, ref MENUINFO lpcmi);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, TPM fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        #endregion

        #region Shell32

        [DllImport("shell32.dll", EntryPoint = "#660")]
        public static extern bool FileIconInit(bool bFullInit);

        [DllImport("shell32.dll", EntryPoint = "#18")]
        public static extern IntPtr ILClone(IntPtr pidl);

        [DllImport("shell32.dll", EntryPoint = "#25")]
        public static extern IntPtr ILCombine(IntPtr pidl1, IntPtr pidl2);

        [DllImport("shell32.dll")]
        public static extern IntPtr ILCreateFromPath(string pszPath);

        [DllImport("shell32.dll", EntryPoint = "#16")]
        public static extern IntPtr ILFindLastID(IntPtr pidl);

        [DllImport("shell32.dll", EntryPoint = "#155")]
        public static extern void ILFree(IntPtr pidl);

        [DllImport("shell32.dll", EntryPoint = "#21")]
        public static extern bool ILIsEqual(IntPtr pidl1, IntPtr pidl2);

        [DllImport("shell32.dll", EntryPoint = "#23")]
        public static extern bool ILIsParent(IntPtr pidl1, IntPtr pidl2,
            bool fImmediate);

        [DllImport("shell32.dll", EntryPoint = "#17")]
        public static extern bool ILRemoveLastID(IntPtr pidl);

        [DllImport("shell32.dll", EntryPoint = "#71")]
        public static extern bool Shell_GetImageLists(out IntPtr lphimlLarge,
            out IntPtr lphimlSmall);

        [DllImport("shell32.dll", EntryPoint = "#2")]
        public static extern uint SHChangeNotifyRegister(IntPtr hWnd,
            SHCNRF fSources, SHCNE fEvents, uint wMsg, int cEntries,
            ref SHChangeNotifyEntry pFsne);

        [DllImport("shell32.dll", EntryPoint = "#4")]
        public static extern bool SHChangeNotifyUnregister(uint hNotify);

        [DllImport("shell32.dll", EntryPoint = "#165", CharSet = CharSet.Unicode)]
        public static extern ERROR SHCreateDirectory(IntPtr hwnd, string pszPath);

        [DllImport("shell32.dll", PreserveSig = false)]
        public static extern IShellItem SHCreateItemFromIDList(
            [In] IntPtr pidl,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern IShellItem SHCreateItemFromParsingName(
            [In] string pszPath,
            [In] IntPtr pbc,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        public static extern IShellItem SHCreateItemWithParent(
            [In] IntPtr pidlParent,
            [In] IShellFolder psfParent,
            [In] IntPtr pidl,
            [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        [DllImport("shell32.dll", PreserveSig = false)]
        public static extern IShellFolder SHGetDesktopFolder();

        [DllImport("shell32.dll")]
        public static extern Int32 SHGetDesktopFolder(out IntPtr ppshf);

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(IntPtr pszPath,
            int dwFileAttributes, out SHFILEINFO psfi, int cbFileInfo,
            SHGFI uFlags);

        [DllImport("shfolder.dll")]
        public static extern HResult SHGetFolderPath(
            [In] IntPtr hwndOwner,
            [In] CSIDL nFolder,
            [In] IntPtr hToken,
            [In] uint dwFlags,
            [Out] StringBuilder pszPath);

        [DllImport("shell32.dll")]
        public static extern HResult SHGetSpecialFolderLocation(IntPtr hwndOwner,
            CSIDL nFolder, out IntPtr ppidl);

        #endregion
    }


    [Flags]
    public enum LVIF
    {
        LVIF_TEXT = 0x0001,
        LVIF_IMAGE = 0x0002,
        LVIF_PARAM = 0x0004,
        LVIF_STATE = 0x0008,
        LVIF_INDENT = 0x0010,
        LVIF_GROUPID = 0x0100,
        LVIF_COLUMNS = 0x0200,
        LVIF_NORECOMPUTE = 0x0800,
        LVIF_DI_SETITEM = 0x1000,
        LVIF_COLFMT = 0x00010000,
    }

    [Flags]
    public enum LVIS
    {
        LVIS_FOCUSED = 0x0001,
        LVIS_SELECTED = 0x0002,
        LVIS_CUT = 0x0004,
        LVIS_DROPHILITED = 0x0008,
        LVIS_ACTIVATING = 0x0020,
        LVIS_OVERLAYMASK = 0x0F00,
        LVIS_STATEIMAGEMASK = 0xF000,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct LVITEMA
    {
        public LVIF mask;
        public int iItem;
        public int iSubItem;
        public LVIS state;
        public LVIS stateMask;
        [MarshalAs(UnmanagedType.LPTStr)]
        public string pszText;
        public int cchTextMax;
        public int iImage;
        public int lParam;
    }

    public enum LVSIL
    {
        LVSIL_NORMAL = 0,
        LVSIL_SMALL = 1,
        LVSIL_STATE = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MENUINFO
    {
        public int cbSize;
        public MIM fMask;
        public int dwStyle;
        public int cyMax;
        public IntPtr hbrBack;
        public int dwContextHelpID;
        public int dwMenuData;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MENUITEMINFO
    {
        public int cbSize;
        public MIIM fMask;
        public uint fType;
        public uint fState;
        public int wID;
        public IntPtr hSubMenu;
        public int hbmpChecked;
        public int hbmpUnchecked;
        public int dwItemData;
        public string dwTypeData;
        public uint cch;
        public int hbmpItem;
    }

    public enum MF
    {
        MF_BYCOMMAND = 0x00000000,
        MF_BYPOSITION = 0x00000400,
    }

    [Flags]
    public enum MIIM : uint
    {
        MIIM_STATE = 0x00000001,
        MIIM_ID = 0x00000002,
        MIIM_SUBMENU = 0x00000004,
        MIIM_CHECKMARKS = 0x00000008,
        MIIM_TYPE = 0x00000010,
        MIIM_DATA = 0x00000020,
        MIIM_STRING = 0x00000040,
        MIIM_BITMAP = 0x00000080,
        MIIM_FTYPE = 0x00000100,
    }

    public enum MIM : uint
    {
        MIM_MAXHEIGHT = 0x00000001,
        MIM_BACKGROUND = 0x00000002,
        MIM_HELPID = 0x00000004,
        MIM_MENUDATA = 0x00000008,
        MIM_STYLE = 0x00000010,
        MIM_APPLYTOSUBMENUS = 0x80000000,
    }

    public enum MK
    {
        MK_LBUTTON = 0x0001,
        MK_RBUTTON = 0x0002,
        MK_SHIFT = 0x0004,
        MK_CONTROL = 0x0008,
        MK_MBUTTON = 0x0010,
        MK_ALT = 0x1000,
    }

    public enum MSG
    {
        WM_COMMAND = 0x0111,
        WM_VSCROLL = 0x0115,
        LVM_SETIMAGELIST = 0x1003,
        LVM_GETITEMCOUNT = 0x1004,
        LVM_GETITEMA = 0x1005,
        LVM_EDITLABEL = 0x1017,
        TVM_SETIMAGELIST = 4361,
        TVM_SETITEMW = 4415,
    }

    [Flags]
    public enum TPM
    {
        TPM_LEFTBUTTON = 0x0000,
        TPM_RIGHTBUTTON = 0x0002,
        TPM_LEFTALIGN = 0x0000,
        TPM_CENTERALIGN = 0x000,
        TPM_RIGHTALIGN = 0x000,
        TPM_TOPALIGN = 0x0000,
        TPM_VCENTERALIGN = 0x0010,
        TPM_BOTTOMALIGN = 0x0020,
        TPM_HORIZONTAL = 0x0000,
        TPM_VERTICAL = 0x0040,
        TPM_NONOTIFY = 0x0080,
        TPM_RETURNCMD = 0x0100,
        TPM_RECURSE = 0x0001,
        TPM_HORPOSANIMATION = 0x0400,
        TPM_HORNEGANIMATION = 0x0800,
        TPM_VERPOSANIMATION = 0x1000,
        TPM_VERNEGANIMATION = 0x2000,
        TPM_NOANIMATION = 0x4000,
        TPM_LAYOUTRTL = 0x8000,
    }

    [Flags]
    public enum TVIF
    {
        TVIF_TEXT = 0x0001,
        TVIF_IMAGE = 0x0002,
        TVIF_PARAM = 0x0004,
        TVIF_STATE = 0x0008,
        TVIF_HANDLE = 0x0010,
        TVIF_SELECTEDIMAGE = 0x0020,
        TVIF_CHILDREN = 0x0040,
        TVIF_INTEGRAL = 0x0080,
    }

    [Flags]
    public enum TVIS
    {
        TVIS_SELECTED = 0x0002,
        TVIS_CUT = 0x0004,
        TVIS_DROPHILITED = 0x0008,
        TVIS_BOLD = 0x0010,
        TVIS_EXPANDED = 0x0020,
        TVIS_EXPANDEDONCE = 0x0040,
        TVIS_EXPANDPARTIAL = 0x0080,
        TVIS_OVERLAYMASK = 0x0F00,
        TVIS_STATEIMAGEMASK = 0xF000,
        TVIS_USERMASK = 0xF000,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct TVITEMW
    {
        public TVIF mask;
        public IntPtr hItem;
        public TVIS state;
        public TVIS stateMask;
        public string pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public int lParam;
    }


    public enum CSIDL
    {
        DESKTOP = 0x0000,
        INTERNET = 0x0001,
        PROGRAMS = 0x0002,
        CONTROLS = 0x0003,
        PRINTERS = 0x0004,
        PERSONAL = 0x0005,
        FAVORITES = 0x0006,
        STARTUP = 0x0007,
        RECENT = 0x0008,
        SENDTO = 0x0009,
        BITBUCKET = 0x000a,
        STARTMENU = 0x000b,
        MYDOCUMENTS = PERSONAL,
        MYMUSIC = 0x000d,
        MYVIDEO = 0x000e,
        DESKTOPDIRECTORY = 0x0010,
        DRIVES = 0x0011,
        NETWORK = 0x0012,
        NETHOOD = 0x0013,
        FONTS = 0x0014,
        TEMPLATES = 0x0015,
        COMMON_STARTMENU = 0x0016,
        COMMON_PROGRAMS = 0x0017,
        COMMON_STARTUP = 0x0018,
        COMMON_DESKTOPDIRECTORY = 0x0019,
        APPDATA = 0x001a,
        PRINTHOOD = 0x001b,
        LOCAL_APPDATA = 0x001c,
        ALTSTARTUP = 0x001d,
        COMMON_ALTSTARTUP = 0x001e,
        COMMON_FAVORITES = 0x001f,
        INTERNET_CACHE = 0x0020,
        COOKIES = 0x0021,
        HISTORY = 0x0022,
        COMMON_APPDATA = 0x0023,
        WINDOWS = 0x0024,
        SYSTEM = 0x0025,
        PROGRAM_FILES = 0x0026,
        MYPICTURES = 0x0027,
        PROFILE = 0x0028,
        SYSTEMX86 = 0x0029,
        PROGRAM_FILESX86 = 0x002a,
        PROGRAM_FILES_COMMON = 0x002b,
        PROGRAM_FILES_COMMONX86 = 0x002c,
        COMMON_TEMPLATES = 0x002d,
        COMMON_DOCUMENTS = 0x002e,
        COMMON_ADMINTOOLS = 0x002f,
        ADMINTOOLS = 0x0030,
        CONNECTIONS = 0x0031,
        COMMON_MUSIC = 0x0035,
        COMMON_PICTURES = 0x0036,
        COMMON_VIDEO = 0x0037,
        RESOURCES = 0x0038,
        RESOURCES_LOCALIZED = 0x0039,
        COMMON_OEM_LINKS = 0x003a,
        CDBURN_AREA = 0x003b,
        COMPUTERSNEARME = 0x003d,
    }

    public enum ERROR
    {
        SUCCESS,
        FILE_EXISTS = 80,
        BAD_PATHNAME = 161,
        ALREADY_EXISTS = 183,
        FILENAME_EXCED_RANGE = 206,
        CANCELLED = 1223,
    }

    public enum FFFP_MODE
    {
        EXACTMATCH,
        NEARESTPARENTMATCH,
    }

    [Flags]
    public enum FOLDERFLAGS : uint
    {
        AUTOARRANGE = 0x1,
        ABBREVIATEDNAMES = 0x2,
        SNAPTOGRID = 0x4,
        OWNERDATA = 0x8,
        BESTFITWINDOW = 0x10,
        DESKTOP = 0x20,
        SINGLESEL = 0x40,
        NOSUBFOLDERS = 0x80,
        TRANSPARENT = 0x100,
        NOCLIENTEDGE = 0x200,
        NOSCROLL = 0x400,
        ALIGNLEFT = 0x800,
        NOICONS = 0x1000,
        SHOWSELALWAYS = 0x2000,
        NOVISIBLE = 0x4000,
        SINGLECLICKACTIVATE = 0x8000,
        NOWEBVIEW = 0x10000,
        HIDEFILENAMES = 0x20000,
        CHECKSELECT = 0x40000
    }

    public enum FOLDERVIEWMODE : uint
    {
        FIRST = 1,
        ICON = 1,
        SMALLICON = 2,
        LIST = 3,
        DETAILS = 4,
        THUMBNAIL = 5,
        TILE = 6,
        THUMBSTRIP = 7,
        LAST = 7
    }

    [Flags]
    public enum SHCONTF
    {
        FOLDERS = 0x0020,
        NONFOLDERS = 0x0040,
        INCLUDEHIDDEN = 0x0080,
        INIT_ON_FIRST_NEXT = 0x0100,
        NETPRINTERSRCH = 0x0200,
        SHAREABLE = 0x0400,
        STORAGE = 0x0800
    }

    [Flags]
    public enum SFGAO : uint
    {
        CANCOPY = 0x00000001,
        CANMOVE = 0x00000002,
        CANLINK = 0x00000004,
        STORAGE = 0x00000008,
        CANRENAME = 0x00000010,
        CANDELETE = 0x00000020,
        HASPROPSHEET = 0x00000040,
        DROPTARGET = 0x00000100,
        CAPABILITYMASK = 0x00000177,
        ENCRYPTED = 0x00002000,
        ISSLOW = 0x00004000,
        GHOSTED = 0x00008000,
        LINK = 0x00010000,
        SHARE = 0x00020000,
        READONLY = 0x00040000,
        HIDDEN = 0x00080000,
        DISPLAYATTRMASK = 0x000FC000,
        STREAM = 0x00400000,
        STORAGEANCESTOR = 0x00800000,
        VALIDATE = 0x01000000,
        REMOVABLE = 0x02000000,
        COMPRESSED = 0x04000000,
        BROWSABLE = 0x08000000,
        FILESYSANCESTOR = 0x10000000,
        FOLDER = 0x20000000,
        FILESYSTEM = 0x40000000,
        HASSUBFOLDER = 0x80000000,
        CONTENTSMASK = 0x80000000,
        STORAGECAPMASK = 0x70C50008,
    }

    [Flags]
    public enum SHCIDS : uint
    {
        ALLFIELDS = 0x80000000,
        CANONICALONLY = 0x10000000,
        BITMASK = 0xFFFF0000,
        COLUMNMASK = 0x0000FFFF,
    }

    public enum SHCNE : uint
    {
        RENAMEITEM = 0x00000001,
        CREATE = 0x00000002,
        DELETE = 0x00000004,
        MKDIR = 0x00000008,
        RMDIR = 0x00000010,
        MEDIAINSERTED = 0x00000020,
        MEDIAREMOVED = 0x00000040,
        DRIVEREMOVED = 0x00000080,
        DRIVEADD = 0x00000100,
        NETSHARE = 0x00000200,
        NETUNSHARE = 0x00000400,
        ATTRIBUTES = 0x00000800,
        UPDATEDIR = 0x00001000,
        UPDATEITEM = 0x00002000,
        SERVERDISCONNECT = 0x00004000,
        UPDATEIMAGE = 0x00008000,
        DRIVEADDGUI = 0x00010000,
        RENAMEFOLDER = 0x00020000,
        FREESPACE = 0x00040000,
        EXTENDED_EVENT = 0x04000000,
        ASSOCCHANGED = 0x08000000,
        DISKEVENTS = 0x0002381F,
        GLOBALEVENTS = 0x0C0581E0,
        ALLEVENTS = 0x7FFFFFFF,
        INTERRUPT = 0x80000000,
    }

    [Flags]
    public enum SHCNRF
    {
        InterruptLevel = 0x0001,
        ShellLevel = 0x0002,
        RecursiveInterrupt = 0x1000,
        NewDelivery = 0x8000,
    }

    [Flags]
    public enum SHGFI
    {
        ICON = 0x000000100,
        DISPLAYNAME = 0x000000200,
        TYPENAME = 0x000000400,
        ATTRIBUTES = 0x000000800,
        ICONLOCATION = 0x000001000,
        EXETYPE = 0x000002000,
        SYSICONINDEX = 0x000004000,
        LINKOVERLAY = 0x000008000,
        SELECTED = 0x000010000,
        ATTR_SPECIFIED = 0x000020000,
        LARGEICON = 0x000000000,
        SMALLICON = 0x000000001,
        OPENICON = 0x000000002,
        SHELLICONSIZE = 0x000000004,
        PIDL = 0x000000008,
        USEFILEATTRIBUTES = 0x000000010,
        ADDOVERLAYS = 0x000000020,
        OVERLAYINDEX = 0x000000040
    }

    public enum SHGNO
    {
        NORMAL = 0x0000,
        INFOLDER = 0x0001,
        FOREDITING = 0x1000,
        FORADDRESSBAR = 0x4000,
        FORPARSING = 0x8000,
    }

    public enum SICHINT : uint
    {
        DISPLAY = 0x00000000,
        CANONICAL = 0x10000000,
        ALLFIELDS = 0x80000000
    }

    public enum SIGDN : uint
    {
        NORMALDISPLAY = 0,
        PARENTRELATIVEPARSING = 0x80018001,
        PARENTRELATIVEFORADDRESSBAR = 0x8001c001,
        DESKTOPABSOLUTEPARSING = 0x80028000,
        PARENTRELATIVEEDITING = 0x80031001,
        DESKTOPABSOLUTEEDITING = 0x8004c000,
        FILESYSPATH = 0x80058000,
        URL = 0x80068000
    }

    public enum SVSI : uint
    {
        SVSI_DESELECT = 0x00000000,
        SVSI_SELECT = 0x00000001,
    }

    public struct FOLDERSETTINGS
    {
        public FOLDERVIEWMODE ViewMode;
        public FOLDERFLAGS fFlags;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct KNOWNFOLDER_DEFINITION
    {
        public int category;
        public IntPtr pszName;
        public IntPtr pszDescription;
        public Guid fidParent;
        public IntPtr pszRelativePath;
        public IntPtr pszParsingName;
        public IntPtr pszTooltip;
        public IntPtr pszLocalizedName;
        public IntPtr pszIcon;
        public IntPtr pszSecurity;
        public uint dwAttributes;
        public int kfdFlags;
        public Guid ftidType;
    }

    public struct SHChangeNotifyEntry
    {
        public IntPtr pidl;
        public bool fRecursive;
    }

    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    public struct SHNOTIFYSTRUCT
    {
        public IntPtr dwItem1;
        public IntPtr dwItem2;
    }

    [StructLayout(LayoutKind.Explicit, Size = 264)]
    public struct STRRET
    {
        [FieldOffset(0)]
        public UInt32 uType;
        [FieldOffset(4)]
        public IntPtr pOleStr;
        [FieldOffset(4)]
        public IntPtr pStr;
        [FieldOffset(4)]
        public UInt32 uOffset;
        [FieldOffset(4)]
        public IntPtr cStr;
    }
}
