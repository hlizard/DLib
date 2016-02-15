using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLib
{
    public static class ThreadHelper
    {
        public static void SafeExecute(this Action test, out Exception exception)
        {
            exception = null;

            try
            {
                test.Invoke();
            }
            catch (Exception ex)
            {
                exception = ex;
            }
        }
    }
}
