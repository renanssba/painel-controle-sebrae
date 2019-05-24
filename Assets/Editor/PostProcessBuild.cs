using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class PostProcessBuild {
  [PostProcessBuildAttribute(1)]
  public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
    string builtDirectoryPath = Path.GetDirectoryName(pathToBuiltProject);

    if(target == BuildTarget.StandaloneWindows || target == BuildTarget.StandaloneWindows64) {
      // remove annoying PDB files
      foreach(string file in Directory.GetFiles(builtDirectoryPath, "*.pdb")) {
        Debug.Log(file + " deleted!");
        File.Delete(file);
      }
    }

//    CopyBoardcodesFile(pathToBuiltProject);
    CreateVersionTxt();
    UpdateVersionTxt();
    UpdateMobileBuildVersion(target);


    switch(target){
    case BuildTarget.Android:
    case BuildTarget.StandaloneOSXIntel:
    case BuildTarget.StandaloneOSXIntel64:
    case BuildTarget.StandaloneOSX:
      RenameRecentBuild(target, pathToBuiltProject, builtDirectoryPath);
      break;
    case BuildTarget.StandaloneWindows:
    case BuildTarget.StandaloneWindows64:
      RenameWindowsBuild(target, pathToBuiltProject, builtDirectoryPath);
      break;
    case BuildTarget.WebGL:
      RenameWebGlBuild(target, pathToBuiltProject, builtDirectoryPath);
      break;
    }
  }

  private static void CopyBoardcodesFile(string pathToBuiltProject){
    string sourcePath = Application.dataPath + "/../boardcodes.txt";
    string destPath = pathToBuiltProject + "/boardcodes.txt";

    File.Copy(sourcePath, destPath);
    Debug.Log("copied " + sourcePath + " to " + destPath);
  }

  private static void CreateVersionTxt(){
    string versionPath = Application.dataPath + "/../version.txt";

    if(!File.Exists(versionPath)){
      File.Create(versionPath).Close();
      File.WriteAllText(versionPath, "0");
    }
  }

  private static void UpdateVersionTxt(){
    string versionPath = Application.dataPath + "/../version.txt";
    int currentVersion;

    currentVersion = int.Parse(File.ReadAllText(versionPath));
    currentVersion++;
    File.WriteAllText(versionPath, currentVersion.ToString());
  }

  private static void UpdateMobileBuildVersion(BuildTarget target){
    string versionPath = Application.dataPath + "/../version.txt";
    int currentVersion;
    currentVersion = int.Parse(File.ReadAllText(versionPath));

    PlayerSettings.Android.bundleVersionCode = currentVersion;
    Debug.Log("updating Android Bundle Version Code. to: " + currentVersion);
    PlayerSettings.iOS.buildNumber = currentVersion.ToString();
  }


  private static void RenameRecentBuild(BuildTarget target, string builtExecutablePath, string builtDirectoryPath){
    string destinationPath = builtDirectoryPath + "/" + GetNewVersionName(target)  + GetPlatformExtension(target);

    File.Move(builtExecutablePath, destinationPath);
    Debug.Log("Renamed the build successfully! Path: " + destinationPath);
  }


  private static void RenameWindowsBuild(BuildTarget target, string builtExecutablePath, string builtDirectoryPath){
    string destinationPath = builtDirectoryPath + "/" + GetNewVersionName(target);

    Debug.Log("built executable path: " + builtExecutablePath);
    Debug.Log("destination path: " + destinationPath);

    Directory.CreateDirectory(destinationPath);

    File.Move(builtExecutablePath, destinationPath+"/"+PlayerSettings.productName+".exe");
    if(File.Exists(builtDirectoryPath + "/UnityPlayer.dll")){
      File.Move(builtDirectoryPath + "/UnityPlayer.dll", destinationPath + "/UnityPlayer.dll");
    }
    if(File.Exists(builtDirectoryPath + "/UnityCrashHandler32.exe")){
      File.Delete(builtDirectoryPath + "/UnityCrashHandler32.exe");
    }
    Directory.Move(builtExecutablePath.Substring(0, builtExecutablePath.Length-4)+"_Data", destinationPath+"/"+PlayerSettings.productName+"_Data");
    Directory.Move(builtDirectoryPath + "/Mono", destinationPath + "/Mono");
    Debug.Log("Renamed the build directory successfully!");
  }


  private static void RenameWebGlBuild(BuildTarget target, string builtExecutablePath, string builtDirectoryPath){
    string destinationPath = builtExecutablePath + "/" + GetNewVersionName(target);

    Debug.Log("built executable path: " + builtExecutablePath);
    Debug.Log("destination path: " + destinationPath);

    Directory.CreateDirectory(destinationPath);
    File.Move(builtExecutablePath+"/index.html", destinationPath+"/index.html");
    if(Directory.Exists(builtExecutablePath + "/TemplateData")) {
      Directory.Move(builtExecutablePath + "/TemplateData", destinationPath + "/TemplateData");
    }
    if (Directory.Exists(builtExecutablePath + "/Build")) {
      Directory.Move(builtExecutablePath + "/Build", destinationPath + "/Build");
    }
    if ( Directory.Exists(builtExecutablePath+"/Release") ){
      Directory.Move(builtExecutablePath+"/Release", destinationPath+"/Release");
    }
    if( Directory.Exists(builtExecutablePath+"/Debug") ){
      Directory.Move(builtExecutablePath+"/Debug", destinationPath+"/Debug");
    }

    //    Directory.Move(builtExecutablePath, destinationPath);
    Debug.Log("Created the build directory successfully!");
  }


  private static string GetNewVersionName(BuildTarget target){
    return PlayerSettings.productName + "_0." + GetVersionString();
  }


  private static string GetPlatformExtension(BuildTarget target){
    switch(target){
    case BuildTarget.Android:
      return ".apk";
    case BuildTarget.StandaloneOSXIntel:
    case BuildTarget.StandaloneOSXIntel64:
    case BuildTarget.StandaloneOSX:
      return ".app";
    default:
      return "";
    }
  }


  private static string GetVersionString(){
    string versionPath = Application.dataPath + "/../version.txt";
    return File.ReadAllText(versionPath);
  }

  private static string GetPlatformFolderName(BuildTarget target){
    switch(target){
    case BuildTarget.Android:
      return "Android/";
    case BuildTarget.StandaloneOSXIntel:
    case BuildTarget.StandaloneOSXIntel64:
    case BuildTarget.StandaloneOSX:
      return "Mac/";
    case BuildTarget.StandaloneWindows:
    case BuildTarget.StandaloneWindows64:
      return "Windows/";
    #if UNITY_WEBGL
    case BuildTarget.WebGL:
      return "WebGl/";
    #endif
    default:
      return "Others/";
    }
  }
}