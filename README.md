🤖 Cloudflare Auto Clicker v1.0 - (File-Based Status)
<p align="center">
<img src="https://img.shields.io/badge/Language-C%23-blueviolet?style=for-the-badge" alt="Language C#">
<img src="https://img.shields.io/badge/.NET-Framework-blue?style=for-the-badge" alt=".NET Framework">
<img src="https://img.shields.io/badge/Platform-Windows-0078D6?style=for-the-badge" alt="Platform Windows">
<img src="https://img.shields.io/badge/License-MIT-green?style=for-the-badge" alt="License MIT">
</p>

A simple utility that automatically finds a specific checkbox image on the screen, clicks it, and then verifies the action by checking for a success image.

⚙️ How It Works
Continuous Scan: The program constantly scans the screen for either checkbox.png or checkbox2.png.

Auto-Click: Upon finding the checkbox image, it simulates a mouse click in the center of the image.

Verification: After clicking, it waits for 5 seconds and then scans for a success image (TrueL.png or TrueD.png).

Status Update:

If a "True" image is found, it creates or updates a file named autoclicker_status.txt with the word True.

If no "True" image is found (or no checkbox was found initially), it writes False to the file.

Loop: This process runs in an infinite loop, allowing the tool to operate continuously in the background.

🚀 Getting Started
Prerequisites
Windows Operating System.

.NET Framework.

OpenCvSharp4 library.

Running the Application
Image Files: Ensure that the image files (checkbox.png, checkbox2.png, TrueL.png, TrueD.png) are in the same directory as the executable.

Launch: Run the AutoClicker.exe executable.

Inter-Script Communication: Other scripts or applications can determine the clicker's status by periodically reading the contents of autoclicker_status.txt.

📝 Notes
This version relies on disk I/O operations, which can be relatively slow.

The application requires write permissions in its running directory to create the status file.
