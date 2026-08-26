using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;

[assembly: CommandClass(typeof(Batch_Scripter.BatchScripterDialog))]

namespace Batch_Scripter
{
    public class BatchScripterDialog
    {
        [CommandMethod("OW:BatchScripter", CommandFlags.Modal)]
        public void BatchScripter()
        {
            using (MainForm form = new MainForm())
                Application.ShowModalDialog(form);
        }
    }
}
