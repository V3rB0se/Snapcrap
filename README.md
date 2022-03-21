# SNAPCRAP
<br>

Extract Snapchat Data on a Rooted Android Phone <br>
Inspired by <a href="#">This Spotlight Project</a>

# Q. What Is Snapcrap?
<br>
<br>
   <b>A.</b> Snapcrap is a console application i made to make it easier to extract snapchat's data (snaps and shit) stored in the data folder of snapchat
     using windows operating system.
  
# Q. Why? 
<br>
   <b>A.</b> <b> seriously? this question? anyway it wasn't because i fell for someone (jk) </b>. 
   <br> I was searching for a way to extract the (snaps saved in the chat) without notifying my friends. after spending hours of browsing and searching through the
     internet i found a gist which claims to extract the data from snapchat. The gist is created by <a href="https://github.com/programminghoch10">programminghoch10</a>
     it's a shell script. the downside of his script is you have to type a bunch of lines in terminal to get it to work. The real reason of this project is to make it 
     easier to extract the data without typing those "bunch of lines".  so i took his idea after asking him nicely. and started working on the project.
  
# Q. How it works? <br>
   <b>A.</b> it just extract the data stored in the data folder of snapchat to sdcard and change the extension to their respective file format using file headers. it works over android debugging bridge (adb).
    <br>


# Q. Requirements? <br>
    1. Rooted Android Phone (Obviously xD).
    2. Snapchat Installed (Well?).
    3. Bunch of Girlfriends (Obviously jk lol).
    4. Brain (Mandatory :P)
    
# Q. Extraction
   <br>
   1. Compile it using Visual Studio Code 2019 or 2022
                           <br><OR>
   <br>
      Download a precompiled Executable <a href="#">from here </a>
   <br>
   2. Turn on The Developer Options by tapping on build number 7 times <br>
   3. Turn on Usb Debugging from settings <br>
   4. Connect your Android Device to windows machine <br>
   5. Run the program and press Y you'll get a prompt on your android device confirming usb debugging. allow it (you will get an error) first time. <br>
   6. run the program again. once its done check the folder Snapchat_Exports 
   
   <br>
   you have to run this program 2 times its a bug im still trying to figure out a solution to this problem.
   <br>
Having a tough time doing a Magisk root ??
<br>
Follow this guide <a href="https://www.xda-developers.com/root/">XDA-Developers Root Directory</a>
<br>

# Contributors 
<ul>
Special Thanks to 
<li><a href="https://github.com/programminghoch10">programminghoch10</a></li>
<li><a href="https://github.com/quamotion/madb">Quamotion's Madb Sharp ADB Client Library</a></li>
</ul>
