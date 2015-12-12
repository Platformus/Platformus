# Platformus 1.0.0-alpha1

## Introduction

Platformus is free, open source and cross-platform CMS based on ASP.NET 5 and
[ExtCore framework](https://github.com/ExtCore/ExtCore). It is built using the best and the most
modern tools and languages (Visual Studio 2015, C#, TypeScript, SCSS etc). Join our team!

Platformus is completely modular and currently consists of 8 extensions:

* Platformus.Barebone;
* Platformus.Configuration;
* Platformus.Security;
* Platformus.Static;
* Platformus.Globalization;
* Platformus.Content;
* Platformus.Navigation;
* Platformus.Forms.

Each extension may consist of several projects:

* Platformus.X;
* Platformus.X.Data.Models;
* Platformus.X.Data.Abstractions;
* Platformus.X.Data.SpecificStorageA;
* Platformus.X.Data.SpecificStorageB;
* Platformus.X.Data.SpecificStorageC;
* Platformus.X.Frontend;
* Platformus.X.Backend;
* etc.

Using the features of the underlying ExtCore framework you can easily create your own extensions
to extend the functionality of Platformus.

## Basic Concepts

Platformus is object-oriented CMS and object is the central unit of its data model. Objects can be
standalone and embedded. While standalone objects can be accessed via URL, embedded objects can only be
used as the part of others.

Each object consists of properties and relations and is described by its class. Classes describe properties and
relations of the objects with the members. Each member has code, name, data type (for properties) or class (for
relations). In addition, with the data sources, classes describe which objects are to be loaded together with
the object.

For example, let’s say we have Developer class and Team class. Also, we can have Contact class too. Each
developer should have first name and last name properties and one relation to the object of class Team and one
or many relations to the objects of class Contact.

## Quick Start

It is very easy to use Platformus. Please take a look at our
[sample project](https://github.com/Platformus/Platformus-Sample) on GitHub.

You can also download our [ready to use sample project](http://platformus.net/files/Platformus-Samle-1.0.0-alpha1.zip).
It contains everything you need to run Platformus from Visual Studio 2015, including SQLite
database with the test data.

## Links

Website: http://platformus.net/ (under construction)

Docs: http://docs.platformus.net/ (under construction)