# **Universal Chevereto Uploadr** #

Copyright (c) 2013-2014 Dobrescu Andrei. Licensed under the GNU GPL v3.

Universal Chevereto Uploadr is a Windows program that helps you upload photos on any chevereto-based online image hosting service.
Now it's easier to upload and share photos with your friends and family!
To share a photo, just upload it (it takes just a few clicks) with the application and paste the link to the photo in the conversation!

## **Links** ##

**You can download the binaries from [here](http://newmusic.net78.net/ucuimg/Binaries.rar).**

**The thread on Chevereto's forum can be found [here](http://chevereto.com/community/threads/desktop-app-universal-chevereto-uploadr.3917/)**

The program is created in c# and requires .Net Framework 4.0 to run.
The program uses a copy of the NDDe library. Its license can be found in the executable's folder.

## **Before you run** ##

Open config.ini and replace the "Url" field with the path to your api.php file on the website and "Key" with your API key.
If you're building from sources, in the /bin/Release/ or /bin/Debug/ folder, create the config.ini file. It should look like this:

```
[Api]
Url=http://www.yourcheveretowebsite.com/api.php
Key=your_api_key
```

## **Its main features are:** ##

  * batch photo uploading from a local disk, by selecting some pictures from a folder or by draging and dropping pictures from Windows Explorer.
  * upload screenshots of the whole screen, a part of the screen ot the active window
  * upload clipboard content: a URL, a path to a local file or a bitmap
  * remote upload
  * the app uses NDDe in order to get the URL from Firefox or Opera web browser and, if the URL contains a picture, you can upload it directly, without copy-pasting.
  * after each upload, you can set the app to copy to the clipboard the URLs of the pictures
  * History - you can see the pictures you've just uploaded and you can delete them from the server.

## **Etc..** ##

This app is an open source project, licensed under the GNU GPL. Thus, you can distribute, copy and modify the program as long as you respect the developer and its work. You can even use it for your own website! To setup the application for your Chevereto-based image hosting website, in the binaries folder, open config.ini in a text editor and put your URL to the api.php file in the Url field and your api key to the Key field. Save the file and run the app! You can also modify the source code to fit best for your website. You can also place a copy of the program on your website, in order to use it by your clients. If so, you can send me a mail at contact@andob.info or usingvirtualbox@gmail.com to show off your website and/or ask any question about this software.

## **Changelog:** ##

The application was made for a Chevereto-based website, imgr.ro, which now is off. Thus, its development had occured in the recent past. Now the development is frozen.
  * development started - 14.04.2013
  * Imgr Uploadr version 1.0 - 16.04.2013 - initial release
  * Imgr Uploadr version 1.1 - 05.05.2013 - bugfixes, remote upload
  * Universal Chevereto Uploadr version 1.1 - 28.08.2013 - bugfixes
  * Universal Chevereto Uploadr version 1.2 - 10.09.2013 - redesigned the "Upload cropped screenshot" feature
  * Universal Chevereto Uploadr version 1.21 - 10.02.2014 - yet more bugfixes
  * Universal Chevereto Uploadr version 1.22 - 12.02.2014 - added hotkey shortcuts

  * to do: chevereto v3 support (as anonymous user - almost done - check the source code; as another user - you'll have to wait for https://chevereto.com/docs/api - API v2)