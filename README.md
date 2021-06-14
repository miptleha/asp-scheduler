![scheduler](scheduler.png)

Simple day scheduler on ASP.NET Core.

Designed to create a daily plan and report. The navigation buttons can be used to change a day.

Has simple architecture: one get handler (get day info) and one post handler (save day info).

Day data are saved in xml format in application subfolder (one file for each day). Add write permissions after publishing this application to IIS. 
