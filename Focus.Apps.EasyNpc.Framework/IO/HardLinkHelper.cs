using System.Runtime.InteropServices;

namespace Focus.Apps.EasyNpc.IO;

// Source: https://stackoverflow.com/a/75348701/38360
internal static partial class HardLinkHelper
{
    #region WinAPI P/Invoke declarations
    [LibraryImport(
        "kernel32.dll",
        SetLastError = true,
        StringMarshalling = StringMarshalling.Utf16
    )]
    public static partial IntPtr FindFirstFileNameW(
        string lpFileName,
        uint dwFlags,
        ref uint StringLength,
        Span<char> LinkName
    );

    [LibraryImport(
        "kernel32.dll",
        SetLastError = true,
        StringMarshalling = StringMarshalling.Utf16
    )]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool FindNextFileNameW(
        IntPtr hFindStream,
        ref uint StringLength,
        Span<char> LinkName
    );

    [LibraryImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool FindClose(IntPtr hFindFile);

    [LibraryImport(
        "kernel32.dll",
        SetLastError = true,
        StringMarshalling = StringMarshalling.Utf16
    )]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool GetVolumePathNameW(
        string lpszFileName,
        Span<char> lpszVolumePathName,
        uint cchBufferLength
    );

    private static readonly IntPtr INVALID_HANDLE_VALUE = -1; // 0xffffffff;
    private const int MAX_PATH = 65535; // Max. NTFS path length.
    #endregion

    /// <summary>
    /// Checks for hard links on a Windows NTFS drive associated with the given path.
    /// </summary>
    /// <param name="filepath">Fully qualified path of the file to check for shared hard links</param>
    /// <param name="returnEmptyListIfOnlyOne">Set true, to return populated list only for files having multiple hard links</param>
    /// <returns>
    ///     Empty list is returned for non-existing path or unsupported path.
    ///     Single hard link paths returns empty list if ReturnEmptyListIfOnlyOne is true. If false, returns single item list.
    ///     For multiple shared hard links, returns list of all the shared hard links.
    /// </returns>
    public static List<string> GetHardLinks(string filepath, bool returnEmptyListIfOnlyOne = false)
    {
        var links = new List<string>();
        try
        {
            char[] sbPath = new char[MAX_PATH + 1];
            uint charCount = MAX_PATH;
            GetVolumePathNameW(filepath, sbPath, MAX_PATH); // Must use GetVolumePathName, because Path.GetPathRoot fails on a mounted drive on an empty folder.
            string volume = new string(sbPath).Trim('\0');
            volume = volume[..^1];
            Array.Clear(sbPath, 0, MAX_PATH); // Reset the array because these API's can leave garbage at the end of the buffer.
            IntPtr findHandle;
            if (
                INVALID_HANDLE_VALUE
                != (findHandle = FindFirstFileNameW(filepath, 0, ref charCount, sbPath))
            )
            {
                do
                {
                    links.Add((volume + new string(sbPath)).Trim('\0')); // Add the full path to the result list.
                    charCount = MAX_PATH;
                    Array.Clear(sbPath, 0, MAX_PATH);
                } while (FindNextFileNameW(findHandle, ref charCount, sbPath));
                FindClose(findHandle);
            }
        }
        catch
        {
            return [];
        }
        return (links.Count >= 2 || !returnEmptyListIfOnlyOne) ? links : [];
    }
}
