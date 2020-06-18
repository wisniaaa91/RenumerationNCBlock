using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RenumeracjaNC
{
  public partial class MainWindow : Window
  {
    //TODO: poszukać czy nie da się tego rozbić na osobne classy?

    NcFile ncFile;
    public MainWindow()
    {
      InitializeComponent();

    }
    private void SaveFile(List<string> list)
    {
      string[] fileName = ncFile.GetFileName().Split(".");
      fileName[0] = fileName[0] + "_renum";
      string fn = String.Join(".", fileName);
      StreamWriter sw = new StreamWriter(@"C:\tempNC\" + fn);
      foreach (string line in list) //TODO: przerobić na while? 
      {
        sw.WriteLine(line);
      }
      sw.Close();
    }
    private string ChoosenNcFileDialog()
    {
      OpenFileDialog chooserFileDialog = new OpenFileDialog();
      chooserFileDialog.InitialDirectory = @"C:\tempNC";
      chooserFileDialog.ShowDialog();
      string choosenFile = chooserFileDialog.FileName;
      return choosenFile;
    }
    private void WriteLinesFromNcFile(NcFile nc, TextBox tb)
    {
      tb.Clear();
      foreach (string line in nc.GetFileLines())
      {
        tb.AppendText(line + "\n");
      }
    }
    private void WriteLinesFromList(List<string> list, TextBox tb)
    {
      tb.Clear();
      foreach(string line in list)
      {
        tb.AppendText(line + "\n");
      }
    }
    private void ModifiLinesNumbers() //TODO: zmienć nazwę
    {
      List<string> linesList = ncFile.GetFileLines();
      List<string> tempList = new List<string>();
      for (int i = 0; i < linesList.Count; i++)
      {
        string[] splitLine = linesList[i].Split(" ");
        int Nb = Int32.Parse(tbStartNb.Text) + (Int32.Parse(tbIncrese.Text) * i);
        string newNb = "N" + Nb;
        string temp = splitLine[0];
        if (splitLine[0].Substring(0, 1).Contains('N'))
        {
          temp = splitLine[0].Replace(temp, newNb);
        }
        splitLine[0] = temp;
        string newLine = String.Join(" ",splitLine);
        tempList.Add(newLine);
      }
      WriteLinesFromList(tempList, tbFileLines);
      SaveFile(tempList);
    }

    private void btChooseFile_Click(object sender, RoutedEventArgs e)
    {
      string choosenFile = ChoosenNcFileDialog();
      if (choosenFile != "")
      {
        ncFile = new NcFile(choosenFile);
        lbFileNameValue.Content = ncFile.GetFileName();
        lbFilePathValue.Content = ncFile.GetFullFilePath();
        WriteLinesFromNcFile(ncFile, tbFileLines);
      }
    }
    private void Renum_Click(object sender, RoutedEventArgs e)
    {
      ModifiLinesNumbers();
    }
  }
}
