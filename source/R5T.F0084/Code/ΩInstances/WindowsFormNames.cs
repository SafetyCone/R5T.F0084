using System;


namespace R5T.F0084
{
    public class WindowsFormNames : IWindowsFormNames
    {
        #region Infrastructure

        public static IWindowsFormNames Instance { get; } = new WindowsFormNames();


        private WindowsFormNames()
        {
        }

        #endregion
    }
}
