using System;
using System.Collections.Generic;
using System.Text;

namespace EventPageGenerator
{
    // Об'єкт zrbq зберігає інф. про подію
    public class Event
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Event(DateTime date, string title, string description, string imageUrl)
        {
            Date = date;
            Title = title;
            Description = description;
            ImageUrl = imageUrl;
        }
    }

    // будівельник для створення HTML сторінки
    public class HtmlPageBuilder
    {
        private StringBuilder html = new StringBuilder();
        private List<Event> events;

        public HtmlPageBuilder(List<Event> events)
        {
            this.events = events;
        }

        public HtmlPageBuilder AddHeader(string headerText)
        {
            html.AppendLine($"<header><h1>{headerText}</h1></header>");
            return this;
        }

        public HtmlPageBuilder AddEventBlocks()
        {
            html.AppendLine("<main>");
            foreach (var ev in events)
            {
                html.AppendLine($@"
                    <div class='event-block'>
                        <h2>{ev.Title}</h2>
                        <p><strong>Date:</strong> {ev.Date.ToShortDateString()}</p>
                        <p>{ev.Description}</p>
                        <img src='{ev.ImageUrl}' alt='Event image'>
                    </div>");
            }
            html.AppendLine("</main>");
            return this;
        }

        public HtmlPageBuilder AddEventList()
        {
            html.AppendLine("<aside><h3>Event Announcements</h3><ul>");
            foreach (var ev in events)
            {
                html.AppendLine($"<li>{ev.Title}</li>");
            }
            html.AppendLine("</ul></aside>");
            return this;
        }

        public HtmlPageBuilder AddFooter(string authorInfo)
        {
            html.AppendLine($"<footer><p>Contact: {authorInfo}</p></footer>");
            return this;
        }

        public string Build()
        {
            return html.ToString();
        }
    }

    // прототип програми
    class Program
    {
        static void Main(string[] args)
        {
            // Приклад
            var events = new List<Event>
            {
                new Event(DateTime.Now, "Tech Conference 2024", "An event about the latest in tech.", "image1.jpg"),
                new Event(DateTime.Now.AddDays(10), "Art Expo", "Explore modern art by various artists.", "image2.jpg"),
                new Event(DateTime.Now.AddDays(20), "Music Festival", "Join us for a night of music and fun.", "image3.jpg")
            };

            // Вибір типу сторінки, що генерується
            Console.WriteLine("Select page type: 1 - Full page, 2 - Without announcements, 3 - Only main section");
            var choice = Console.ReadLine();

            HtmlPageBuilder builder = new HtmlPageBuilder(events);

            switch (choice)
            {
                case "1":
                    builder.AddHeader("Upcoming Events")
                           .AddEventBlocks()
                           .AddEventList()
                           .AddFooter("Team XYZ");
                    break;
                case "2":
                    builder.AddHeader("Upcoming Events")
                           .AddEventBlocks()
                           .AddFooter("Team XYZ");
                    break;
                case "3":
                    builder.AddEventBlocks();
                    break;
                default:
                    Console.WriteLine("Invalid choice");
                    return;
            }

            string resultPage = builder.Build();
            Console.WriteLine(resultPage);
        }
    }
}

