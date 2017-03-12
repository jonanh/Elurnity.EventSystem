# Elurnity.EventSystem
Event system for Unity3D

## Features
- Strongly typed events.
- Event bubbling in Unity scenes.

## Work in progress
- Event bubbling through a pseudo DOM [Scopes](https://docs.angularjs.org/guide/scope).
- Fast and memory leak safe by using cached open delegates and weak references.

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

