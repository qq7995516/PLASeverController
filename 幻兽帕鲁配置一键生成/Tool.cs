using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 幻兽帕鲁配置一键生成
{
    public static class Tool
    {
        /// <summary>
        /// 检查路径是否存在,同时支持检查文件夹与文件路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns>存在则返回true</returns>
        public static bool IsExist(this string str) => Directory.Exists(str) || File.Exists(str);

        /// <summary>
        /// 选择一个文件夹路径
        /// </summary>
        /// <param name="default_path">默认打开路径</param>
        /// <param name="show_new_folder_button">是否显示新建文件夹按钮</param>
        /// <returns>返回选择的路径,如果选择了不存在的路径则返回null</returns>
        public static string SelectFolder(string default_path, bool show_new_folder_button = true)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = "选择文件夹"; //提示的文字
            folder.ShowNewFolderButton = show_new_folder_button;
            folder.SelectedPath = default_path;
            if (folder.ShowDialog() == DialogResult.OK)
                return folder.SelectedPath.IsExist() ? folder.SelectedPath : null;
            else
                return null;
        }

        /// <summary>
        /// 运行指定路径的文件   
        /// </summary>
        /// <param name="str">文件路径</param>
        /// <param name="argument">执行参数</param>
        /// <param name="UseShellExecute">使用操作系统外壳程序启动进程</param>
        /// <param name="RedirectStandardOutput">是否重定向
        /// 如果要重定向,则使用process.StandardOutput.ReadToEnd();
        /// </param>
        public static Process Run(this string str, string? argument = null, bool UseShellExecute = true, bool RedirectStandardOutput = false)
        {
            var p = new Process()
            {
                StartInfo = argument == null ?
                new ProcessStartInfo(str)
                {
                    UseShellExecute = UseShellExecute,
                    RedirectStandardOutput = RedirectStandardOutput
                }
                :
                new ProcessStartInfo(str, argument)
                {
                    UseShellExecute = UseShellExecute,
                    RedirectStandardOutput = RedirectStandardOutput
                }
            };
            p.Start();
            return p;
        }
    }
}
