# Platformus 1.0.0-alpha3

[![Join the chat at https://gitter.im/Platformus/Platformus](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Platformus/Platformus?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

## Introduction

Platformus is free, open source and cross-platform CMS based on ASP.NET 5 and
[ExtCore framework](https://github.com/ExtCore/ExtCore). It is built using the best and the most
modern tools and languages (Visual Studio 2015, C#, TypeScript, SCSS etc). Join our team!

### Few Facts About Platformus

1. It runs on Windows, Mac and Linux.
2. It is completely modular and extendable. Using the features of the underlying ExtCore framework
you can easily create your own extensions to extend its functionality. 
3. Multicultural and multilingual.
4. It is fast, flexible and easy to use. You can describe even complicated entities and their relationships
without writing any code!

## Basic Concepts

With the full set of extensions listed below Platformus is object-oriented CMS and object is the central unit
of its data model. Objects can be standalone and embedded. While standalone objects can be accessed via URL
(using some specified view), embedded objects can only be used as the part of others.

Each object consists of properties and relations and is described by its class. Classes describe properties and
relations of the objects with the members. Each member has code, name, data type (for properties) or class (for
relations). In addition, with the data sources, classes describe which objects are to be loaded together with
the object.

For example, let’s say we have Developer class and Team class. Also, we can have Contact class too. Each
developer should have first name and last name properties and one relation to the object of class Team and one
or many relations to the objects of class Contact.

## Extensions

Currently Platformus consists of 8 extensions:

*	Platformus.Barebone;
*	Platformus.Configuration;
*	Platformus.Security;
*	Platformus.Static;
*	Platformus.Globalization;
*	Platformus.Content;
*	Platformus.Navigation;
*	Platformus.Forms.

### Platformus.Barebone

This is the basic extension that contains such things as base controller class, base view component class and
base view model class, backend UI and controls etc. Every other extension uses Platformus.Barebone to have
unified look and behavior.

### Platformus.Configuration

### Platformus.Security

### Platformus.Static

### Platformus.Globalization

### Platformus.Content

### Platformus.Navigation

### Platformus.Forms

## Quick Start

### Samples

Please take a look at our [sample](https://github.com/Platformus/Platformus-Sample) on GitHub.

You can also download our [ready to use sample](http://platformus.net/files/Platformus-Sample-1.0.0-alpha3.zip).
It contains everything you need to run Platformus-based web application from Visual Studio 2015, including SQLite
database with the test data.

## Links

Website: http://platformus.net/ (under construction)

Docs: http://docs.platformus.net/ (under construction)