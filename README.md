# Login Page Example in C#: Snapchat Clone

This project is a desktop-based simulation of Snapchat’s login and chat interface, 
built using Windows Presentation Foundation (WPF) in C#. It serves as a demonstration 
of modern UI development in WPF, featuring a custom login screen, a basic chat 
environment, and message simulation with delivery status updates.

---

## Table of Contents

* [Overview](#overview)
* [Features](#features)
* [Technologies Used](#technologies-used)
* [Getting Started](#getting-started)
* [Project Structure](#project-structure)
* [Functionality](#functionality)
* [Future Enhancements](#future-enhancements)
* [License](#license)

---

## Overview

This application demonstrates a stylized, responsive login and messaging 
interface modeled after Snapchat. It includes UI transitions, basic input 
validation, simulated conversation logic, and dynamic message status 
handling. The layout is optimized for a 400x700 window, similar to a 
mobile form factor.

---

## Features

* Custom login interface with input validation
* Message sending with dynamic status updates (Sent → Delivered → Viewed)
* Simulated automatic replies to mimic chat interaction
* Scrollable chat view with timestamp and status indicators
* Navigation placeholders for camera, settings, stories, and discover tabs
* Fully styled UI using native WPF elements and custom templates

---

## Technologies Used

* **.NET (6.0 or higher)**
* **WPF (Windows Presentation Foundation)**
* **C# (with MVVM-lite approach)**
* **XAML for UI markup**
* **ObservableCollection** for reactive UI updates
* **DispatcherTimer** for timed message simulations

---

## Getting Started

### Prerequisites

* [.NET SDK 6.0 or later](https://dotnet.microsoft.com/download)
* Visual Studio 2022+ or Visual Studio Code with C# extensions

### Clone and Run

```bash
git clone https://github.com/HChristopherNaoyuki/login-page-example-cs.git
cd login-page-example-cs
```

1. Open the solution (`login_page_example_cs.sln`) in Visual Studio.
2. Build the project.
3. Run using `F5` or the Start button.

---

## Project Structure

```text
login-page-example-cs/
├── MainWindow.xaml            // UI definitions
├── MainWindow.xaml.cs        // Application logic
├── README.md                 // Project documentation
```

---

## Functionality

### Login

* Validates both username and password fields
* Password must be a minimum of 6 characters
* On successful login, the interface transitions to chat mode

### Chat System

* User messages are aligned right with distinct color styling
* Messages include timestamp and delivery status
* A reply is generated automatically after a short delay
* Statuses progress from “Sent” to “Delivered” to “Viewed” over time

### Navigation

* Buttons for camera, settings, chats, stories, and discover are functional placeholders that display simulated messages

---

## Future Enhancements

* Integrate a real authentication backend
* Enable actual message persistence via local storage or database
* Add image/camera/media functionality
* Expand message types (media, voice, etc.)
* Implement user profile and story sharing
* Improve layout for dynamic resizing and multiple devices

---

## License

This project is licensed under the MIT License. See the `LICENSE` file for more information.

---
