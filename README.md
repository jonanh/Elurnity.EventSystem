# Elurnity.EventSystem
Event system for Unity3D

## Features
- Strongly typed events.
- Event bubbling in Unity scenes.

## API

### Events

```
namespace Elurnity.EventSystem;

public struct TestEvent : Event
{
    public string field;
}
```

Events are structs implementing the Event interface.

The Event interface allows us to inspect all the events in the system using reflection.

Since they are value types, the performance impact of emiting events is low.

### Event handler

```
Action<Event> handler = (Event evt) => Console.Write(evt.ToString());
```
```
public class Receiver
{
   public void OnEventReceived(Event evt)
   {
      Console.Write(evt.ToString()
   }
}

Action<Event> handler = new Receiver().OnEventReceived;
```

### Listener

```
public sealed class EventListener
{
  public void On<T>(Action<T> listener) where T : struct, Event;
  public void On<T>(Action<T> listener, EventListener to) where T : struct, Event;
  
  public void Off<T>(Action<T> listener) where T : struct, Event;
  public void Off<T>(Action<T> listener, EventListener to) where T : struct, Event;

  public void Emit<T>(T evt) where T : struct, Event;
```

The listener delivers the emitted event into the handlers registered into it. It also allows to listen to the events emitted into another listener.

In Unity the listener class in instantiated dynamically for each game object.

## Work in progress
- Event bubbling through a [pseudo DOM](https://docs.angularjs.org/guide/scope).

A chain of listeners can be constructed.

- Fast and memory leak safe by using cached open delegates and weak references.

Open handler
```
public class Receiver
{
   public void OnEventReceived(Event evt)
   {
      Console.Write(evt + " received by " + instance);
   }
}

Action<Receiver, Event> handler = Receiver.OnEventReceived; 
```

Open handlers as opposed to closed handlers don't have an implicit reference to the receiver, allowing a weak reference to be stored pointing to the Receiver instance and sharing the same delegate handler for all the Receivers.

## Inspired by
- API inspired by [node.js's events](https://nodejs.org/api/events.html)
- Javascript bubbling [backbone's listenTo](http://backbonejs.org/#Events-listenTo)
- Angularjs's event scope bubbling.

## References

- [Simulating “Weak Delegates” in the CLR](https://blogs.msdn.microsoft.com/greg_schechter/2004/05/28/simulating-weak-delegates-in-the-clr)
- [Observable property pattern, memory leaks, and weak delegates for .NET](https://www.codeproject.com/Articles/16182/Observable-property-pattern-memory-leaks-and-weak)

## Alternatives

- [UniRx](https://github.com/neuecc/UniRx)
- [Redux.NET](https://github.com/GuillaumeSalles/redux.NET)

