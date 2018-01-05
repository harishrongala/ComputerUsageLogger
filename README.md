# Introduction
Do you binge watch on your computer? How much time do you spend on a computer doing productive activities [Subjective]? Get a number for every question. Get your computer usage statistics.

**Note:** This application logs your computer usage into your local file system. It **does not** upload that data on to any external server. 

# Technical details

#### Application Compatability: Windows

This WindowsForm Project runs in the background and collects the following information every minute
* Foreground window's process name
* Foreground window's title

It creates a log (.txt) file with current system date as it's name, like **Thursday, January 4,2018.txt**. If file exists, it would be opened to append the data. Say, you're watching a video on YouTube in Google Chrome at 10:00 AM in the morning. This application would save that activity as follows

> chrome # Your tab's title # 10:00:21 AM

Here the field seperator is **#**.

# Code files

In this repository you can find the **Visual Studio Solution files** of the project. You may download the files and change the **filePath** variable to properly run the application.

