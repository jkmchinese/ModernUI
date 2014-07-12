/*******************************************************************
* 版权所有： 深圳市震有科技有限公司
* 文件名称： KeyboardBehavior.cs
* 作   者： chenzhifen
* 创建日期： 2013-12-12 00:05:58
* 文件版本： 1.0.0.0
* 修改时间：             修改人：                修改内容：
*******************************************************************/

using System.Windows.Interactivity;
using System.Windows;
using System.Diagnostics;

namespace ModernUI.Behaviors
{
    /// <summary>
    /// 控件键盘行为，聚焦后弹出系统键盘
    /// </summary>
    public class KeyboardBehavior : Behavior<DependencyObject>
    {

        #region Constructors

        #endregion

        #region Properties
        public bool IsShowKeyboard
        {
            get { return (bool)GetValue(IsShowKeyboardProperty); }
            set { SetValue(IsShowKeyboardProperty, value); }
        }
        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty IsShowKeyboardProperty = DependencyProperty.Register("IsShowKeyboard", typeof(bool), typeof(KeyboardBehavior), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnIsShowKeyboardChanged)));

        #endregion

        #region Fields
        //系统键盘名
        private const string KEYBOARD_NAME = "Osk";
        private const string CMD_NAME = "cmd.exe";
        private const string EXIT = "exit";
        private static readonly object _locker = new object();
        #endregion

        #region Public Methods

        #endregion

        #region Private Methods
        private static void OnIsShowKeyboardChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var item = o as KeyboardBehavior;

            Process pro = Initial();
            lock (_locker)
            {
                if (item.IsShowKeyboard)
                {
                    ExcuteCmd(pro, KEYBOARD_NAME);
                    Debug.WriteLine("启动小键盘");
                }
                else
                {
                    CloseProcess(KEYBOARD_NAME);
                    Debug.WriteLine("关闭小键盘");
                }
            }
        }

        private static void ExcuteCmd(Process pro, string cmd)
        {
            pro.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            pro.Start();
            pro.StandardInput.WriteLine(cmd);
            pro.StandardInput.WriteLine(EXIT);
        }

        private static Process Initial()
        {
            Process pro = new Process();
            pro.StartInfo.FileName = CMD_NAME;
            pro.StartInfo.UseShellExecute = false;
            pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.RedirectStandardInput = true;
            pro.StartInfo.RedirectStandardOutput = true;
            pro.StartInfo.CreateNoWindow = true;
            return pro;
        }

        /// <summary>
        /// 关闭进程
        /// </summary>
        /// <param name="processName"></param>
        private static void CloseProcess(string processName)
        {
            Process[] process = Process.GetProcessesByName(processName);
            if (process.Length > 0)
            {
                foreach (Process item in process)
                {
                    if (!item.HasExited)
                    {
                        item.Kill();
                    }
                }
            }
        }

        #endregion
    }
}
