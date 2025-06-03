using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace login_page_example_cs
{
    public partial class MainWindow : Window
    {
        // Store the current user name
        private string currentUser = "Guest";

        public MainWindow()
        {
            InitializeComponent();

            // Set initial state
            login_page.Visibility = Visibility.Visible;
            chats_page.Visibility = Visibility.Collapsed;

            // Set focus to login button for better accessibility
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
        }

        private void open_login(object sender, RoutedEventArgs e)
        {
            // In a real application, you would validate credentials here
            // For this example, we'll simulate a successful login

            // Simulate login validation
            bool loginSuccessful = true; // Always true for this example

            if (loginSuccessful)
            {
                // Update UI for successful login
                currentUser = "User"; // In real app, this would come from login
                welcome_message.Text = $"Welcome, {currentUser}!";

                // Switch to chat page
                login_page.Visibility = Visibility.Collapsed;
                chats_page.Visibility = Visibility.Visible;

                // Clear any previous status messages
                login_status.Visibility = Visibility.Collapsed;

                // Add a welcome message to the chat
                AddMessageToChat("System", $"Hello {currentUser}! How can I help you today?", Brushes.Gray);
            }
            else
            {
                // Show login error
                login_status.Text = "Login failed. Please try again.";
                login_status.Visibility = Visibility.Visible;
            }
        }

        private void back(object sender, RoutedEventArgs e)
        {
            // Clear chat history when logging out
            chat_display.Children.Clear();

            // Switch back to login page
            chats_page.Visibility = Visibility.Collapsed;
            login_page.Visibility = Visibility.Visible;

            // Reset the user
            currentUser = "Guest";
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            string message = message_input.Text.Trim();
            if (!string.IsNullOrEmpty(message))
            {
                // Add user message to chat
                AddMessageToChat(currentUser, message, Brushes.DodgerBlue);

                // Clear input
                message_input.Text = string.Empty;

                // Simulate a response (in a real app, this would call your chat service)
                System.Threading.ThreadPool.QueueUserWorkItem(_ =>
                {
                    // Simulate processing delay
                    System.Threading.Thread.Sleep(500);

                    // Run on UI thread
                    Dispatcher.Invoke(() =>
                    {
                        AddMessageToChat("ChatBot", "Thanks for your message!", Brushes.Green);
                    });
                });
            }
        }

        private void OnMessageKeyDown(object sender, KeyEventArgs e)
        {
            // Allow sending message with Enter key
            if (e.Key == Key.Enter)
            {
                SendMessage(sender, e);
            }
        }

        private void AddMessageToChat(string sender, string message, Brush color)
        {
            // Create a message container
            StackPanel messageContainer = new StackPanel
            {
                Margin = new Thickness(0, 5, 0, 5),
                HorizontalAlignment = sender == "ChatBot" ? HorizontalAlignment.Left : HorizontalAlignment.Right
            };

            // Add sender label
            TextBlock senderLabel = new TextBlock
            {
                Text = sender + ":",
                FontWeight = FontWeights.Bold,
                Foreground = color
            };

            // Add message text
            TextBlock messageText = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 500
            };

            // Add elements to container
            messageContainer.Children.Add(senderLabel);
            messageContainer.Children.Add(messageText);

            // Add to chat display
            chat_display.Children.Add(messageContainer);

            // Scroll to bottom
            if (chat_display.Parent is ScrollViewer scrollViewer)
            {
                scrollViewer.ScrollToEnd();
            }
        }
    }
}