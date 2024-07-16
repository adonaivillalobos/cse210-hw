using System;
using System.Collections.Generic;

// Define the Address class
public class Address
{
    private string street;
    private string city;
    private string state;
    private string country;

    public Address(string street, string city, string state, string country)
    {
        this.street = street;
        this.city = city;
        this.state = state;
        this.country = country;
    }

    public string GetAddressString()
    {
        return $"{street}, {city}, {state}, {country}";
    }
}

// Define the base Event class
public abstract class Event
{
    private string title;
    private string description;
    private DateTime dateTime;
    private Address address;

    public Event(string title, string description, DateTime dateTime, Address address)
    {
        this.title = title;
        this.description = description;
        this.dateTime = dateTime;
        this.address = address;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public DateTime GetDateTime()
    {
        return dateTime;
    }

    public Address GetAddress()
    {
        return address;
    }

    public virtual string GetStandardDetails()
    {
        return $"{title}\n{description}\n{dateTime}\n{address.GetAddressString()}";
    }

    public abstract string GetFullDetails();
    public virtual string GetShortDescription()
    {
        return $"{GetType().Name}: {title}\n{dateTime}";
    }
}

// Define the Lecture class
public class Lecture : Event
{
    private string speakerName;
    private int capacity;

    public Lecture(string title, string description, DateTime dateTime, Address address, string speakerName, int capacity)
        : base(title, description, dateTime, address)
    {
        this.speakerName = speakerName;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nSpeaker: {speakerName}\nCapacity: {capacity}";
    }
}

// Define the Reception class
public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, DateTime dateTime, Address address, string rsvpEmail)
        : base(title, description, dateTime, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nRSVP Email: {rsvpEmail}";
    }
}

// Define the OutdoorGathering class
public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, DateTime dateTime, Address address, string weatherForecast)
        : base(title, description, dateTime, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nWeather: {weatherForecast}";
    }
}

// Program class to demonstrate functionality
public class Program
{
    public static void Main()
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

        // Create events
        Event lecture = new Lecture("C# Programming", "An in-depth look at C#.", new DateTime(2024, 7, 20, 18, 0, 0), address1, "Dr. Smith", 100);
        Event reception = new Reception("Networking Event", "Meet and greet with professionals.", new DateTime(2024, 7, 21, 19, 0, 0), address2, "rsvp@networkingevent.com");
        Event outdoorGathering = new OutdoorGathering("Summer Picnic", "Enjoy a day at the park.", new DateTime(2024, 7, 22, 12, 0, 0), address1, "Sunny with a chance of clouds");

        // Create a list of events
        List<Event> events = new List<Event> { lecture, reception, outdoorGathering };

        // Display details for each event
        foreach (var ev in events)
        {
            Console.WriteLine(ev.GetStandardDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetFullDetails());
            Console.WriteLine();
            Console.WriteLine(ev.GetShortDescription());
            Console.WriteLine();
        }
    }
}