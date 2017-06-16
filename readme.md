# Immutable Collections for .NET

This project is a library implementing some common immutable collections. I made it as a part of my computer science master thesis.

Mind, that this library was created for academic purposes! It lacks some final touches, to be used on production. For this reason, it was never published on NuGet. If your interested in how immutable collections are implemented, please take a look. However, if you wish to use immutable collections in your projects, please take a look at [System.Collections.Immutable](https://www.nuget.org/packages/System.Collections.Immutable).

Implemented collections:

* `ImmutableHashDictionary`
* `ImmutableHashSet`
* `ImmutableLinkedList`
* `ImmutableSortedDictionary`
* `ImmutableSortedSet`
* `ImmutableVector`

Following collections are incomplete (item removing is not implemented):

* `ImmutableRedBlackDictionary`
* `ImmutableRedBlackSet`

Collections were implemented using following immutable data structures:

* 2-3 Tree
* Bitmapped Vector Trie
* Linked List
* Patricia Trie
* Red Black Tree (incomplete)
