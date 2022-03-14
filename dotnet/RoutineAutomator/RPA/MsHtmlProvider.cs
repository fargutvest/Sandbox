using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using mshtml;
using RPA.Report;
using System.Linq;
using System.Threading;

namespace RPA
{
    public class MsHtmlProvider
    {
        private IReport _report;
        private RetryHelper _retryHelper;

        public MsHtmlProvider(IReport report)
        {
            _report = report;
            _retryHelper = new RetryHelper(_report);
        }

        public void SetInput(string locator, string value, IntPtr pointer)
        {
            var ieDom = GetCompletedUIeDom(pointer);
            var rs = ieDom.readyState;
            var inputs = GetAllInputs(ieDom);
            var input = inputs.FirstOrDefault(_ => _.id == locator);
            input.value = value;
            _report?.WriteLine($"{nameof(SetInput)} '{value}'");
        }

        public void ClickButton(string locator, IntPtr pointer)
        {
            var ieDom = GetCompletedUIeDom(pointer);
            var buttons = GetAllButtons(ieDom);
            var input = buttons.FirstOrDefault(_ => _.innerText == locator);
            input.click();
            _report?.WriteLine($"{locator} clicked");
        }

        #region private

        #region Native

        [DllImport("user32", EntryPoint = "RegisterWindowMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int RegisterWindowMessage(string lpString);

        [DllImport("user32", EntryPoint = "SendMessageTimeoutA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SendMessageTimeout(IntPtr hWnd, int msg, int wParam, int lParam, int fuFlags, int uTimeout, ref int lpdwResult);

        [DllImport("oleacc", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int ObjectFromLresult(int lResult, ref Guid riid, int wParam, ref IHTMLDocument2 ppvObject);

        private const int SMTO_ABORTIFHUNG = 2;

        #endregion Native
        
        private DispHTMLDocument GetCompletedUIeDom(IntPtr hWnd)
        {
            return _retryHelper.Retry(() =>
            {
                var result = GetIeDom(hWnd);
                if (result.readyState != "complete")
                {
                    result = null;
                }
                return result;
            }, new CancellationToken());
        }

        private DispHTMLDocument GetIeDom(IntPtr hWnd)
        {
            DispHTMLDocument result = null;

            var lRes = 0;
            var lMsg = RegisterWindowMessage("WM_HTML_GETOBJECT");
            SendMessageTimeout(hWnd, lMsg, 0, 0, SMTO_ABORTIFHUNG, 1000, ref lRes);
            if (lRes != 0)
            {
                IHTMLDocument2 ieDomFromhWnd = null;
                var iidIhtmlDocument2 = new Guid("626FC520-A41E-11CF-A731-00A0C9082637");
                var hr = ObjectFromLresult(lRes, ref iidIhtmlDocument2, 0, ref ieDomFromhWnd);
                if (hr == 0)
                {
                    result = ieDomFromhWnd as DispHTMLDocument;
                }
            }
            return result;
        }

        private List<HTMLInputElement> GetAllInputs(DispHTMLDocument ieDom)
        {
            return GetAllElements<HTMLInputElement>(ieDom, "input");
        }

        private List<HTMLButtonElement> GetAllButtons(DispHTMLDocument ieDom)
        {
            return GetAllElements<HTMLButtonElement>(ieDom, "button");
        }

        private List<T> GetAllElements<T>(DispHTMLDocument ieDom, string tagName)
        {
            var result = new List<T>();
            var elements = (ieDom.body as DispHTMLBody)?.getElementsByTagName(tagName);
            for (int i = 0; i < elements.length; i++)
            {
                result.Add((T)elements.item(i));
            }
            return result;
        }


        #endregion
    }
}
