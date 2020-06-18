using System.Collections.Generic;
using System.IO;

namespace RenumeracjaNC
{
  class NcFile
  {
    private string fullFilePath;
    private string FileName;
    private List<string> lines;

    //==== Read NcFileProperties begin ====
    public string GetFullFilePath()
    {
      return fullFilePath;
    }
    public string GetFileName()
    {
      return FileName;
    }
    public List<string> GetFileLines()
    {
      return lines;
    }
    //==== Read NcFileProperties end ====
    //==== Create NcFileObject begin ====
    public NcFile(string fFP)
    {
      fullFilePath = fFP;
      FileName = extractFileName(fFP);
      lines = getLinesFromFile(fFP);
    }
    private string extractFileName(string filePath)
    {
      FileInfo fi = new FileInfo(filePath);
      string fileName = fi.Name;
      return fileName;
    }
    private List<string> getLinesFromFile(string filePath)
    {
      List<string> linesInFile = new List<string>();
      string line;
      StreamReader streamReader = new StreamReader(filePath);
      while ((line = streamReader.ReadLine()) != null)
      {
        linesInFile.Add(line);
      }
      return linesInFile;
    }
    //==== Create NcFIleObject end ====

  }
}
