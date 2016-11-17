using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Web.Script.Serialization;
using System.IO;

namespace UtaskSyllabusScraper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //ファイルをドラッグした時に、ドロップ可能であることを表示します。
        private void fileDropPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        //ファイルをドロップした時に、ファイルの処理を始めます。
        private void fileDropPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                List<string> fileList = ((string[])e.Data.GetData(DataFormats.FileDrop)).ToList<string>();
                if (fileList.Count > 0 && saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //全HTMLファイルからCSVファイルを出力します。
                    string outputPath = saveFileDialog.FileName;
                    string outputCsv = "";
                    fileList.ForEach(filePath =>
                    {
                        HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                        html.Load(filePath, Encoding.UTF8);
                        HtmlNodeCollection dataCellList = html.DocumentNode.SelectNodes("//table[@class=\"syllabus-normal\"]//td");
                        if (dataCellList == null)
                        {
                            Console.Write("xpath探索結果がnull " + filePath);
                            return;
                        }
                        Dictionary<string, string> outputList = new Dictionary<string, string>(dataCellList.Count);
                        string outputText = "";
                        for (int i = 0; i < dataCellList.Count; i++)
                        {
                            if (dataCellList[i].HasChildNodes && dataCellList[i].ChildNodes[0].Name == "a")
                            {
                                dataCellList[i] = dataCellList[i].ChildNodes[0];
                            }
                            outputText = outputText + "\"" + dataCellList[i].InnerHtml.Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n").Replace("<br>", "\\n").Replace("\\n\\n", "\\n") + "\",";
                        }
                        outputCsv = outputCsv + outputText.Substring(0, outputText.Length - 1) + Environment.NewLine;
                    });
                    try
                    {
                        StreamWriter streamWriter = new StreamWriter(outputPath, false, Encoding.UTF8);
                        streamWriter.Write(outputCsv);
                        streamWriter.Close();
                    }
                    catch (IOException error)
                    {
                        Console.Write(error.Message);
                    }
                }
                if (fileList.Count > 0 && directorySelectDialog.ShowDialog() == DialogResult.OK)
                {
                    //HTMLファイルごとにJSONファイルを出力します。
                    string outputPath = directorySelectDialog.SelectedPath;
                    if (outputPath.Substring(outputPath.Length - 1) != "" + Path.DirectorySeparatorChar)
                    {
                        outputPath = outputPath + Path.DirectorySeparatorChar;
                    }
                    fileList.ForEach(filePath =>
                    {
                        HtmlAgilityPack.HtmlDocument html = new HtmlAgilityPack.HtmlDocument();
                        html.Load(filePath, Encoding.UTF8);
                        HtmlNodeCollection titleCellList = html.DocumentNode.SelectNodes("//table[@class=\"syllabus-normal\"]//th");
                        HtmlNodeCollection dataCellList = html.DocumentNode.SelectNodes("//table[@class=\"syllabus-normal\"]//td");
                        if (titleCellList == null || dataCellList == null)
                        {
                            Console.Write("xpath探索結果がnull " + filePath);
                            return;
                        }
                        if (titleCellList.Count != dataCellList.Count)
                        {
                            Console.Write("xpath探索結果の数一致せず " + filePath);
                            return;
                        }
                        Dictionary<string, string> outputList = new Dictionary<string, string>(dataCellList.Count);
                        for (int i = 0; i < dataCellList.Count; i++)
                        {
                            if (dataCellList[i].HasChildNodes && dataCellList[i].ChildNodes[0].Name == "a")
                            {
                                dataCellList[i] = dataCellList[i].ChildNodes[0];
                            }
                            outputList.Add(titleCellList[i].InnerText, dataCellList[i].InnerHtml.Replace("\r", "").Replace("\n", "").Replace("<br>", "\n"));
                        }
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        string outputJson = serializer.Serialize(outputList);
                        try
                        {
                            FileInfo fileInfo = new FileInfo(filePath);
                            string savePath = outputPath + fileInfo.Name.Replace(fileInfo.Extension, ".json");
                            if (new FileInfo(savePath).Exists)
                            {
                                int count = 1;
                                string savePathFront = savePath.Substring(0, savePath.Length - 5);
                                while (new FileInfo(savePath).Exists)
                                {
                                    savePath = savePathFront + " (" + count + ").json";
                                    count++;
                                }
                            }
                            StreamWriter streamWriter = new StreamWriter(savePath, false, Encoding.UTF8);
                            streamWriter.Write(outputJson);
                            streamWriter.Close();
                        }
                        catch (IOException error)
                        {
                            Console.Write(error.Message);
                        }
                    });
                }
            }
        }

    }
}
