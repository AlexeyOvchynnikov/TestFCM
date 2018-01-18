using System;
using Firebase.CloudMessaging;
using Firebase.InstanceID;
using Foundation;
using UIKit;
using UserNotifications;
using static Firebase.CloudMessaging.Messaging;

namespace TestFCM.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            Firebase.Core.App.Configure();
            // Register your app for remote notifications.

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // iOS 10 or later
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) =>
                {
                    Console.WriteLine(granted);
                });

                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;

                // For iOS 10 data message (sent via FCM)
#pragma warning disable 618
                SharedInstance.RemoteMessageDelegate = this;
#pragma warning restore 618


            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
#pragma warning disable 618
            SharedInstance.Connect(error =>
#pragma warning restore 618
            {
                if (error != null)
                {
                    // Handle if something went wrong while connecting
                }
                else
                {
                    // Let the user know that connection was successful
                }
            });
            var token = InstanceId.SharedInstance.Token;
            // Monitor token generation
            InstanceId.Notifications.ObserveTokenRefresh((sender, e) =>
            {
                // Note that this callback will be fired everytime a new token is generated, including the first
                // time. So if you need to retrieve the token as soon as it is available this is where that
                // should be done.
                var refreshedToken = InstanceId.SharedInstance.Token;

                // Do your magic to refresh the token where is needed
            });
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
            return base.FinishedLaunching(app, options);
        }

        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.

            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);
        }
        [Export("userNotificationCenter:didReceiveNotificationResponse:withCompletionHandler:")]
        public void DidReceiveNotificationResponse(UNUserNotificationCenter center, UNNotificationResponse response, Action completionHandler)
        {
            // Do your magic to handle the notification data
            System.Console.WriteLine(response);
        }
        public override void DidEnterBackground(UIApplication application)
        {
            // Use this method to release shared resources, save user data, invalidate timers and store the application state.
            // If your application supports background exection this method is called instead of WillTerminate when the user quits.
#pragma warning disable 618
            SharedInstance?.Disconnect();
#pragma warning restore 618

            Console.WriteLine("Disconnected from FCM");
        }

        public override void OnActivated(UIApplication uiApplication)
        {
#pragma warning disable 618
            Messaging.SharedInstance.Connect(error => {
#pragma warning restore 618
                if (error != null)
                {
                    // Handle if something went wrong while connecting
                }
                else
                {
                    // Let the user know that connection was successful
                }
            });
            base.OnActivated(uiApplication);
        }

        // To receive notifications in foreground on iOS 10 devices.
        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do your magic to handle the notification data
            System.Console.WriteLine(notification.Request.Content.UserInfo);
        }

        // Receive data message on iOS 10 devices.
        public void ApplicationReceivedRemoteMessage(RemoteMessage remoteMessage)
        {
            Console.WriteLine(remoteMessage.AppData);
        }

        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            throw new NotImplementedException();
        }

        
    }
}
