using EasyMobile;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsHandler : MonoBehaviour
{
    public NotificationsHandler instance = null;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();

        Notifications.GrantDataPrivacyConsent();

        int areNotificationsSet = PlayerPrefs.GetInt("RepeatigNotificationsSet", 0);
        if (areNotificationsSet == 0)
        {
            Invoke("ScheduleRepeatingLocalNotificationAfterDelay", 2f);
            PlayerPrefs.SetInt("RepeatigNotificationsSet", 1);
        }
    }

    void ScheduleRepeatingLocalNotificationAfterDelay()
    {
        // Prepare the notification content (see the above section).
        NotificationContent content = PrepareNotificationContent("PLAY NOW!", "The penguin misses you", "You haven't played in a while. Have another go!");

        // Set the delay time as a TimeSpan.
        TimeSpan delay = new TimeSpan(0, 1, 0);

        // Schedule the notification.
        // Notifications.ScheduleLocalNotification(delay, content, NotificationRepeat.EveryMinute);
        Notifications.ScheduleLocalNotification(delay, content, NotificationRepeat.EveryMinute);
    }

    void ScheduleLocalNotificationAtDate()
    {
        // Prepare the notification content (see the above section).
        NotificationContent content = PrepareNotificationContent("PLAY NOW!", "The penguin misses you", "You haven't played in a while. Have another go!");

        DateTime date = new DateTime(DateTime.Now.Millisecond + 86400000);

        // Schedule the notification.
        // Notifications.ScheduleLocalNotification(delay, content, NotificationRepeat.EveryMinute);
        Notifications.ScheduleLocalNotification(date, content);
    }

    NotificationContent PrepareNotificationContent(string title, string subtitle, string body)
    {
        NotificationContent content = new NotificationContent();

        // Provide the notification title.
        content.title = title;

        // You can optionally provide the notification subtitle, which is visible on iOS only.
        content.subtitle = subtitle;

        // Provide the notification message.
        content.body = body;

        // You can optionally attach custom user information to the notification
        // in form of a key-value dictionary.
        // content.userInfo = new Dictionary<string, object>();
        // content.userInfo.Add("string", "OK");
        // content.userInfo.Add("number", 3);
        // content.userInfo.Add("bool", true);

        // You can optionally assign this notification to a category using the category ID.
        // If you don't specify any category, the default one will be used.
        // Note that it's recommended to use the category ID constants from the EM_NotificationsConstants class
        // if it has been generated before. In this example, UserCategory_notification_category_test is the
        // generated constant of the category ID "notification.category.test".

        // uncomment to set a category
        // content.categoryId = EM_NotificationsConstants.UserCategory_notification_category_Ice;

        // If you want to use default small icon and large icon (on Android),
        // don't set the smallIcon and largeIcon fields of the content.
        // If you want to use custom icons instead, simply specify their names here (without file extensions).

        content.smallIcon = "ic_stat_snowflake";
        // content.largeIcon = "ic_stat_cooltemperature";

        return content;
    }
}
