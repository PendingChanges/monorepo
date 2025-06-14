﻿using MailKit;
using MailKit.Search;

using MailKit.Net.Imap;

namespace TestClient;

static class Program
{
    public static void Main(string[] args)
    {
        using (var client = new ImapClient())
        {
            using (var cancel = new CancellationTokenSource())
            {
                client.Connect("imap.gmail.com", 993, true, cancel.Token);

                // If you want to disable an authentication mechanism,
                // you can do so by removing the mechanism like this:
                client.AuthenticationMechanisms.Remove("XOAUTH");

                client.Authenticate("aubert.remi", "irfw txml ceqf vcna", cancel.Token);

                // The Inbox folder is always available...
                var inbox = client.Inbox;
                inbox.Open(FolderAccess.ReadOnly, cancel.Token);

                Console.WriteLine("Total messages: {0}", inbox.Count);
                Console.WriteLine("Recent messages: {0}", inbox.Recent);

                // download each message based on the message index
                for (int i = 0; i < inbox.Count; i++)
                {
                    var message = inbox.GetMessage(i, cancel.Token);
                    Console.WriteLine("Subject: {0}", message.Subject);
                }

                // let's try searching for some messages...
                var query = SearchQuery.All;

                foreach (var uid in inbox.Search(query, cancel.Token))
                {
                    var message = inbox.GetMessage(uid, cancel.Token);
                    Console.WriteLine("[match] {0}: {1}", uid, message.Subject);
                }

                client.Disconnect(true, cancel.Token);
            }
        }
    }
}