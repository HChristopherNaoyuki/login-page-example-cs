using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace login_page_example_cs
{
    /// <summary>
    /// Main window for Snapchat-inspired chat application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Class representing a chat message with all necessary properties
        /// </summary>
        public class ChatMessage
        {
            public string Sender { get; set; } = "";        // Who sent the message
            public string Message { get; set; } = "";       // The message content
            public string Timestamp { get; set; } = "";     // When the message was sent
            public string Status { get; set; } = "";        // Delivery status (Sent/Delivered/Viewed)
            public Brush Background { get; set; } = Brushes.Transparent;     // Background color
            public HorizontalAlignment Alignment { get; set; } // Message alignment (left/right)
        }

        // Collection to store and display chat messages
        public ObservableCollection<ChatMessage> Messages { get; set; } = new ObservableCollection<ChatMessage>();

        // Current authenticated user
        private string currentUser = "";

        // Timer to simulate message status updates
        private DispatcherTimer messageStatusTimer;

        /// <summary>
        /// Initialize the main window components
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Bind the chat messages collection to the UI
            chatMessages.ItemsSource = Messages;

            // Set up timer to update message statuses every 2 seconds
            messageStatusTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            messageStatusTimer.Tick += UpdateMessageStatus;

            // Set initial focus to username field when window loads
            Loaded += (sender, e) =>
            {
                usernameInput.Focus();
                usernameInput.SelectAll();
            };
        }

        /// <summary>
        /// Handle user login attempt
        /// </summary>
        private void LoginUser(object sender, RoutedEventArgs e)
        {
            // Get user input
            string username = usernameInput.Text == "Username" ? "" : usernameInput.Text.Trim();
            string password = passwordInput.Password;

            // Validate input fields
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowLoginError("Please enter both username and password");
                return;
            }

            // Simple password validation
            if (password.Length < 6)
            {
                ShowLoginError("Password must be at least 6 characters");
                return;
            }

            // Successful login - update UI state
            currentUser = username;
            loginScreen.Visibility = Visibility.Collapsed;
            chatScreen.Visibility = Visibility.Visible;

            // Add welcome messages to the chat
            AddSystemMessage($"Welcome to Snapchat Clone, {currentUser}!");
            AddSystemMessage("Your messages will appear here");

            // Start checking message status
            messageStatusTimer.Start();

            // Focus message input
            messageInput.Focus();
        }

        /// <summary>
        /// Display login error message
        /// </summary>
        private void ShowLoginError(string message)
        {
            loginStatus.Text = message;
            loginStatus.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Send a new chat message
        /// </summary>
        private void SendMessage(object sender, RoutedEventArgs e)
        {
            string message = messageInput.Text == "Send a chat..." ? "" : messageInput.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                // Add user message to chat (right-aligned with yellow background)
                AddChatMessage(
                    sender: currentUser,
                    message: message,
                    background: new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFCEE4B")),
                    alignment: HorizontalAlignment.Right,
                    status: "Sent");

                // Clear input field
                messageInput.Text = "";

                // Simulate reply after a short delay
                SimulateReply();
            }
        }

        /// <summary>
        /// Add a system message (centered with gray background)
        /// </summary>
        private void AddSystemMessage(string message)
        {
            AddChatMessage(
                sender: "System",
                message: message,
                background: Brushes.LightGray,
                alignment: HorizontalAlignment.Center,
                status: "");
        }

        /// <summary>
        /// Add a chat message to the collection
        /// </summary>
        private void AddChatMessage(string sender, string message, Brush background,
                                  HorizontalAlignment alignment, string status)
        {
            // Create new message with current timestamp
            var newMessage = new ChatMessage
            {
                Sender = sender,
                Message = message,
                Timestamp = DateTime.Now.ToString("h:mm tt"),
                Status = status,
                Background = background,
                Alignment = alignment
            };

            // Add to observable collection (UI updates automatically)
            Messages.Add(newMessage);

            // Scroll to show the new message
            chatScrollViewer.ScrollToBottom();
        }

        /// <summary>
        /// Simulate a friend replying to your message
        /// </summary>
        private void SimulateReply()
        {
            // Create timer for delayed response
            DispatcherTimer replyTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1.5)
            };

            // Set up timer callback
            replyTimer.Tick += (s, args) =>
            {
                replyTimer.Stop();

                // Add reply message (left-aligned with white background)
                AddChatMessage(
                    sender: "Friend",
                    message: GetRandomReply(),
                    background: Brushes.White,
                    alignment: HorizontalAlignment.Left,
                    status: "Delivered");
            };

            // Start the timer
            replyTimer.Start();
        }

        /// <summary>
        /// Update message statuses (Sent -> Delivered -> Viewed)
        /// </summary>
        private void UpdateMessageStatus(object sender, EventArgs e)
        {
            // Check all messages for status updates
            for (int i = 0; i < Messages.Count; i++)
            {
                var message = Messages[i];

                // Only update status for messages sent by current user
                if (message.Sender == currentUser)
                {
                    if (message.Status == "Sent")
                    {
                        message.Status = "Delivered";
                    }
                    else if (message.Status == "Delivered" && i == Messages.Count - 1)
                    {
                        // Only mark as viewed if it's the most recent message
                        message.Status = "Viewed";
                    }
                }
            }
        }

        /// <summary>
        /// Generate random replies for demo purposes
        /// </summary>
        private string GetRandomReply()
        {
            string[] replies = {
                "Hey there! 👋",
                "What's up?",
                "Nice! 😊",
                "I'll get back to you later",
                "Cool!",
                "LOL 😂",
                "I'm busy right now",
                "Let's meet soon!",
                "Did you see my story?",
                "👍"
            };

            // Return random reply from array
            return replies[new Random().Next(replies.Length)];
        }

        /// <summary>
        /// Handle Enter key press in message input
        /// </summary>
        private void OnMessageKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage(sender, e);
            }
        }

        // Placeholder methods for navigation buttons
        private void OpenCamera(object sender, RoutedEventArgs e) =>
            AddSystemMessage("Camera feature would open here");

        private void OpenSettings(object sender, RoutedEventArgs e) =>
            AddSystemMessage("Settings screen would open here");

        private void ShowChats(object sender, RoutedEventArgs e) =>
            AddSystemMessage("Chat list would display here");

        private void ShowStories(object sender, RoutedEventArgs e) =>
            AddSystemMessage("Stories would display here");

        private void ShowDiscover(object sender, RoutedEventArgs e) =>
            AddSystemMessage("Discover content would display here");
    }
}